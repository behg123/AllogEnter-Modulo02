using Univali.Api.Models;

namespace Univali.Api.Features.Courses.Queries.GetCourseDetail;

public class GetCourseDetailDto
{
    public int CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}