using System;
using System.Linq;
using System.Threading.Tasks;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

using BakersDozen.Customers.Core.Entities;
using BakersDozen.Customers.Core.Exceptions;
using BakersDozen.Customers.Infrastructure.DataAccess.Extensions;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace BakersDozen.Customers.Infrastructure.DataAccess
{
    public class CustomerRepositoryDynamoDb : ICustomerRepository
    {
	    private readonly ILogger<CustomerRepositoryDynamoDb> _logger;

	    private readonly AmazonDynamoDBClient _client;

	    public CustomerRepositoryDynamoDb(
		    ILogger<CustomerRepositoryDynamoDb> logger,
		    AmazonDynamoDBClient client)
	    {
		    this._logger = logger;
		    this._client = client;
	    }

	    /// <inheritdoc />
	    public async Task<Customer> CreateAsync(
		    Customer customer)
	    {
			this._logger.LogInformation("Attempting creation of Customer");

			try
			{
				await this._client.PutItemAsync(
					DynamoDbConstants.TableName,
					customer.AsItem()).ConfigureAwait(false);

				return customer;
			}
			catch (TransactionCanceledException ex)
			{
				this._logger.LogError(ex, "Failure creating customer");
				
				return null;
			}
			catch (ConditionalCheckFailedException ex)
			{
				this._logger.LogError(ex, "Customer already exists");

				throw new CustomerExistsException($"Customer already exists with email {customer.EmailAddress}");
			}
	    }

	    /// <inheritdoc />
	    public async Task<Customer> RetrieveAsync(
		    string emailAddress)
	    {
		    var customer = new Customer()
		    {
			    EmailAddress = emailAddress
		    };

		    var getItemRequest = new GetItemRequest() { TableName = DynamoDbConstants.TableName, Key = customer.GetKeys() };

		    var getResult = await this._client.GetItemAsync(getItemRequest).ConfigureAwait(false);

		    if (getResult.Item != null)
		    {
			    var customerData = getResult.Item.FirstOrDefault(p => p.Key == "Data");

			    var addressDetails = customerData.Value.M.FirstOrDefault(p => p.Key == "addresses");

			    customerData.Value.M.Remove("addresses");

			    customer = JsonConvert.DeserializeObject<Customer>(Document.FromAttributeMap(customerData.Value.M).ToJson());

			    foreach (var address in addressDetails.Value.M)
			    {
				    customer.AddAddress(
					    address.Key,
					    address.Value.M["AddressLine1"].S,
					    address.Value.M["Town"].S,
					    address.Value.M["Postcode"].S,
					    address.Value.M["CountryCode"].S);
			    }

			    return customer;
		    }
		    else
		    {
			    this._logger.LogWarning($"Customer search executed but not found {emailAddress}");

			    throw new CustomerNotFoundException("Customer not found");
		    }
	    }

	    /// <inheritdoc />
	    public async Task<Customer> UpdateAsync(
		    Customer customer)
	    {
		    throw new NotImplementedException();
	    }
    }
}
