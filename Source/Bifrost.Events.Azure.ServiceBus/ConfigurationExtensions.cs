﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Configuration;
using Bifrost.Events.Azure.ServiceBus;

namespace Bifrost.Events
{
    /// <summary>
    /// Extensions for configuring RabbitMQ related communication for <see cref="IEvent">events</see>
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Configure <see cref="ICanSendCommittedEventStream"/> using RabbitMQ
        /// </summary>
        /// <param name="configuration"><see cref="CommittedEventStreamReceiverConfiguration"/> to configure</param>
        /// <param name="connectionString">ConnectionString to connect with</param>
        /// <returns>Chained <see cref="CommittedEventStreamReceiverConfiguration"/></returns>
        public static CommittedEventStreamSenderConfiguration UsingServiceBus(this CommittedEventStreamSenderConfiguration configuration, string connectionString)
        {
            configuration.CommittedEventStreamSender = typeof(CommittedEventStreamQueueSender);
            Configure.Instance.Container.Bind<ICanProvideConnectionStringToSender>(() => connectionString);
            return configuration;
        }


        /// <summary>
        /// Configure <see cref="ICanReceiveCommittedEventStream"/> using Azure Service Bus
        /// </summary>
        /// <param name="configuration"><see cref="CommittedEventStreamReceiverConfiguration"/> to configure</param>
        /// <param name="connectionString">ConnectionString to connect with</param>
        /// <returns>Chained <see cref="CommittedEventStreamReceiverConfiguration"/></returns>
        public static CommittedEventStreamReceiverConfiguration UsingServiceBusQueues(this CommittedEventStreamReceiverConfiguration configuration, string connectionString)
        {
            configuration.CommittedEventStreamReceiver = typeof(CommittedEventStreamQueueReceiver);
            Configure.Instance.Container.Bind<ICanProvideConnectionStringToReceiver>(() => connectionString);
            return configuration;
        }

        /// <summary>
        /// Configure <see cref="ICanReceiveCommittedEventStream"/> using Azure Service Bus
        /// </summary>
        /// <param name="configuration"><see cref="CommittedEventStreamReceiverConfiguration"/> to configure</param>
        /// <param name="connectionString">ConnectionString to connect with</param>
        /// <param name="topicProvider">Topic provider</param>
        /// <returns>Chained <see cref="CommittedEventStreamReceiverConfiguration"/></returns>
        public static CommittedEventStreamReceiverConfiguration UsingServiceBusSubscriptions(this CommittedEventStreamReceiverConfiguration configuration, string connectionString, ICanProvideSubscriptionTopics topicProvider)
        {
            configuration.CommittedEventStreamReceiver = typeof(CommittedEventStreamSubscriptionReceiver);
            Configure.Instance.Container.Bind<ICanProvideConnectionStringToReceiver>(() => connectionString);
            Configure.Instance.Container.Bind<ICanProvideSubscriptionTopics>(() => topicProvider);
            return configuration;
        }
    }
}
