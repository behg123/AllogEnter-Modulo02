using MediatR;

namespace Univali.Api.Features.Customers.Queries.GetCustomersWithAddressesDetail;

public class GetCustomersWithAddressesDetailQuery : IRequest<IEnumerable<GetCustomersWithAddressesDetailDto>>
{
}