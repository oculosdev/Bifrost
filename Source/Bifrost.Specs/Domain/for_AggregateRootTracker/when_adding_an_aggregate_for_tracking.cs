using Bifrost.Domain;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Domain.for_AggregateRootTracker
{
    [Subject(typeof (AggregateRootTracker))]
    public class when_adding_an_aggregate_for_tracking : given.an_aggregate_root_tracker
    {
        static IAggregateRoot aggregate_to_track;

        Establish context = () =>
            {
                aggregate_to_track = new Mock<IAggregateRoot>().Object;
            };

        Because of = () => tracker.TrackAggregate(aggregate_to_track);

        It should_authorize_access_to_the_aggregate = () => security_manager.Verify(sm => sm.Authorize(aggregate_to_track),Times.Once());
        It should_add_the_aggregate_to_the_tracked_aggregates = () => tracker.GetTrackedAggregates().ShouldContainOnly(aggregate_to_track);
    }
}