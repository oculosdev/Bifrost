using Bifrost.Commands;
using Bifrost.Domain;
using Bifrost.Testing.Fakes.Commands;
using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Commands.for_CommandCoordinator
{
    [Subject(typeof(CommandCoordinator))]
    public class when_handling_a_command_that_accesses_an_aggregate_root_that_is_not_authorized_with_security_messages : given.a_command_coordinator
    {
        static ICommand command;
        static CommandResult result;
        static UnauthorizedAccessToAggregateRootException exception;
        static string message_one = "one";
        static string message_two = "two";

        Establish context = () =>
            {
                var security_messages = new[] { message_one, message_two };
                var exception = new UnauthorizedAccessToAggregateRootException("error");
                exception.Data.Add(UnauthorizedAccessToAggregateRootException.SecurityMessages,security_messages);
                command = new SimpleCommand();
                command_handler_manager_mock.Setup(ch => ch.Handle(command))
                                            .Throws(exception);
                var validation_results = new CommandValidationResult();
                command_validation_service_mock.Setup(cvs => cvs.Validate(command)).Returns(validation_results);
            };

        Because of = () => result = coordinator.Handle(command);

        It should_validate = () => command_validation_service_mock.Verify(c => c.Validate(command), Moq.Times.Once());
        It should_set_not_passed_in_command_result = () => result.PassedSecurity.ShouldBeFalse();
        It should_set_security_messages_from_the_exception = () => result.SecurityMessages.ShouldContain(message_one,message_two);
    }
}