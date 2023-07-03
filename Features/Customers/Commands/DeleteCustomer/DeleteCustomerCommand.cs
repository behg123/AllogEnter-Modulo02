using System.ComponentModel.DataAnnotations;
using MediatR;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.Customers.Commands.DeleteCustomer;

// IRequest<> transforma a classe em uma Mensagem
// DeleteCustomerDto é o tipo que este comando espera receber de volta
public class DeleteCustomerCommand : IRequest<bool>
{
    [Required(ErrorMessage = "You should fill out a Id")]
    public int Id {get; set;}
}