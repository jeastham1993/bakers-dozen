using System;
using System.Collections.Generic;
using System.Text;

using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

using BakersDozen.Customers.Core.Entities;

using Newtonsoft.Json;

namespace BakersDozen.Customers.Infrastructure.DataAccess.Extensions
{
	public static class CustomerExtensions
	{
		public static AttributeValue GetPk(
			this Customer customer)
		{
			return new AttributeValue($"CUSTOMER#{customer.EmailAddress}");
		}

		public static AttributeValue GetSk(
			this Customer customer)
		{
			return new AttributeValue($"CUSTOMER#{customer.EmailAddress}");
		}

		public static Dictionary<string, AttributeValue> GetKeys(
			this Customer customer)
		{
			return new Dictionary<string, AttributeValue>()
			{
				{ "PK", customer.GetPk() },
				{ "SK", customer.GetSk() }
			};
		}

		public static Dictionary<string, AttributeValue> AsItem(
			this Customer customer)
		{
			var attributeData = customer.GetKeys();

			attributeData.Add(
				"Type",
				new AttributeValue(customer.GetType().Name));
			attributeData.Add(
				"Data",
				new AttributeValue()
				{
					M = customer.AsData()
				});

			return attributeData;
		}

		private static Dictionary<string, AttributeValue> AsData(
			this Customer customer)
		{
			var document = Document.FromJson(JsonConvert.SerializeObject(customer));
			var documentAttributeMap = document.ToAttributeMap();
			documentAttributeMap.Remove("addresses");

			var addressData = new Dictionary<string, AttributeValue>();

			foreach (var address in customer.Addresses)
			{
				addressData.Add(
					address.Name,
					new AttributeValue()
					{
						M = address.AsItem(),
					});
			}

			documentAttributeMap.Add(
				"addresses",
				new AttributeValue()
				{
					M = addressData
				});

			return documentAttributeMap;
		}
	}
}