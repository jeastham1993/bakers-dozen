using System.Threading.Tasks;

namespace BakersDozen.SharedKernel.Queues
{
    /// <summary>
    /// Encapsulates methods for interacting with a message queue.
    /// </summary>
    public interface IMessagePublisher
    {
	    Task PublishAsync<T>(
		    T message) where T : QueueMessage;
    }
}
