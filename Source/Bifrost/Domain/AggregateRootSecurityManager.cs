
using Bifrost.Security;

namespace Bifrost.Domain
{

    /// <summary>
    /// Represents an implementation of <see cref="ICommandSecurityManager"/>
    /// </summary>
    public class AggregateRootSecurityManager : IAggregateRootSecurityManager
    {
        ISecurityManager _securityManager;

        /// <summary>
        /// Initializes a new instance of <see cref="AggregateRootSecurityManager"/>
        /// </summary>
        /// <param name="securityManager"><see cref="ISecurityManager"/> for forwarding requests related to security to</param>
        public AggregateRootSecurityManager(ISecurityManager securityManager)
        {
            _securityManager = securityManager;
        }

#pragma warning disable 1591 // Xml Comments
        public AuthorizationResult Authorize(IAggregateRoot aggregateRoot)
        {
            return _securityManager.Authorize<AccessingAggregateRoot>(aggregateRoot);
        }
#pragma warning restore 1591 // Xml Comments
    }
}