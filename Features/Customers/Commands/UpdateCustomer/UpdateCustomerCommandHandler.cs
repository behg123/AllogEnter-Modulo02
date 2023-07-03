using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Features.Customers.Commands.CreateCustomer;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Commands.UpdateCustomer;

// O primeiro parâmetro é o tipo da mensagem
// O segundo parâmetro é o tipo que se espera receber.
public class UpdateCustomerCommandHandler: IRequestHandler<UpdateCustomerCommand, UpdateCustomerCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateCustomerCommand> _validator;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper, IValidator<UpdateCustomerCommand> validator)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<UpdateCustomerCommandResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        UpdateCustomerCommandResponse updateCustomerCommandResponse = new();

        var validationResult = _validator.Validate(request);

        if(!validationResult.IsValid)
        {
            foreach(var error in validationResult.ToDictionary())
            {
                updateCustomerCommandResponse.Errors.Add(error.Key, error.Value);
            }

            updateCustomerCommandResponse.IsSuccess = false;
            return updateCustomerCommandResponse;
        }

        var customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(request.Id);

        if (customerFromDatabase == null)
        {
            updateCustomerCommandResponse.Exist = false;
            return updateCustomerCommandResponse;
        }

        _mapper.Map(request, customerFromDatabase);
        
        await _customerRepository.SaveChangesAsync();

        return updateCustomerCommandResponse;
    }
}