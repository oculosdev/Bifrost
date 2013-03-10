using System;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents a <see cref="Securable"/> that applies to a specific instance of a <see cref="System.Type"/>
    /// </summary>
    public class InstanceSecurable : Securable
    {
        const string INSTANCE = "InstanceOf_{{{0}}}";

        /// <summary>
        /// Initializes an instance of <see cref="TypeSecurable"/>
        /// </summary>
        /// <param name="type"><see cref="System.Type"/> to secure</param>
        public InstanceSecurable(Type type)
            : base(string.Format(INSTANCE, type.FullName))
        {
            Type = type;
        }


        /// <summary>
        /// Gets the type that is secured
        /// </summary>
        public Type Type { get; private set; }

#pragma warning disable 1591
        public override bool CanAuthorize(object actionToAuthorize)
        {
            return actionToAuthorize != null && Type == actionToAuthorize.GetType();
        }
#pragma warning restore 1591
    }
}