using System.Threading.Tasks;

namespace BakersDozen.Customers.Core.Entities
{
    public interface ICustomerRepository
    {
	    Task<Customer> CreateAsync(
		    Customer customer);

	    Task DeleteAsync(
		    Customer customer);

	    Task<Customer> RetrieveAsync(
		    string emailAddress);

	    Task<Customer> UpdateAsync(
		    Customer customer);

	    Task<Customer> DeleteAddressAsync(
		    string emailAddress,
		    string addressName);

	    Task UpdateAddressAsync(
		    string emailAddress,
		    string addressName,
		    Address address);

	    Task<Address> GetAddressAsync(
			string emailAddress,
		    string addressName);
    }
}
