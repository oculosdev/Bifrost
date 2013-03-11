namespace Bifrost.Security
{
    /// <summary>
    /// Defines a provider that returns object-specific authorizers
    /// </summary>
    public interface IInstanceAuthorizerProvider
    {
        /// <summary>
        /// Gets an instance of an <see cref="IInstanceAuthorizer{T}"/> that can authorize an instance of the type
        /// </summary>
        /// <typeparam name="T">Type of the instance to be authorized</typeparam>
        /// <returns>An instance of <see cref="IInstanceAuthorizer{T}"/></returns>
        IInstanceAuthorizer<T> GetInstanceAuthorizerFor<T>() where T : class;
    }
}