using System.ComponentModel.DataAnnotations;
using MediatR;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.Addresses.Commands.CreateAddress;

// IRequest<> transforma a classe em uma Mensagem
// CreateCustomerDto é o tipo que este comando espera receber de volta
public class CreateAddressCommand : IRequest<CreateAddressCommandResponse>
{
    public string Street {get; set;} = string.Empty;
    public string City {get; set;} = string.Empty;
    public int CustomerId {get; set;}
}

