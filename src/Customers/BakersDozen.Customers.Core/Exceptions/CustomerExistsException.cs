using System;

namespace BakersDozen.Customers.Core.Exceptions
{
    /// <summary>
    /// Custom exception used to indicate a customer already exists.
    /// </summary>
    public class CustomerExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerExistsException"/> class.
        /// </summary>
        public CustomerExistsException()
	    {
	    }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerExistsException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public CustomerExistsException(string message)
	        : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerExistsException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">An inner exception.</param>
        public CustomerExistsException(string message, Exception innerException)
	        : base(message, innerException)
        {
        }
    }
}
