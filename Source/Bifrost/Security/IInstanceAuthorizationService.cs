namespace Bifrost.Security
{
    /// <summary>
    /// Defines a service for that will authorize access to specific instances of a type T.
    /// </summary>
    public interface IInstanceAuthorizationService
    {
        /// <summary>
        /// Indicates the result of an attempt to authorize access to the specific instance of type T.
        /// </summary>
        /// <typeparam name="T">The type of the instance to be authorized</typeparam>
        /// <param name="instance">The specific instance of type T</param>
        /// <returns>true for authorized, false for not authorized</returns>
        bool IsAuthorized<T>(T instance) where T : class;
    }
}