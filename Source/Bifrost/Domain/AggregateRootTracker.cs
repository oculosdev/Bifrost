using System.Collections.Generic;
using Bifrost.Extensions;

namespace Bifrost.Domain
{
    /// <summary>
    /// Represents a <see cref="IAggregateRootTracker"/>
    /// </summary>
    public class AggregateRootTracker : IAggregateRootTracker
    {
        readonly IAggregateRootSecurityManager _securityManager;
        readonly List<IAggregateRoot> _objectsTracked = new List<IAggregateRoot>();

        /// <summary>
        /// Instantiates an instance of <see cref="AggregateRootTracker"/>
        /// </summary>
        /// <param name="securityManager"></param>
        public AggregateRootTracker(IAggregateRootSecurityManager securityManager)
        {
            _securityManager = securityManager;
        }

#pragma warning disable 1591 // Xml Comments
        public IEnumerable<IAggregateRoot> GetTrackedAggregates()
        {
            return _objectsTracked.ToArray();
        }

        public void TrackAggregate(IAggregateRoot aggregateRootToTrack)
        {
            ThrowIfNotAuthorizedToAccess(aggregateRootToTrack);
            _objectsTracked.Add(aggregateRootToTrack);
        }

        void ThrowIfNotAuthorizedToAccess(IAggregateRoot aggregateRoot)
        {
            var result = _securityManager.Authorize(aggregateRoot);
            if (result.IsAuthorized) return;
            var securityMessages = result.BuildFailedAuthorizationMessages();
            var exception = new UnauthorizedAccessToAggregateRootException(securityMessages.ConcatAll());
            exception.Data.Add(UnauthorizedAccessToAggregateRootException.SecurityMessages, securityMessages);
            throw exception;
        }
    }
}