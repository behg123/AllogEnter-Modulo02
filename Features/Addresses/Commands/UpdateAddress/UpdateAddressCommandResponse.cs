namespace Univali.Api.Features.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommandResponse
{
    public bool IsSuccess {get;set;}
    public Dictionary<string, string[]> Erros {get;set;}

    public UpdateAddressCommandResponse(){
        IsSuccess = true;
        Erros = new Dictionary<string, string[]>();
    }
}