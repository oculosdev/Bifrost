using System;
using Bifrost.Security;
using Bifrost.Testing.Fakes.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_InstanceAuthorizerProvider
{
    [Subject(typeof (InstanceAuthorizerProvider))]
    public class when_created : given.an_instance_authorizer_provider
    {
        It should_discover_all_instance_authorizers = () => type_discoverer.Verify(d => d.FindMultiple(typeof(IInstanceAuthorizer<>)), Moq.Times.Once());
        It should_create_instance_of_each_authorizer = () =>
            {
                container.Verify(c => c.Get(typeof (StatelessAggregateRootAuthorizer)), Moq.Times.Once());
                container.Verify(c => c.Get(typeof (StatefulAggregateRootAuthorizer)), Moq.Times.Once());
            };
    }
}