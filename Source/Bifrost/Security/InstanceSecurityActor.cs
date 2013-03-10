namespace Bifrost.Security
{
    /// <summary>
    /// Represents a concrete <see cref="SecurityActor"/> for an object instance
    /// </summary>
    public class InstanceSecurityActor : SecurityActor
    {
        /// <summary>
        /// Description of the <see cref="UserSecurityActor"/>
        /// </summary>
        public const string INSTANCE = "Instance";

        /// <summary>
        /// Instantiates an instance of <see cref="InstanceSecurityActor"/>
        /// </summary>
        public InstanceSecurityActor()
            : base(INSTANCE)
        { }

    }
}