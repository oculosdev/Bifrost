using Bifrost.Domain;
using Bifrost.Security;
using Bifrost.Testing.Fakes.Domain;

namespace Bifrost.Testing.Fakes.Security
{
    public class SecurityDescriptorForStatefulAggregateRoot : BaseSecurityDescriptor
    {
        readonly IInstanceAuthorizationService _instanceAuthorizationService;

        public SecurityDescriptorForStatefulAggregateRoot(IInstanceAuthorizationService instanceAuthorizationService)
        {
            _instanceAuthorizationService = instanceAuthorizationService;
            When.Accessing().AggregateRoots().InstanceOfType<StatefulAggregateRoot>(i => i.Instance().MustBeAccessible<StatefulAggregateRoot>(_instanceAuthorizationService));
        }
    }
}