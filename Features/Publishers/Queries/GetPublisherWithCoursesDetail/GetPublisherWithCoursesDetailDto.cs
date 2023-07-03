using Univali.Api.Entities;
using Univali.Api.Models;


namespace Univali.Api.Features.Publishers.Queries.GetPublisherWithCoursesDetail;


public class GetPublisherWithCoursesDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public List<CourseDto> Courses { get; set; } = new();
}

