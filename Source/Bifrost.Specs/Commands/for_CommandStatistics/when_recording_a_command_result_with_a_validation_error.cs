using Bifrost.Commands;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandStatistics
{
    public class when_recording_a_command_result_with_a_validation_error : given.a_command_statistics_with_registered_recorder
    {
        static CommandResult command_result = new CommandResult()
        {
            CommandValidationMessages = new [] {"Error"}
        };

        Because of = () => command_statistics.Record(command,command_result);

        It should_add_the_statistic_to_the_store = () =>
            statistics_store_mock.Verify(s => s.Record(CommandStatistics.ContextName, CommandStatistics.InvalidEvent, category_owner, category));
    }
}
