using System;
using Bifrost.Events;
using Bifrost.Execution;
using Bifrost.Security;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="ISecurityConfiguration"/>
    /// </summary>
    public class SecurityConfiguration : ConfigurationStorageElement, ISecurityConfiguration
    {
        /// <summary>
        /// Initializes an instance of <see cref="SecurityConfiguration"/>
        /// </summary>
        public SecurityConfiguration()
        {
            DefaultInstanceAuthorizationStrategy = typeof(AuthorizedByDefaultInstanceAuthorizer);
        }

#pragma warning disable 1591 // Xml Comments
        public Type DefaultInstanceAuthorizationStrategy { get; set; }

        public override void Initialize(IContainer container)
        {
            var authorizationStrategyType = DefaultInstanceAuthorizationStrategy ?? typeof(AuthorizedByDefaultInstanceAuthorizer);
            container.Bind<IDefaultInstanceAuthorizationStrategy>(authorizationStrategyType, BindingLifecycle.Singleton);
        }
#pragma warning restore 1591 // Xml Comments
    }
}