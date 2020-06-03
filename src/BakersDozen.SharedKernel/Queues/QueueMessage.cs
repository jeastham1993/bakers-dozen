using System;

namespace BakersDozen.SharedKernel.Queues
{
    /// <summary>
    /// Base class for a queue message.
    /// </summary>
    public interface QueueMessage
    {
	    DateTime DateCreated { get; }

        string QueueName { get; }
    }
}
