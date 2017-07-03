using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bifrost.Events.Azure.ServiceBus
{
    /// <summary>
    /// Defines something that can provide topics a <see cref="CommittedEventStreamSubscriptionReceiver"/> should subscribe to.
    /// </summary>
    public delegate IEnumerable<string> ICanProvideSubscriptionTopics();
}
