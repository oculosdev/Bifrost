namespace Bifrost.Security
{
    /// <summary>
    /// Represents a specific <see cref="ISecurityRule"/> that authorizes access to instances of T
    /// </summary>
    /// <typeparam name="T">The type that the instance-specific authorization rule applies to.</typeparam>
    public class AccessToInstanceRule<T> : ISecurityRule where T : class
    {
        readonly IInstanceAuthorizationService _instanceAuthorizationService;

        /// <summary>
        /// Creates an instance of an <see cref="AccessToInstanceRule"/>
        /// </summary>
        /// <param name="instanceAuthorizationService"><see cref="IInstanceAuthorizationService"/> for authorizing the particular instance of type T</param>
        public AccessToInstanceRule(IInstanceAuthorizationService instanceAuthorizationService)
        {
            _instanceAuthorizationService = instanceAuthorizationService;
            Description = string.Format(@"AccessTo_{{{0}}}", typeof (T).Name);
        }

#pragma warning disable 1591 // Xml Comments
        public bool IsAuthorized(object securable)
        {
            return _instanceAuthorizationService.IsAuthorized(securable as T);
        }

        public string Description { get; private set; }
#pragma warning restore 1591 // Xml Comments
    }
}