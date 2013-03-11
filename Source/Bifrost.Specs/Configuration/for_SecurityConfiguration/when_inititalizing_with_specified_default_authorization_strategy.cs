using Bifrost.Configuration;
using Bifrost.Execution;
using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Configuration.for_SecurityConfiguration
{
    [Subject(typeof(SecurityConfiguration))]
    public class when_inititalizing_with_specified_default_authorization_strategy : given.a_security_configuration
    {
        Establish context = () => security_configuration.DefaultInstanceAuthorizationStrategy = typeof(UnauthorizedByDefaultInstanceAuthorizer);

        Because of = () => security_configuration.Initialize(container.Object);

        It should_configure_the_specified_strategy = () => container.Verify(c => c.Bind<IDefaultInstanceAuthorizationStrategy>(typeof(UnauthorizedByDefaultInstanceAuthorizer), BindingLifecycle.Singleton));
    }
}