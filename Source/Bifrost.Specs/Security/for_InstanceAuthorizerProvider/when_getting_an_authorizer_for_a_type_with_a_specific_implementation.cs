using System;
using Bifrost.Security;
using Bifrost.Testing.Fakes.Domain;
using Bifrost.Testing.Fakes.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_InstanceAuthorizerProvider
{
    [Subject(typeof (InstanceAuthorizerProvider))]
    public class when_getting_an_authorizer_for_a_type_with_a_specific_implementation : given.an_instance_authorizer_provider
    {
        static IInstanceAuthorizer<StatefulAggregateRoot> authorizer;

        Because of = () => authorizer =  provider.GetInstanceAuthorizerFor<StatefulAggregateRoot>();

        It should_get_the_specific_authorizer_for_this_type = () => authorizer.ShouldBeOfType<StatefulAggregateRootAuthorizer>();
    }
}