using System;
using Bifrost.Configuration;
using Bifrost.Execution;
using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Configuration.for_SecurityConfiguration
{
    [Subject(typeof (SecurityConfiguration))]
    public class when_inititalizing_without_default_authorization_strategy : given.a_security_configuration
    {
        Because of = () => security_configuration.Initialize(container.Object);

        It should_configure_the_authorized_by_default_strategy = () => container.Verify(c => c.Bind<IDefaultInstanceAuthorizationStrategy>(typeof(AuthorizedByDefaultInstanceAuthorizer), BindingLifecycle.Singleton));
    }
}