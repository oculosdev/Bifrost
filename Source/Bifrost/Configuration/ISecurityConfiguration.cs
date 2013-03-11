using System;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Defines the configuration for security
    /// </summary>
    public interface ISecurityConfiguration : IConfigurationElement
    {
        /// <summary>
        /// Gets or sets the type of <see cref="DefaultInstanceAuthorizationStrategy"/> used for authorizing instances of types without a specific authorizer
        /// </summary>
        Type DefaultInstanceAuthorizationStrategy { get; set; }
    }
}