using Bifrost.Security;
using Bifrost.Testing.Fakes.Domain;

namespace Bifrost.Testing.Fakes.Security
{
    public class StatelessAggregateRootAuthorizer : IInstanceAuthorizer<StatelessAggregateRoot>
    {
        public bool CanAuthorize(object instance)
        {
            return instance.GetType() == typeof(StatelessAggregateRoot);
        }

        public bool IsAuthorized(StatelessAggregateRoot instance)
        {
            return true;
        }
    }
}