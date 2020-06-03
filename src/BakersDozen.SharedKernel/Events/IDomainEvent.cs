using System;

namespace BakersDozen.SharedKernel.Events
{
    public interface IDomainEvent
    {
        /// <summary>
        /// Gets the date the event occurred.
        /// </summary>
	    DateTime DateOccurred { get; }
    }
}
