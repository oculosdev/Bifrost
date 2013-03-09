using Bifrost.Security;

namespace Bifrost.Domain
{
    /// <summary>
    /// Represents a specific <see cref="ISecurityTarget"/> for <see cref="IAggregateRoot">aggregate roots</see>
    /// </summary>
    public class AggregateRootSecurityTarget : SecurityTarget
    {
        const string AGGREGATE_ROOT = "Aggregate Root";

        /// <summary>
        /// Instantiates an instance of <see cref="AggregateRootSecurityTarget"/>
        /// </summary>
        public AggregateRootSecurityTarget()
            : base(AGGREGATE_ROOT)
        {
        }
    }
}
