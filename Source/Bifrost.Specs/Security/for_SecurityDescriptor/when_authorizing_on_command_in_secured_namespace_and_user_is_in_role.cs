using System.Security.Principal;
using System.Threading;
using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_SecurityDescriptor
{
    [Subject(typeof(BaseSecurityDescriptor))]
    public class when_authorizing_on_command_in_secured_namespace_and_user_is_in_role : given.a_configured_security_descriptor_for_commands
    {
        static AuthorizeDescriptorResult authorize_descriptor_result;

        Establish context = () =>
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(""), new[] { Testing.Fakes.Security.SecurityDescriptorForCommands.NAMESPACE_ROLE });
            };

        Because of = () => authorize_descriptor_result = security_descriptor.Authorize(command_that_has_namespace_rule);

        It should_be_authorized = () => authorize_descriptor_result.IsAuthorized.ShouldBeTrue();
    }

    [Subject(typeof(BaseSecurityDescriptor))]
    public class when_authorizing_an_instance_of_stateful_aggregate_root_that_should_be_authorized : given.a_configured_security_descriptor_for_stateful_aggregate_roots
    {
        static AuthorizeDescriptorResult authorize_descriptor_result;

        Because of = () => authorize_descriptor_result = security_descriptor.Authorize(stateful_aggregate_root);

        It should_be_authorized = () => authorize_descriptor_result.IsAuthorized.ShouldBeTrue();
    }

    [Subject(typeof(BaseSecurityDescriptor))]
    public class when_authorizing_an_instance_of_stateful_aggregate_root_that_should_not_be_authorized : given.a_configured_security_descriptor_for_stateful_aggregate_roots
    {
        static AuthorizeDescriptorResult authorize_descriptor_result;

        Because of = () => authorize_descriptor_result = security_descriptor.Authorize(unauthorized_stateful_aggregate_root);

        It should_be_authorized = () => authorize_descriptor_result.IsAuthorized.ShouldBeFalse();
    }
}