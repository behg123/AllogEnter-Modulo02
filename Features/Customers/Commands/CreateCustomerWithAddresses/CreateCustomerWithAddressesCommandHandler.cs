using AutoMapper;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Univali.Api.Entities;
using Univali.Api.Features.Customers.Commands.CreateCustomer;
using Univali.Api.Features.Customers.Commands.CreateCustomerWithAddresses;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Commands.CreateCustomerWithAddresses
{
    public class CreateCustomerWithAddressesCommandHandler : IRequestHandler<CreateCustomerWithAddressesCommand, CreateCustomerWithAddressesCommandResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCustomerWithAddressesCommand> _validator;

        public CreateCustomerWithAddressesCommandHandler(ICustomerRepository customerRepository, IMapper mapper, IValidator<CreateCustomerWithAddressesCommand> validator)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<CreateCustomerWithAddressesCommandResponse> Handle(CreateCustomerWithAddressesCommand request, CancellationToken cancellationToken)
        {
            var createCustomerCommandResponse = new CreateCustomerWithAddressesCommandResponse();

            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.ToDictionary())
                {
                    createCustomerCommandResponse.Errors.Add(error.Key, error.Value);
                }

                createCustomerCommandResponse.IsSuccess = false;
                return createCustomerCommandResponse;
            }

            var customerEntity = _mapper.Map<Customer>(request);

            _customerRepository.AddCustomer(customerEntity);
            await _customerRepository.SaveChangesAsync();

            createCustomerCommandResponse.Customer = _mapper.Map<CreateCustomerWithAddressesDto>(customerEntity);

            return createCustomerCommandResponse;
        }
    }
}
