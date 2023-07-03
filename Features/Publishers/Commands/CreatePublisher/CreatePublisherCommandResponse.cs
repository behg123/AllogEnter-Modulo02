namespace Univali.Api.Features.Publishers.Commands.CreatePublisher;

public class CreatePublisherCommandResponse
{
    public bool IsSuccess;

    public Dictionary<string, string[]> Errors { get; set; }

    public CreatePublisherDto Publisher { get; set; } = default!;

    public CreatePublisherCommandResponse()
    {
        IsSuccess = true;
        Errors = new Dictionary<string, string[]>();
    }
}