using System;
using Bifrost.Security;
using Bifrost.Testing.Fakes.Domain;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_InstanceAuthorizationService
{
    [Subject(typeof (InstanceAuthorizationService))]
    public class when_authorizing_an_instance : given.an_instance_authorization_service
    {
        static StatefulAggregateRoot instance;
        static bool is_authorized;

        Establish context = () => { instance = new StatefulAggregateRoot(Guid.NewGuid());  };

        Because of = () => is_authorized = authorization_service.IsAuthorized(instance);

        It should_retrieve_the_authorizer_from_the_provider = () => authorizer_provider.Verify(p => p.GetInstanceAuthorizerFor<StatefulAggregateRoot>(),Moq.Times.Once());
        It should_invoke_the_authorizer = () => stateful_aggregate_root_authorizer.Verify(a => a.IsAuthorized(instance), Moq.Times.Once());
    }
}