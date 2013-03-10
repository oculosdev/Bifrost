using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Specs.Commands.for_CommandContextManager;
using Bifrost.Statistics;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Commands.for_CommandStatistics.given
{
    public class a_command_statistics_with_registered_recorder
    {
        protected static ICommand command;
        protected static ICommandStatistics command_statistics;
        protected static Mock<IStatisticsStore> statistics_store_mock;
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Mock<IContainer> container_mock;
        protected static Mock<ICanRecordStatisticsForCommand> recorder_mock;
        protected static string category_owner;
        protected const string category = "Mock Category";

        Establish context = () =>
        {
            command = new SimpleCommand();
            recorder_mock = new Mock<ICanRecordStatisticsForCommand>();

            recorder_mock.Setup(r => r.Record(command, Moq.It.IsAny<CommandResult>(), Moq.It.IsAny<RecordStatisticsForCategory>()))
                .Callback((ICommand c, CommandResult cr, RecordStatisticsForCategory record) => record(category));

            category_owner = recorder_mock.Object.GetType().Name;

            type_discoverer_mock = new Mock<ITypeDiscoverer>();
            type_discoverer_mock.Setup(
                t =>t.FindMultiple<ICanRecordStatisticsForCommand>()).Returns(() => { 
                    return new [] { recorder_mock.Object.GetType() }; 
                });

            statistics_store_mock = new Mock<IStatisticsStore>();
            container_mock = new Mock<IContainer>();
            container_mock.Setup(c => c.Get(recorder_mock.Object.GetType())).Returns(recorder_mock.Object);
            command_statistics = new CommandStatistics(statistics_store_mock.Object, type_discoverer_mock.Object, container_mock.Object);
        };
    }
}
