namespace Univali.Api.Features.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandResponse
{
    public bool IsSuccess;
    public Dictionary<string, string[]> Errors { get; set; }
    public bool Exist { get; set; }
    
    public UpdateAuthorCommandResponse()
    {
        IsSuccess = true;
        Errors = new Dictionary<string, string[]>();
    }
}