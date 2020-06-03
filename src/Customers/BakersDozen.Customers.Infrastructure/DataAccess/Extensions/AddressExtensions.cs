using System;
using System.Collections.Generic;
using System.Text;

using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

using BakersDozen.Customers.Core.Entities;

using Newtonsoft.Json;

namespace BakersDozen.Customers.Infrastructure.DataAccess.Extensions
{
    public static class AddressExtensions
    {
	    public static Dictionary<string, AttributeValue> AsItem(
		    this Address address)
	    {
		    return Document.FromJson(JsonConvert.SerializeObject(address)).ToAttributeMap();
	    }
    }
}
