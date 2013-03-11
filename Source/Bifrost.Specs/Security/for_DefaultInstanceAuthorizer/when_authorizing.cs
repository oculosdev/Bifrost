using System;
using Bifrost.Security;
using Bifrost.Testing.Fakes.Domain;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_DefaultInstanceAuthorizer
{
    [Subject(typeof(DefaultInstanceAuthorizer<>))]
    public class when_authorizing : given.a_default_instance_authorizer
    {
        static StatelessAggregateRoot instance;
        static bool is_authorized;

        Establish context = () => instance = new StatelessAggregateRoot(Guid.NewGuid());

        Because of = () => is_authorized = default_instance_authorizer.IsAuthorized(instance);

        It should_delegate_to_the_default_strategy = () => default_instance_authorization_strategy.Verify(s => s.IsAuthorized(instance), Moq.Times.Once());
    }
}