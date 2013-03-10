using Bifrost.Commands;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Commands.for_CommandStatistics
{
    public class when_recording_a_command_result_that_did_not_pass_security : given.a_command_statistics_with_registered_recorder
    {
        static CommandResult command_result = command_result = new CommandResult()
        {
            SecurityMessages = new[] { "Security Error" }
        };

        Because of = () => command_statistics.Record(command, command_result);

        It should_add_the_statistic_to_the_store = () => 
            statistics_store_mock.Verify(s=>s.Record(CommandStatistics.ContextName, CommandStatistics.NotAuthorizedEvent, category_owner, category));
    }
}
