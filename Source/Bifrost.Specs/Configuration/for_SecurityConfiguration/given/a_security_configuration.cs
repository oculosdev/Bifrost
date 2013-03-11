using System;
using Bifrost.Configuration;
using Bifrost.Execution;
using Bifrost.Security;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Configuration.for_SecurityConfiguration.given
{
    public class a_security_configuration
    {
        protected static ISecurityConfiguration security_configuration;
        protected static Mock<IContainer> container;

        Establish context = () =>
            {
                container = new Mock<IContainer>();
                container.Setup(c => c.Get(typeof(AuthorizedByDefaultInstanceAuthorizer))).Returns(new AuthorizedByDefaultInstanceAuthorizer());
                container.Setup(c => c.Get(typeof(UnauthorizedByDefaultInstanceAuthorizer))).Returns(new UnauthorizedByDefaultInstanceAuthorizer());

                security_configuration = new SecurityConfiguration();
            };
    }
}