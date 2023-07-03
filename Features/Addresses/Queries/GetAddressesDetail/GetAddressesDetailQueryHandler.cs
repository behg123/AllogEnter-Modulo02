using AutoMapper;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Addresses.Queries.GetAddressesDetail;

public class GetAddressesDetailQueryHandler : IRequestHandler<GetAddressesDetailQuery, IEnumerable<GetAddressesDetailDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetAddressesDetailQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAddressesDetailDto>> Handle(GetAddressesDetailQuery request, CancellationToken cancellationToken)
    {
        var addressesFromDatabase = await _customerRepository.GetAddressesAsync(request.CustomerId);
        return _mapper.Map<IEnumerable<GetAddressesDetailDto>>(addressesFromDatabase);
    }
}