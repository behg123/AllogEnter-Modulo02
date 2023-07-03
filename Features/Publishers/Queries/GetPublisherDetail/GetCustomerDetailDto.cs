using Univali.Api.Entities;
using Univali.Api.Models;


namespace Univali.Api.Features.Publishers.Queries.GetPublisherDetail;


public class GetPublisherDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
}

