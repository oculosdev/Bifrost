using System;
using Bifrost.Security;
using Bifrost.Testing.Fakes.Domain;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Security.for_InstanceAuthorizationService.given
{
    public class an_instance_authorization_service
    {
        protected static InstanceAuthorizationService authorization_service;
        protected static Mock<IInstanceAuthorizerProvider> authorizer_provider;
        protected static Mock<IInstanceAuthorizer<StatefulAggregateRoot>> stateful_aggregate_root_authorizer;
        protected static StatefulAggregateRoot unauthorized_instance;

        Establish context = () =>
            {
                unauthorized_instance = new StatefulAggregateRoot(Guid.NewGuid());
                stateful_aggregate_root_authorizer = new Mock<IInstanceAuthorizer<StatefulAggregateRoot>>();
                stateful_aggregate_root_authorizer.Setup(a => a.IsAuthorized(Moq.It.IsAny<StatefulAggregateRoot>()))
                                                  .Returns((StatefulAggregateRoot s) => s != unauthorized_instance);
                authorizer_provider = new Mock<IInstanceAuthorizerProvider>();
                authorizer_provider.Setup(p => p.GetInstanceAuthorizerFor<StatefulAggregateRoot>()).Returns(stateful_aggregate_root_authorizer.Object);

                authorization_service = new InstanceAuthorizationService(authorizer_provider.Object);
            };
    }
}