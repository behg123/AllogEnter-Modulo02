using Univali.Api.Features.Customers.Commands.CreateCustomer;

namespace Univali.Api.Features.Customers.Commands.UpdateCustomerWithAddresses;

public class UpdateCustomerWithAddressesCommandResponse
{
    public bool IsSuccess;
    public bool Exist { get; set; }

    public Dictionary<string, string[]> Errors {get; set;}
    public CreateCustomerDto Customer {get; set;} = default!;

    public UpdateCustomerWithAddressesCommandResponse()
    {
        Exist = true;
        IsSuccess = true;
        Errors = new Dictionary<string, string[]>();
    }
}