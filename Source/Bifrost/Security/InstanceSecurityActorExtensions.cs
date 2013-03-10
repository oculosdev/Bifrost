namespace Bifrost.Security
{
    /// <summary>
    /// Extensions for <see cref="InstanceSecurityActor"/>
    /// </summary>
    public static class InstanceSecurityActorExtensions
    {

        /// <summary>
        /// Declares that the <see cref="InstanceSecurityActor"/> must be accessible
        /// </summary>
        /// <param name="securityActor"><see cref="InstanceSecurityActor"/> to declare it for</param>
        /// <returns><see cref="InstanceSecurityActor"/> to continue the chain with</returns>
        public static InstanceSecurityActor MustBeAccessible<T>(this InstanceSecurityActor securityActor) where T : class
        {
            //TODO: need to get the IInstanceAuthorizerService into here...
            securityActor.AddRule(new AccessToInstanceRule<T>(null));
            return securityActor;
        }

    }
}