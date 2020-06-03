using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

using BakersDozen.Customers.Core.Entities;
using BakersDozen.Customers.Core.Exceptions;
using BakersDozen.Customers.Infrastructure;
using BakersDozen.Customers.Serverless.ViewModels;
using BakersDozen.SharedKernel;

using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

namespace BakersDozen.Customers.Serverless.CustomerEndpoints
{
    public class Create
    {
	    private readonly ICustomerRepository _customerRepository;

	    public Create()
	    {
		    var serviceCollection = new ServiceCollection()
			    .AddLogging()
			    .ConfigureDynamoDb();

		    var serviceProvider = serviceCollection.BuildServiceProvider();

		    this._customerRepository = serviceProvider.GetRequiredService<ICustomerRepository>();
	    }

	    public async Task<APIGatewayProxyResponse> Execute(
		    APIGatewayProxyRequest request,
		    ILambdaContext context)
	    {
		    var customerDto = JsonConvert.DeserializeObject<CreateCustomerDTO>(request.Body);

		    try
		    {
				var customer = new Customer()
				{
					EmailAddress = customerDto.EmailAddress,
					FirstName = customerDto.FirstName,
					LastName = customerDto.LastName,
					Username = customerDto.Username
				};

				customer.AddAddress(
					customerDto.AddressName,
					customerDto.AddressLine1,
					customerDto.Town,
					customerDto.Postcode,
					customerDto.Country);

			    var created = await this._customerRepository.CreateAsync(customer).ConfigureAwait(false);

			    if (created != null)
			    {
				    return new APIGatewayProxyResponse
				    {
					    StatusCode = 200,
					    Body = JsonConvert.SerializeObject(new ApiResponse<Customer>(created))
				    };
			    }
			    else
			    {
				    return new APIGatewayProxyResponse
				    {
					    StatusCode = 400,
					    Body = JsonConvert.SerializeObject(new ApiResponse<Customer>(null, "Unhandled failure"))
				    };
			    }
		    }
		    catch (CustomerExistsException)
		    {
			    return new APIGatewayProxyResponse
			    {
				    StatusCode = 400,
				    Body = JsonConvert.SerializeObject(new ApiResponse<Customer>(null, "Customer already exists"))
			    };
		    }
	    }
    }
}
