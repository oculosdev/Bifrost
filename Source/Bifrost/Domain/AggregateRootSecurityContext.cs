namespace Bifrost.Domain
{
    /// <summary>
    /// Reperesents a Security Context for an <see cref="IAggregateRoot"> aggregate root </see>.
    /// </summary>
    /// <typeparam name="T">The type of the aggregate root</typeparam>
    public class AggregateRootSecurityContext<T> where T : IAggregateRoot
    {
        /// <summary>
        /// Holds the instance of the Aggregate root that this security context applies to.
        /// </summary>
        public T Instance { get; set; }
    }
}