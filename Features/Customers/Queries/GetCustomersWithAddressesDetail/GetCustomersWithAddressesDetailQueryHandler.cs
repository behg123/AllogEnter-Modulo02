using AutoMapper;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Queries.GetCustomersWithAddressesDetail;

public class GetCustomerWithAddressesDetailQueryHandler : IRequestHandler<GetCustomersWithAddressesDetailQuery, IEnumerable<GetCustomersWithAddressesDetailDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerWithAddressesDetailQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetCustomersWithAddressesDetailDto>> Handle(GetCustomersWithAddressesDetailQuery request, CancellationToken cancellationToken)
    {
        var customerFromDatabase = await _customerRepository.GetCustomersWithAddressesAsync();
        return _mapper.Map<IEnumerable<GetCustomersWithAddressesDetailDto>>(customerFromDatabase);
    }
}