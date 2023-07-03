namespace Univali.Api.Features.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandResponse
{
    public bool IsSuccess;
    public Dictionary<string, string[]> Errors { get; set; }
    public CreateAuthorDto Author { get; set; } = default!;

    public CreateAuthorCommandResponse()
    {
        IsSuccess = true;
        Errors = new Dictionary<string, string[]>();
    }
}