using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    public class Update
    {
	    private readonly ICustomerRepository _customerRepository;

	    public Update()
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
		    var customer = JsonConvert.DeserializeObject<Customer>(request.Body);

		    try
		    {
			    var created = await this._customerRepository.UpdateAsync(customer).ConfigureAwait(false);

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
		    catch (CustomerNotFoundException)
		    {
			    return new APIGatewayProxyResponse
			    {
				    StatusCode = 400,
				    Body = JsonConvert.SerializeObject(new ApiResponse<Customer>(null, "Customer not found"))
			    };
		    }
	    }
    }
}
