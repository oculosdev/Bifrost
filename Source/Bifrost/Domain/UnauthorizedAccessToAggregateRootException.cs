using System;

namespace Bifrost.Domain
{
    /// <summary>
    /// The exception that is thrown when there is access to 
    /// an <see cref="AggregateRoot">AggregateRoot</see> that is not authorized
    /// </summary>
    public class UnauthorizedAccessToAggregateRootException : Exception
    {
        /// <summary>
        /// Key to access Security Messages in the Exception Data dictionary. 
        /// </summary>
        public const string SecurityMessages = "SECURITY_MESSAGES";

        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedAccessToAggregateRootException">IllegalAccessToAggregateRootException</see> class
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        public UnauthorizedAccessToAggregateRootException(string message)
            : base(message)
        {
        }
    }
}