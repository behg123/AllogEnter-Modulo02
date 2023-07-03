using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Addresses.Commands.UpdateAddress;

// O primeiro parâmetro é o tipo da mensagem
// O segundo parâmetro é o tipo que se espera receber.
public class UpdateAddressCommandHandler: IRequestHandler<UpdateAddressCommand, UpdateAddressCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateAddressCommand> _validator;

    public UpdateAddressCommandHandler(ICustomerRepository customerRepository, IMapper mapper, IValidator<UpdateAddressCommand> validator)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<UpdateAddressCommandResponse> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        UpdateAddressCommandResponse updateAddressCommandResponse = new();

        var validateResult = _validator.Validate(request);

        if(!validateResult.IsValid)
        {
            foreach(var error in validateResult.ToDictionary())
            {
                updateAddressCommandResponse.Erros.Add(error.Key, error.Value);
            }
            updateAddressCommandResponse.IsSuccess = false;
            return updateAddressCommandResponse;
        } 

        if(! await _customerRepository.CustomerExistsAsync(request.CustomerId)) return null!;

        var addressFromDatabase = await _customerRepository.GetAddressByIdAsync(request.CustomerId, request.Id);

        if(addressFromDatabase == null) return null!;

        _mapper.Map(request, addressFromDatabase);

        await _customerRepository.SaveChangesAsync();
        
        return updateAddressCommandResponse;
    }
}