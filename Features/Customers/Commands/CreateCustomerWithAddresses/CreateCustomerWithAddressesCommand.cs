using System.ComponentModel.DataAnnotations;
using MediatR;
using Univali.Api.Features.Customers.Commands.CreateCustomer;
using Univali.Api.Models;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.Customers.Commands.CreateCustomerWithAddresses;

// IRequest<> transforma a classe em uma Mensagem
// CreateCustomerDto é o tipo que este comando espera receber de volta
public class CreateCustomerWithAddressesCommand : IRequest<CreateCustomerWithAddressesCommandResponse>
{
    public string Name {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
    public List<AddressForCreationDto> Addresses { get; set; } = new();
}

