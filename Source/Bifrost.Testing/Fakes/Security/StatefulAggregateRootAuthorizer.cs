using Bifrost.Security;
using Bifrost.Testing.Fakes.Domain;

namespace Bifrost.Testing.Fakes.Security
{
    public class StatefulAggregateRootAuthorizer : IInstanceAuthorizer<StatefulAggregateRoot>
    {
        public bool CanAuthorize(object instance)
        {
            return instance.GetType() == typeof (StatefulAggregateRoot);
        }

        public bool IsAuthorized(StatefulAggregateRoot instance)
        {
            return true;
        }
    }
}