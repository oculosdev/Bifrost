using Bifrost.Applications;
using Bifrost.Lifecycle;
using Bifrost.Serialization;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bifrost.Events.Azure.ServiceBus
{
    /// <summary>
    /// Represents a subscription based implementation of <see cref="ICanReceiveCommittedEventStream"/> for Azure ServiceBus
    /// </summary>
    public class CommittedEventStreamSubscriptionReceiver : ICanReceiveCommittedEventStream
    {
        readonly ISerializer _serializer;
        readonly IApplicationResourceIdentifierConverter _applicationResourceIdentifierConverter;
        readonly IApplicationResourceResolver _applicationResourceResolver;
        ICanProvideSubscriptionTopics _topicProvider;

        ServiceBusConnectionStringBuilder _connectionStringBuilder;
        Dictionary<string, SubscriptionClient> _clientsByTopic = new Dictionary<string, SubscriptionClient>();

        /// <inheritdoc/>
        public event CommittedEventStreamReceived Received = (e) => { };

        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStreamSubscriptionReceiver"/>
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> to use for deserializing <see cref="IEvent">events</see></param>
        /// <param name="applicationResourceIdentifierConverter"><see cref="IApplicationResourceIdentifierConverter"/> used for converting resource identifiers</param>
        /// <param name="applicationResourceResolver"><see cref="IApplicationResourceResolver"/> used for resolving types from <see cref="IApplicationResourceIdentifier"/></param>
        /// <param name="connectionStringProvider"><see cref="ICanProvideConnectionStringToReceiver">Provider</see> of connection string</param>
        /// <param name="topicProvider">Provider of topics to subscribe to.</param>
        public CommittedEventStreamSubscriptionReceiver(ISerializer serializer,
            IApplicationResourceIdentifierConverter applicationResourceIdentifierConverter,
            IApplicationResourceResolver applicationResourceResolver,
            ICanProvideConnectionStringToReceiver connectionStringProvider,
            ICanProvideSubscriptionTopics topicProvider)
        {
            _serializer = serializer;
            _applicationResourceIdentifierConverter = applicationResourceIdentifierConverter;
            _applicationResourceResolver = applicationResourceResolver;

            _connectionStringBuilder = new ServiceBusConnectionStringBuilder(connectionStringProvider());

            _topicProvider = topicProvider;

            foreach(string topic in _topicProvider())
            {
                //TODO: Fix subscription name.
                SubscriptionClient client = new SubscriptionClient(_connectionStringBuilder.GetEntityConnectionString(), topic, "Subscription", ReceiveMode.PeekLock);
                client.RegisterMessageHandler(Receive);
                _clientsByTopic.Add(topic, client);
            }
        }

        Task Receive(Message message, CancellationToken token)
        {
            var dynamicEventsAndEnvelopes = new List<dynamic>();
            var json = System.Text.Encoding.UTF8.GetString(message.Body);

            _serializer.FromJson(dynamicEventsAndEnvelopes, json);

            var eventsAndEnvelopes = new List<EventAndEnvelope>();

            foreach (var dynamicEventAndEnvelope in dynamicEventsAndEnvelopes)
            {
                var env = dynamicEventAndEnvelope.Envelope;

                var correlationId = (TransactionCorrelationId)Guid.Parse(env.CorrelationId.ToString());
                var eventId = (EventId)Guid.Parse(env.EventId.ToString());
                var sequenceNumber = (EventSequenceNumber)long.Parse(env.SequenceNumber.ToString());
                var sequenceNumberForEventType = (EventSequenceNumber)long.Parse(env.SequenceNumberForEventType.ToString());
                var generation = (EventGeneration)long.Parse(env.Generation.ToString());
                var @event = _applicationResourceIdentifierConverter.FromString(env.Event.ToString());
                var eventSourceId = (EventSourceId)Guid.Parse(env.EventSourceId.ToString());
                var eventSource = _applicationResourceIdentifierConverter.FromString(env.EventSource.ToString());
                var version = (EventSourceVersion)EventSourceVersion.FromCombined(double.Parse(env.Version.ToString()));
                var causedBy = (CausedBy)env.CausedBy.ToString();
                var occurred = DateTimeOffset.Parse(env.Occurred.ToString());

                var envelope = new EventEnvelope(
                    correlationId,
                    eventId,
                    sequenceNumber,
                    sequenceNumberForEventType,
                    generation,
                    @event,
                    eventSourceId,
                    eventSource,
                    version,
                    causedBy,
                    occurred
                );

                var eventType = _applicationResourceResolver.Resolve(@event);

                var eventInstance = Activator.CreateInstance(eventType, new object[] { eventSourceId }) as IEvent;
                var e = dynamicEventAndEnvelope.Event.ToString();

                _serializer.FromJson(eventInstance, e);
                eventsAndEnvelopes.Add(new EventAndEnvelope(envelope, eventInstance));
            }

            var stream = new CommittedEventStream(eventsAndEnvelopes.First().Envelope.EventSourceId, eventsAndEnvelopes);
            Received(stream);

            //_queueClient.CompleteAsync(message.SystemProperties.LockToken);

            return Task.CompletedTask;
        }
    }
}
