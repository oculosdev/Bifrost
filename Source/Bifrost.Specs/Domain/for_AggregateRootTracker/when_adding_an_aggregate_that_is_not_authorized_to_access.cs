using System;
using Bifrost.Domain;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Domain.for_AggregateRootTracker
{
    [Subject(typeof(AggregateRootTracker))]
    public class when_adding_an_aggregate_that_is_not_authorized_to_access : given.an_aggregate_root_tracker
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => tracker.TrackAggregate(aggregate_root_that_is_not_authorized_to_access) );

        It should_authorize_access_to_the_aggregate = () => security_manager.Verify(sm => sm.Authorize(aggregate_root_that_is_not_authorized_to_access), Times.Once());  
        It should_not_add_the_aggregate_to_the_tracked_aggregates = () => tracker.GetTrackedAggregates().ShouldBeEmpty();
        It should_throw_an_UnauthorizedAccessToAggregateRootException = () => exception.ShouldBeOfType<UnauthorizedAccessToAggregateRootException>();
    }
}