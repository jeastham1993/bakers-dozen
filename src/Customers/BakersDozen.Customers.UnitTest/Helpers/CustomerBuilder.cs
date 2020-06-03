using System;
using System.Collections.Generic;
using System.Text;

using BakersDozen.Customers.Core.Entities;

namespace BakersDozen.Customers.UnitTest.Helpers
{
    internal class CustomerBuilder
    {
	    private readonly Customer _customer;

	    public CustomerBuilder()
	    {
			this._customer = new Customer();
	    }

	    public CustomerBuilder WithDefaults()
	    {
		    this._customer.FirstName = "James";
		    this._customer.LastName = "Eastham";
		    this._customer.EmailAddress = "dev@jameseastham.co.uk";
		    this._customer.Username = "jeasthamdev";

		    return this;
	    }

	    public Customer Build()
	    {
		    return this._customer;
	    }
    }
}
