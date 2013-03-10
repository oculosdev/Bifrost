using Bifrost.Testing.Fakes.Domain;
using Machine.Specifications;
using System;

namespace Bifrost.Specs.Commands.for_CommandContext.given
{
    public class a_command_context_for_a_simple_command_with_one_tracked_object : a_command_context_for_a_simple_command
    {
        protected static StatefulAggregateRoot aggregate_root;

        Establish context = () =>
        {
            aggregate_root = new StatefulAggregateRoot(Guid.NewGuid());
            command_context.RegisterForTracking(aggregate_root);
        };
    }
}
