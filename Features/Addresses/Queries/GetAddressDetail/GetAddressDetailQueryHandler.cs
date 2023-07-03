using AutoMapper;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Addresses.Queries.GetAddressDetail;

public class GetAddressDetailQueryHandler : IRequestHandler<GetAddressDetailQuery, GetAddressDetailDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetAddressDetailQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<GetAddressDetailDto> Handle(GetAddressDetailQuery request, CancellationToken cancellationToken)
    {
        var addressFromDatabase = await _customerRepository.GetAddressByIdAsync(request.CustomerId, request.Id);
        return _mapper.Map<GetAddressDetailDto>(addressFromDatabase);
    }
}