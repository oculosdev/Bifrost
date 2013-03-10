using Bifrost.Commands;
using Bifrost.Statistics;
using Machine.Specifications;
using System;
using System.Collections.Generic;

namespace Bifrost.Specs.Commands.for_CommandStatistics
{
    public class when_recording_a_command_result_with_an_exception : given.a_command_statistics_with_registered_recorder
    {
        static CommandResult command_result = new CommandResult(){Exception = new Exception()};

        Because of = () => command_statistics.Record(command, command_result);

        It should_add_the_statistic_to_the_store = () =>
            statistics_store_mock.Verify(s => s.Record(CommandStatistics.ContextName, CommandStatistics.HasExceptionEvent, category_owner, category));
    }
}
