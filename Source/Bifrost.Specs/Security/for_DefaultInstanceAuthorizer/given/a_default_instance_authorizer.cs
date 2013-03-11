using System;
using Bifrost.Security;
using Bifrost.Testing.Fakes.Domain;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Security.for_DefaultInstanceAuthorizer.given
{
    public class a_default_instance_authorizer
    {
        protected static DefaultInstanceAuthorizer<StatelessAggregateRoot> default_instance_authorizer;
        protected static Mock<IDefaultInstanceAuthorizationStrategy> default_instance_authorization_strategy;

        Establish context = () =>
            {
                default_instance_authorization_strategy = new Mock<IDefaultInstanceAuthorizationStrategy>();
                default_instance_authorization_strategy.Setup(s => s.IsAuthorized(Moq.It.IsAny<StatelessAggregateRoot>())).Returns(true);

                default_instance_authorizer = new DefaultInstanceAuthorizer<StatelessAggregateRoot>(default_instance_authorization_strategy.Object);
            };
    }
}