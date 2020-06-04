using System;
using System.Collections.Generic;
using System.Net;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace BakersDozen.Customers.LocalSetup
{
    class Program
    {
        static void Main(string[] args)
        {
            var dynamoDbConfig = new AmazonDynamoDBConfig { ServiceURL = "http://localhost:4566" };

			var client = new AmazonDynamoDBClient(dynamoDbConfig);
			var tables = client.ListTablesAsync().Result;

			if (tables.TableNames.Contains("Customers"))
			{
				client.DeleteTableAsync(new DeleteTableRequest("Customers"));
			}

			Console.WriteLine("Creating table");

			var request = new CreateTableRequest
							  {
								  TableName = "Customers",
								  AttributeDefinitions = new List<AttributeDefinition>(2)
															 {
																 new AttributeDefinition(
																	 "PK",
																	 ScalarAttributeType.S),
																 new AttributeDefinition(
																	 "SK",
																	 ScalarAttributeType.S),
															 },
								  KeySchema = new List<KeySchemaElement>(1)
												  {
													  new KeySchemaElement(
														  "PK",
														  KeyType.HASH),
													  new KeySchemaElement(
														  "SK",
														  KeyType.RANGE),
												  },
								  ProvisionedThroughput = new ProvisionedThroughput(
									  10,
									  10),
							  };

			var createResult = client.CreateTableAsync(request).Result;

			if (createResult.HttpStatusCode != HttpStatusCode.OK)
			{
				throw new Exception($"Failure creating table");
			}
        }
    }
}
