using Bifrost.Execution;
using Bifrost.Statistics;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Commands.for_CommandStatistics.given
{
    public class a_command_statistics
    {
        protected static CommandStatistics command_statistics;
        protected static Mock<IStatisticsStore> statistics_store_mock;
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Mock<IContainer> container_mock;

        Establish context = () =>
        {
            type_discoverer_mock = new Mock<ITypeDiscoverer>();
            statistics_store_mock = new Mock<IStatisticsStore>();
            container_mock = new Mock<IContainer>();
            command_statistics = new CommandStatistics(statistics_store_mock.Object, type_discoverer_mock.Object, container_mock.Object);
        };
    }
}
