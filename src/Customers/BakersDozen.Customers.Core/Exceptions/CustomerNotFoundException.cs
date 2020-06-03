using System;

namespace BakersDozen.Customers.Core.Exceptions
{
    /// <summary>
    /// Custom exception used to indicate a customer could not be found.
    /// </summary>
    public class CustomerNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerNotFoundException"/> class.
        /// </summary>
        public CustomerNotFoundException()
	    {
	    }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public CustomerNotFoundException(string message)
	        : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">An inner exception.</param>
        public CustomerNotFoundException(string message, Exception innerException)
	        : base(message, innerException)
        {
        }
    }
}
