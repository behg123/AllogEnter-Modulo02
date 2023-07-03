namespace Univali.Api.Features.Publishers.Commands.UpdatePublisher;

public class UpdatePublisherCommandResponse
{
    public bool IsSuccess;

    public Dictionary<string, string[]> Errors { get; set; }

    public UpdatePublisherCommandResponse()
    {
        IsSuccess = true;
        Errors = new Dictionary<string, string[]>();
    }
}