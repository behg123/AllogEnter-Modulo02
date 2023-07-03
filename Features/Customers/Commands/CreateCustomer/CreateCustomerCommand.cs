using System.ComponentModel.DataAnnotations;
using MediatR;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.Customers.Commands.CreateCustomer;

// IRequest<> transforma a classe em uma Mensagem
// CreateCustomerDto é o tipo que este comando espera receber de volta
public class CreateCustomerCommand : IRequest<CreateCustomerCommandResponse>
{
    public string Name {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
}

