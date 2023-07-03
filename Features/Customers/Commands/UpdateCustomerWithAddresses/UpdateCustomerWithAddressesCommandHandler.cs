using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Commands.UpdateCustomerWithAddresses;

// O primeiro parâmetro é o tipo da mensagem
// O segundo parâmetro é o tipo que se espera receber.
public class UpdateCustomerWithAddressesCommandHandler: IRequestHandler<UpdateCustomerWithAddressesCommand, UpdateCustomerWithAddressesCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateCustomerWithAddressesCommand> _validator;

    public UpdateCustomerWithAddressesCommandHandler(ICustomerRepository customerRepository, IMapper mapper, IValidator<UpdateCustomerWithAddressesCommand> validator)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<UpdateCustomerWithAddressesCommandResponse> Handle(UpdateCustomerWithAddressesCommand request, CancellationToken cancellationToken)
    {
        UpdateCustomerWithAddressesCommandResponse updateCustomerWithAddressesCommand = new();

        var validationResult = _validator.Validate(request);

        if(!validationResult.IsValid)
        {
            foreach(var error in validationResult.ToDictionary())
            {
                updateCustomerWithAddressesCommand.Errors.Add(error.Key, error.Value);
            }

            updateCustomerWithAddressesCommand.IsSuccess = false;
            return updateCustomerWithAddressesCommand;
        }

        var customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(request.Id);

        if (customerFromDatabase == null)
        {
            updateCustomerWithAddressesCommand.Exist = false;
            return updateCustomerWithAddressesCommand;
        }

        _mapper.Map(request, customerFromDatabase);
        
        await _customerRepository.SaveChangesAsync();

        return updateCustomerWithAddressesCommand;
    }
}