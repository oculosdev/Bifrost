using System.Collections.Generic;

namespace Bifrost.Domain
{
    /// <summary>
    /// Defines a tracked for <see cref="IAggregateRoot">aggregate roots</see> that are involved in a command context
    /// </summary>
    public interface IAggregateRootTracker
    {
        /// <summary>
        /// Gets the Aggregates that are being tracked
        /// </summary>
        /// <returns>Aggregate Roots being tracked</returns>
        IEnumerable<IAggregateRoot> GetTrackedAggregates();

        /// <summary>
        /// Adds an <see cref="IAggregateRoot"> aggregate root </see>  to track. 
        /// </summary>
        /// <remarks>Throws an <see cref="UnauthorizedAccessToAggregateRootException"/> is the access to the aggregate is not authorized</remarks>
        /// <param name="aggregateRootToTrack">The aggregate root to track</param>
        void TrackAggregate(IAggregateRoot aggregateRootToTrack);
    }

#pragma warning restore 1591 // Xml Comments
}