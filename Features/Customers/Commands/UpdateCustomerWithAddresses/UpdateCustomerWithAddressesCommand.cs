using System.ComponentModel.DataAnnotations;
using MediatR;
using Univali.Api.Models;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.Customers.Commands.UpdateCustomerWithAddresses;

// IRequest<> transforma a classe em uma Mensagem
// CreateCustomerDto é o tipo que este comando espera receber de volta
public class UpdateCustomerWithAddressesCommand : IRequest<UpdateCustomerWithAddressesCommandResponse>
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
    public List<AddressForUpdateDto> Addresses { get; set; } = new();
}

