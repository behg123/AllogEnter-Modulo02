namespace Univali.Api.Entities;

public class Author
{
    public int AuthorId {get; set;}
    public string Name {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
    public List<Course> Courses {get; set;} = new();
}