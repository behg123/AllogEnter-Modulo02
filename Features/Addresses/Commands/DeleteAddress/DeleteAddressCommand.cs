using System.ComponentModel.DataAnnotations;
using MediatR;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.Addresses.Commands.DeleteAddress;

// IRequest<> transforma a classe em uma Mensagem
// DeleteCustomerDto é o tipo que este comando espera receber de volta
public class DeleteAddressCommand : IRequest<bool>
{
    public int Id {get; set;}
    public int CustomerId {get; set;}
}