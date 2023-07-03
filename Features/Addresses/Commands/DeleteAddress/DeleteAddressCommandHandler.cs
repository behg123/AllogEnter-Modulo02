using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Addresses.Commands.DeleteAddress;

// O primeiro parâmetro é o tipo da mensagem
// O segundo parâmetro é o tipo que se espera receber.
public class DeleteAddressCommandHandler: IRequestHandler<DeleteAddressCommand, bool>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public DeleteAddressCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        if(! await _customerRepository.CustomerExistsAsync(request.CustomerId)) return false;

        var addressFromDataBase = await _customerRepository.GetAddressByIdAsync(request.CustomerId, request.Id);
        
        if(addressFromDataBase ==  null)
        {
            return false;
        }
        
        _customerRepository.RemoveAddress(addressFromDataBase);

        return await _customerRepository.SaveChangesAsync();
    }
}