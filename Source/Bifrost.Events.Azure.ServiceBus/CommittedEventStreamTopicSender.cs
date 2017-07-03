using Bifrost.Dynamic;
using Bifrost.Serialization;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bifrost.Events.Azure.ServiceBus
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanReceiveCommittedEventStream"/> for Azure ServiceBus
    /// </summary>
    public class CommittedEventStreamTopicSender : ICanSendCommittedEventStream
    {
        readonly ISerializer _serializer;

        ServiceBusConnectionStringBuilder _connectionStringBuilder;

        Dictionary<string, ITopicClient> _topicClients = new Dictionary<string, ITopicClient>();


        /// <summary>
        /// Initializes a new instance of <see cref="CommittedEventStreamTopicSender"/>
        /// </summary>
        /// <param name="serializer"><see cref="ISerializer"/> used for serializing messages</param>
        /// <param name="connectionStringProvider"><see cref="ICanProvideConnectionStringToSender">Provider</see> for connectionstring</param>
        public CommittedEventStreamTopicSender(ICanProvideConnectionStringToSender connectionStringProvider, ISerializer serializer)
        {
            _serializer = serializer;

            _connectionStringBuilder = new ServiceBusConnectionStringBuilder(connectionStringProvider());
        }

        private ITopicClient GetTopicClient(string topic)
        {
            ITopicClient result;

            if (_topicClients.ContainsKey(topic))
            {
                result = _topicClients[topic];
            }
            else
            {
                result = new TopicClient(_connectionStringBuilder.GetNamespaceConnectionString(), topic);
                _topicClients.Add(topic, result);
            }

            return result;
        }

        private string GetTopic(EventAndEnvelope eventAndEnvelope)
        {
            return eventAndEnvelope.Envelope.EventSource.Resource.Type.Identifier;
        }

        /// <inheritdoc/>
        public void Send(CommittedEventStream committedEventStream)
        {
            Dictionary<string, dynamic> eventsToSendByTopic = new Dictionary<string, dynamic>();

            //var eventsToSend = new List<dynamic>();

            foreach (var eventAndEnvelope in committedEventStream)
            {
                string topic = GetTopic(eventAndEnvelope);

                dynamic eventToSend = new ExpandoObject();
                eventToSend.Envelope = eventAndEnvelope.Envelope;
                eventToSend.Event = eventAndEnvelope.Event.AsExpandoObject();
                eventsToSendByTopic.Add(topic, eventToSend);
            }

            foreach (string topic in eventsToSendByTopic.Keys)
            {
                var eventsToSendAsJson = _serializer.ToJson(eventsToSendByTopic[topic]);
                var messageBodyBytes = Encoding.UTF8.GetBytes(eventsToSendAsJson);
                var message = new Message(messageBodyBytes);
                GetTopicClient(topic).SendAsync(message);
            }
        }
    }
}
