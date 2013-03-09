using Bifrost.Domain;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Domain.for_AggregateRootTracker
{
    [Subject(typeof(AggregateRootTracker))]
    public class when_adding_multiple_aggregates_for_tracking : given.an_aggregate_root_tracker
    {
        static IAggregateRoot aggregate_to_track;
        static IAggregateRoot another_aggregate_to_track;

        Establish context = () =>
            {
                aggregate_to_track = new Mock<IAggregateRoot>().Object;
                another_aggregate_to_track = new Mock<IAggregateRoot>().Object;
            };

        Because of = () =>
            {
                tracker.TrackAggregate(aggregate_to_track);
                tracker.TrackAggregate(another_aggregate_to_track);
            };

        It should_authorize_access_to_each_aggregate = () =>
            {
                security_manager.Verify(sm => sm.Authorize(aggregate_to_track), Times.Once());
                security_manager.Verify(sm => sm.Authorize(another_aggregate_to_track), Times.Once());
            };
        It should_add_each_aggregate_to_the_tracked_aggregates = () => tracker.GetTrackedAggregates().ShouldContainOnly(aggregate_to_track, another_aggregate_to_track);
    }
}