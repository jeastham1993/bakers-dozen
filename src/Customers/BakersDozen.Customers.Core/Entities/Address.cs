using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using BakersDozen.SharedKernel;

namespace BakersDozen.Customers.Core.Entities
{
    public class Address : ValueObject<Address>
    {
	    public Address(
		    string name,
		    string addressLine1,
		    string town,
		    string postcode,
		    string country)
	    {
			Guard.AgainstNullEmpty(name, nameof(name));
			Guard.AgainstNullEmpty(addressLine1, nameof(addressLine1));
			Guard.AgainstNullEmpty(postcode, nameof(postcode));
			Guard.AgainstNullEmpty(country, nameof(country));

		    this.Name = name;
		    this.AddressLine1 = addressLine1;
		    this.Town = town;
		    this.Postcode = postcode;
		    this.Country = country;
	    }

        public string Name { get; private set; }

        public string AddressLine1 { get; private set; }

        public string Town { get; private set; }

        public string Postcode { get; private set; }

        public string Country { get; private set; }
    }
}
