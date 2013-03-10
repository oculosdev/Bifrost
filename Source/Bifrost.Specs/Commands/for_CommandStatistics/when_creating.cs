using Bifrost.Commands;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Commands.for_CommandStatistics
{
    public class when_creating : given.a_command_statistics
    {
        It should_discover_statistics_plugins = () => type_discoverer_mock.Verify(t => t.FindMultiple<ICanRecordStatisticsForCommand>(), Times.Once());
    }
}
