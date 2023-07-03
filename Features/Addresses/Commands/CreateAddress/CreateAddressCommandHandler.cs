using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Addresses.Commands.CreateAddress;

// O primeiro parâmetro é o tipo da mensagem
// O segundo parâmetro é o tipo que se espera receber.
public class CreateAddressCommandHandler: IRequestHandler<CreateAddressCommand, CreateAddressCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateAddressCommand> _validator;


    public CreateAddressCommandHandler(ICustomerRepository customerRepository, IMapper mapper, IValidator<CreateAddressCommand> validator)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<CreateAddressCommandResponse> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        CreateAddressCommandResponse createAddressCommandResponse = new();

        var validationResult = _validator.Validate(request);

        if(!validationResult.IsValid)
        {
            foreach(var error in validationResult.ToDictionary())
            {
                createAddressCommandResponse.Erros.Add(error.Key, error.Value);
            }
            createAddressCommandResponse.IsSuccess = false;
            return createAddressCommandResponse;
        }

        if(!await _customerRepository.CustomerExistsAsync(request.CustomerId)) return null!;

        var addressEntity = _mapper.Map<Address>(request);
        _customerRepository.AddAddress(addressEntity);
        await _customerRepository.SaveChangesAsync();
        createAddressCommandResponse.Address = _mapper.Map<CreateAddressDto>(addressEntity);

        return createAddressCommandResponse;
    }
}