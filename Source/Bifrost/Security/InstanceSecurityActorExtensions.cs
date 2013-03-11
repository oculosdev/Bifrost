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
        /// <param name="instanceAuthorizationService">An instance of <see cref="IInstanceAuthorizationService"/></param>
        /// <returns><see cref="InstanceSecurityActor"/> to continue the chain with</returns>
        public static InstanceSecurityActor MustBeAccessible<T>(this InstanceSecurityActor securityActor, IInstanceAuthorizationService instanceAuthorizationService) where T : class
        {
            securityActor.AddRule(new AccessToInstanceRule<T>(instanceAuthorizationService));
            return securityActor;
        }

    }
}