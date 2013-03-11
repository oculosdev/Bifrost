using System;
using Bifrost.SomeRandomNamespace;
using Bifrost.Testing.Fakes.Commands;
using Bifrost.Testing.Fakes.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_SecurityDescriptor.given
{
    public class a_configured_security_descriptor_for_commands
    {
        protected static SecurityDescriptorForCommands security_descriptor;
        protected static SimpleCommand command_that_has_namespace_and_type_rule;
        protected static AnotherSimpleCommand command_that_has_namespace_rule;
        protected static CommandInADifferentNamespace command_that_is_not_applicable;

        Establish context = () =>
            {
                security_descriptor = new SecurityDescriptorForCommands();

                command_that_has_namespace_and_type_rule = new SimpleCommand(Guid.NewGuid());
                command_that_has_namespace_rule = new AnotherSimpleCommand(Guid.NewGuid());
                command_that_is_not_applicable = new CommandInADifferentNamespace();
            };
    }
}