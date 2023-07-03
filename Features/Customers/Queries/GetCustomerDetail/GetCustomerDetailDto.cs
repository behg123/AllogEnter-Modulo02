using Univali.Api.Entities;
using Univali.Api.Models;


namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;


public class GetCustomerDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
}

