using Bifrost.Execution;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents an instance of <see cref="IInstanceAuthorizationService"/>
    /// </summary>
    [Singleton]
    public class InstanceAuthorizationService : IInstanceAuthorizationService
    {
        readonly IInstanceAuthorizerProvider _instanceAuthorizerProvider;

        /// <summary>
        /// Creates an instance of <see cref="InstanceAuthorizationService"/>
        /// </summary>
        /// <param name="instanceAuthorizerProvider"></param>
        public InstanceAuthorizationService(IInstanceAuthorizerProvider instanceAuthorizerProvider)
        {
            _instanceAuthorizerProvider = instanceAuthorizerProvider;
        }

#pragma warning disable 1591 // Xml Comments
        public bool IsAuthorized<T>(T instance) where T : class
        {
            return _instanceAuthorizerProvider.GetInstanceAuthorizerFor<T>().IsAuthorized(instance);
        }
#pragma warning restore 1591 // Xml Comments  


    }
}