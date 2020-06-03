using System;
using System.Linq;

using BakersDozen.Customers.Core.Entities;
using BakersDozen.Customers.UnitTest.Helpers;

using FluentAssertions;

using Xunit;

namespace BakersDozen.Customers.UnitTest
{
    public class CustomerTests
    {
        [Fact]
        public void CanCreate_ShouldSetCreatedAndUpdated()
        {
            var customer = new Customer();

            customer.CreatedAt.Should().BeCloseTo(DateTime.Now);
            customer.UpdatedAt.Should().BeCloseTo(DateTime.Now);
        }
        
        [Fact]
        public void CanCreate_ShouldSetProperties()
        {
	        var customer = new CustomerBuilder().WithDefaults().Build();

	        customer.EmailAddress.Should().Be("dev@jameseastham.co.uk");
	        customer.FirstName.Should().Be("James");
	        customer.LastName.Should().Be("Eastham");
	        customer.Username.Should().Be("jeasthamdev");
        }
        
        [Fact]
        public void CanCreate_CanAddAddress()
        {
	        var customer = new CustomerBuilder().WithDefaults().Build();

	        customer.AddAddress(
		        "Home",
		        "1 My Street",
		        "My Town",
		        "AB1 1UT",
		        "United Kingdom");

	        customer.Addresses.Count.Should().Be(1);
        }
        
        [Fact]
        public void CanCreate_CanAddAddressWithSameName_ShouldReplace()
        {
	        var customer = new CustomerBuilder().WithDefaults().Build();

	        customer.AddAddress(
		        "Home",
		        "1 My Street",
		        "My Town",
		        "AB1 1UT",
		        "United Kingdom");

	        customer.AddAddress(
		        "Home",
		        "2 My Street",
		        "My Town",
		        "AB1 1UT",
		        "United Kingdom");

	        customer.Addresses.Count.Should().Be(1);
	        customer.Addresses.FirstOrDefault().AddressLine1.Should().Be("2 My Street");
        }
    }
}
