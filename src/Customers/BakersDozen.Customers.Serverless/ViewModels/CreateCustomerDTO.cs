using System;
using System.Collections.Generic;
using System.Text;

namespace BakersDozen.Customers.Serverless.ViewModels
{
    public class CreateCustomerDTO
    {
        public string EmailAddress { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AddressName { get; set; }

        public string AddressLine1 { get; set; }

        public string Town { get; set; }

        public string Postcode { get; set; }

        public string Country { get; set; }
    }
}
