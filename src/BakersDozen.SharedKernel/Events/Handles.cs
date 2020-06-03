using System.Threading.Tasks;

namespace BakersDozen.SharedKernel.Events
{
    /// <summary>
    /// Default event handler.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface Handles<T> where T : IDomainEvent
    {
	    Task Handle(
		    T args);
    }
}
