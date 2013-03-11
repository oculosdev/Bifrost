namespace Bifrost.Security
{
    /// <summary>
    /// Represents a implementation for <see cref="IDefaultInstanceAuthorizer"/> that always authorizes access.
    /// </summary>
    /// <typeparam name="T">Type of the instance to authorize</typeparam>
    public class DefaultInstanceAuthorizer<T> : IInstanceAuthorizer<T> where T : class
    {
        IDefaultInstanceAuthorizationStrategy _defaultInstanceAuthorization;

        /// <summary>
        /// Instantiates an instance of <see cref="DefaultInstanceAuthorizer{T}"/>
        /// </summary>
        /// <param name="defaultInstanceAuthorization"></param>
        public DefaultInstanceAuthorizer(IDefaultInstanceAuthorizationStrategy defaultInstanceAuthorization)
        {
            _defaultInstanceAuthorization = defaultInstanceAuthorization;
        }

#pragma warning disable 1591 // Xml Comments
        public bool CanAuthorize(object instance)
        {
            return true;
        }

        public bool IsAuthorized(T instance)
        {
            return _defaultInstanceAuthorization.IsAuthorized(instance);
        }
#pragma warning restore 1591 // Xml Comments  
    }
}