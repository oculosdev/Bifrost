using Bifrost.Security;

namespace Bifrost.Domain
{
    /// <summary>
    /// Represents a <see cref="ISecurityAction"/> for accessing an <see cref="IAggregateRoot">aggreagate root</see>
    /// </summary>
    public class AccessingAggregateRoot : SecurityAction
    {
#pragma warning disable 1591 // Xml Comments
        public override string ActionType
        {
            get { return "Accessing Aggregate Root"; }
        }
#pragma warning restore 1591 // Xml Comments
    }
}