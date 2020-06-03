using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BakersDozen.SharedKernel;

namespace BakersDozen.Customers.Core.Entities
{
    public class Customer : Entity<Guid>
    {
	    private List<Address> _addresses;

	    public Customer()
	    {
		    this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this._addresses = new List<Address>();
	    }

        public string Username { get; set; }

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IReadOnlyCollection<Address> Addresses => this._addresses;

        public Address AddAddress(
	        string name,
	        string addressLine1,
	        string town,
	        string postcode,
	        string countryCode)
        {
	        var newAddress = new Address(
		        name,
		        addressLine1,
		        town,
		        postcode,
		        countryCode);

	        var existingAddress = this._addresses.Where(p => p.Name == newAddress.Name);

	        if (existingAddress.Any())
	        {
		        this._addresses.Remove(existingAddress.First());
	        }

	        this._addresses.Add(newAddress);

	        return newAddress;
        }
    }
}
