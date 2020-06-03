using System.Threading.Tasks;

namespace BakersDozen.Customers.Core.Entities
{
    public interface ICustomerRepository
    {
	    Task<Customer> CreateAsync(
		    Customer customer);

	    Task<Customer> RetrieveAsync(
		    string emailAddress);

	    Task<Customer> UpdateAsync(
		    Customer customer);
    }
}
