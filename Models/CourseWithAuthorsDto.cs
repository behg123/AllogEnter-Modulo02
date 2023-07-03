namespace Univali.Api.Models;

public class CourseWithAuthorsDto
{
    public int CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<AuthorDto> Authors {get; set;} = new();
}