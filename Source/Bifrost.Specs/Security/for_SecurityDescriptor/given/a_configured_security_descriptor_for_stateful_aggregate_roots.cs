using System;
using Bifrost.Security;
using Bifrost.Testing.Fakes.Domain;
using Bifrost.Testing.Fakes.Security;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Security.for_SecurityDescriptor.given
{
    public class a_configured_security_descriptor_for_stateful_aggregate_roots
    {
        protected static Guid id_of_unauthorized_instance = Guid.NewGuid();

        protected static SecurityDescriptorForStatefulAggregateRoot security_descriptor;
        protected static StatefulAggregateRoot stateful_aggregate_root;
        protected static StatefulAggregateRoot unauthorized_stateful_aggregate_root;
        protected static IInstanceAuthorizationService instance_authorization_service;
        protected static Mock<IInstanceAuthorizerProvider> instance_authorizer_provider;
        protected static Mock<IInstanceAuthorizer<StatefulAggregateRoot>> instance_authorizer;

        Establish context = () =>
            {
                unauthorized_stateful_aggregate_root = new StatefulAggregateRoot(id_of_unauthorized_instance);
                stateful_aggregate_root = new StatefulAggregateRoot(Guid.NewGuid());

                instance_authorizer = new Mock<IInstanceAuthorizer<StatefulAggregateRoot>>();
                instance_authorizer.Setup(a => a.CanAuthorize(Moq.It.IsAny<StatefulAggregateRoot>())).Returns(true);
                instance_authorizer.Setup(a => a.IsAuthorized(Moq.It.IsAny<StatefulAggregateRoot>())).Returns((StatefulAggregateRoot s) => s.Id != id_of_unauthorized_instance);

                instance_authorizer_provider = new Mock<IInstanceAuthorizerProvider>();
                instance_authorizer_provider.Setup(p => p.GetInstanceAuthorizerFor<StatefulAggregateRoot>()).Returns(instance_authorizer.Object);

                instance_authorization_service = new InstanceAuthorizationService(instance_authorizer_provider.Object);

                security_descriptor = new SecurityDescriptorForStatefulAggregateRoot(instance_authorization_service);
            };
    }
}