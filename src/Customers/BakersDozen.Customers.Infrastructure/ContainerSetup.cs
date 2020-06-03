using System;
using System.Runtime.CompilerServices;

using Amazon.DynamoDBv2;

using BakersDozen.Customers.Core.Entities;
using BakersDozen.Customers.Infrastructure.DataAccess;

using Microsoft.Extensions.DependencyInjection;

namespace BakersDozen.Customers.Infrastructure
{
    public static class ContainerSetup
    {
	    public static IServiceCollection ConfigureServices(
		    this IServiceCollection services)
	    {

		    return services;
	    }

	    public static IServiceCollection ConfigureDynamoDb(
		    this IServiceCollection services)
	    {
		    var dynamoDbConfig = new AmazonDynamoDBConfig();
		    services.AddSingleton<AmazonDynamoDBClient>(new AmazonDynamoDBClient(dynamoDbConfig));

		    DynamoDbConstants.TableName = Environment.GetEnvironmentVariable("TABLE_NAME");

		    services.AddTransient<ICustomerRepository, CustomerRepositoryDynamoDb>();

		    return services;
	    }
    }
}
