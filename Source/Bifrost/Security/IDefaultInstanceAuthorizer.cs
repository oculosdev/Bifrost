namespace Bifrost.Security
{
    /// <summary>
    /// Defines a default instance authorization strategy for use when an instance does not have a specific authorizer
    /// </summary>
    public interface IDefaultInstanceAuthorizationStrategy
    {
        /// <summary>
        /// Authorizes the instance of Type T.
        /// </summary>
        /// <typeparam name="T">Type of the instance</typeparam>
        /// <param name="instance">Instance to be authorized</param>
        /// <returns>true for authorized, false otherwise</returns>
        bool IsAuthorized<T>(T instance);
    }
}