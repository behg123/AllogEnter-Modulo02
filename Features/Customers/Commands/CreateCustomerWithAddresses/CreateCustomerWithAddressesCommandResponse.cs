using Univali.Api.Features.Customers.Commands.CreateCustomerWithAddresses;

namespace Univali.Api.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerWithAddressesCommandResponse
{
    public bool IsSuccess;
    public Dictionary<string, string[]> Errors {get; set;}
    public CreateCustomerWithAddressesDto Customer {get; set;} = default!;

    public CreateCustomerWithAddressesCommandResponse()
    {
        IsSuccess = true;
        Errors = new Dictionary<string, string[]>();
    }
}