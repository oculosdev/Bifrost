using Bifrost.Execution;
using Bifrost.Security;
using Bifrost.Testing.Fakes.Security;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Security.for_InstanceAuthorizerProvider.given
{
    public class an_instance_authorizer_provider
    {
        protected static IInstanceAuthorizerProvider provider;
        protected static Mock<ITypeDiscoverer> type_discoverer;
        protected static Mock<IContainer> container;

        Establish context = () =>
            {
                type_discoverer = new Mock<ITypeDiscoverer>();
                container = new Mock<IContainer>();

                type_discoverer.Setup(d => d.FindMultiple(typeof(IInstanceAuthorizer<>)))
                    .Returns(new []{ typeof(StatefulAggregateRootAuthorizer), typeof(StatelessAggregateRootAuthorizer)} );

                container.Setup(c => c.Get(typeof(StatefulAggregateRootAuthorizer))).Returns(new StatefulAggregateRootAuthorizer());
                container.Setup(c => c.Get(typeof(StatelessAggregateRootAuthorizer))).Returns(new StatelessAggregateRootAuthorizer());

                provider = new InstanceAuthorizerProvider(container.Object, type_discoverer.Object, new AuthorizedByDefaultInstanceAuthorizer());
            };
    }
}