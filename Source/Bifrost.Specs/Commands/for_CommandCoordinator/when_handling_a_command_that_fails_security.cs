using Bifrost.Commands;
using Bifrost.Security;
using Bifrost.Testing.Fakes.Security;
using Machine.Specifications;
using Bifrost.Testing.Fakes.Commands;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Commands.for_CommandCoordinator
{
    [Subject(typeof(CommandCoordinator))]
    public class when_handling_a_command_that_fails_security : given.a_command_coordinator
    {
        static ICommand command;
        static CommandResult result;
        static AuthorizationResult authorization_result;

        Establish context = () => 
        {
            authorization_result = new FakeAuthorizationResult(false, new [] { "Something went wrong" });
            command = new SimpleCommand();
            command_security_manager_mock.Setup(c => c.Authorize(Moq.It.IsAny<ICommand>())).Returns(authorization_result);
        };

        Because of = () => result = coordinator.Handle(command);

        It should_not_validate = () => command_validation_service_mock.Verify(c => c.Validate(command), Moq.Times.Never());
        It should_set_not_passed_in_command_result = () => result.PassedSecurity.ShouldBeFalse();
    }
}
