using Univali.Api.Entities;
using Univali.Api.Models;


namespace Univali.Api.Features.Customers.Queries.GetCustomerWithAddressesDetail;


public class GetCustomerWithAddressesDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public List<AddressDto> Addresses { get; set; } = new();
}

