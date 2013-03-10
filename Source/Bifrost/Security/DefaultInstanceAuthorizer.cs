namespace Bifrost.Security
{
    /// <summary>
    /// Represents a default implementation for <see cref="IInstanceAuthorizer"/> for instacens of types that do not have a specific implementation.
    /// Always authorizes access.
    /// </summary>
    /// <typeparam name="T">Type of the instance to authorize</typeparam>
    public class DefaultInstanceAuthorizer<T> : IInstanceAuthorizer<T> where T : class
    {
#pragma warning disable 1591 // Xml Comments
        public bool CanAuthorize(object instance)
        {
            return true;
        }

        public bool IsAuthorized(T instance)
        {
            return true;
        }
#pragma warning restore 1591 // Xml Comments  
    }
}