using Bifrost.Security;

namespace Bifrost.Domain
{
    /// <summary>
    /// Defines a manager for dealing with security for <see cref="IAggregateRoot">aggregate roots</see>
    /// </summary>
    public interface IAggregateRootSecurityManager
    {
        /// <summary>
        /// Decides whether we can handle an aggregate root in the current context
        /// </summary>
        /// <param name="aggregateRoot"><see cref="IAggregateRoot"/> to ask for</param>
        /// <returns>An <see cref="AuthorizationResult"/> with IsAuthorized flag set to true if authorized, false is not authorized</returns>
        AuthorizationResult Authorize(IAggregateRoot aggregateRoot);
    }
}