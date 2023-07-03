namespace Univali.Api.Models;

public class AuthorForUpdateDto
{
    public int AuthorId { get; set; }
    public string Name {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
}