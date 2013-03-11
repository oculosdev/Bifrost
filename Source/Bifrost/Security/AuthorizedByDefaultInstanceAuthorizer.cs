namespace Bifrost.Security
{
    /// <summary>
    /// Represents a implementation for <see cref="IDefaultInstanceAuthorizer"/> that always authorizes access.
    /// </summary>
    public class AuthorizedByDefaultInstanceAuthorizer : IDefaultInstanceAuthorizationStrategy
    {
#pragma warning disable 1591 // Xml Comments
        public bool IsAuthorized<T>(T instance)
        {
            return true;
        }
#pragma warning restore 1591 // Xml Comments
    }
}