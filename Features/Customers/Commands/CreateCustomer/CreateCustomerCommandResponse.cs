namespace Univali.Api.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandResponse
{
    public bool IsSuccess;
    public Dictionary<string, string[]> Errors {get; set;}
    public CreateCustomerDto Customer {get; set;} = default!;

    public CreateCustomerCommandResponse()
    {
        IsSuccess = true;
        Errors = new Dictionary<string, string[]>();
    }
}