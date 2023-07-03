namespace Univali.Api.Features.Authors.Commands.CreateAuthor;

public class CreateAuthorDto
{
    public int AuthorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
}