using Bifrost.Security;
using Bifrost.Testing.Fakes.Domain;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_InstanceAuthorizerProvider
{
    [Subject(typeof(InstanceAuthorizerProvider))]
    public class when_getting_an_authorizer_for_a_type_with_no_specific_implementation : given.an_instance_authorizer_provider
    {
        static IInstanceAuthorizer<StatelessAggregateRootWithOneMethod> authorizer;

        Because of = () => authorizer = provider.GetInstanceAuthorizerFor<StatelessAggregateRootWithOneMethod>();

        It should_get_the_default_authorizer_for_this_type = () => authorizer.ShouldBeOfType<DefaultInstanceAuthorizer<StatelessAggregateRootWithOneMethod>>();
    }
}