namespace Univali.Api.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommandResponse
{
    public bool IsSuccess {get;set;}
    public Dictionary<string, string[]> Erros { get; set;}
    public CreateAddressDto Address { get; set;} = default!;

    public CreateAddressCommandResponse()
    {
        IsSuccess = true;
        Erros = new Dictionary<string, string[]>();
    }
}