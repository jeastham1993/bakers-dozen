using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
    public class Get
    {
	    private readonly ICustomerRepository _customerRepository;

	    public Get()
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
		    try
		    {
			    var foundCustomer = await this._customerRepository
				                        .RetrieveAsync(HttpUtility.HtmlDecode(request.PathParameters["username"]))
				                        .ConfigureAwait(false);

			    if (foundCustomer != null)
			    {
				    return new APIGatewayProxyResponse
				    {
					    StatusCode = 200,
					    Body = JsonConvert.SerializeObject(new ApiResponse<Customer>(foundCustomer))
				    };
			    }
			    else
			    {
				    return new APIGatewayProxyResponse
				    {
					    StatusCode = 404,
					    Body = JsonConvert.SerializeObject(new ApiResponse<Customer>(null, "Customer not found"))
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
