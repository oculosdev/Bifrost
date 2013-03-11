namespace Bifrost.Security
{
    /// <summary>
    /// Represents a implementation for <see cref="IDefaultInstanceAuthorizer"/> that never authorizes access.
    /// </summary>
    public class UnauthorizedByDefaultInstanceAuthorizer : IDefaultInstanceAuthorizationStrategy
    {
#pragma warning disable 1591 // Xml Comments
        public bool IsAuthorized<T>(T instance) 
        {
            return false;
        }
#pragma warning restore 1591 // Xml Comments
    }
}