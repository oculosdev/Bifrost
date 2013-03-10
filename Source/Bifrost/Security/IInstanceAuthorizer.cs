namespace Bifrost.Security
{
    /// <summary>
    /// Represents an authorizer for an instance of Type T
    /// </summary>
    /// <typeparam name="T">The Type of the instance to authorize</typeparam>
    public interface IInstanceAuthorizer<T> where T : class
    {
        /// <summary>
        /// Indicates whether this <see cref="IInstanceAuthorizer"/> can authorize the specific instance
        /// </summary>
        /// <param name="instance">The specific instance to check if can be authorized</param>
        /// <returns>true if can authorize, false otherwise</returns>
        bool CanAuthorize(object instance);

        /// <summary>
        /// Indicates whether the instance is authorized
        /// </summary>
        /// <param name="instance">Instance to authorize</param>
        /// <returns>true if the instance is authorized, false otherwise</returns>
        bool IsAuthorized(T instance);
    }
}