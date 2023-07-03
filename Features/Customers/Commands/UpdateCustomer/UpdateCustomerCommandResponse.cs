using Univali.Api.Features.Customers.Commands.CreateCustomer;

namespace Univali.Api.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandResponse
{
    public bool IsSuccess;
    public bool Exist { get; set; }

    public Dictionary<string, string[]> Errors {get; set;}
    public CreateCustomerDto Customer {get; set;} = default!;

    public UpdateCustomerCommandResponse()
    {
        Exist = true;
        IsSuccess = true;
        Errors = new Dictionary<string, string[]>();
    }
}