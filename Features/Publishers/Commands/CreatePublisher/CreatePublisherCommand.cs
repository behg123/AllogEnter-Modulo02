using MediatR;

namespace Univali.Api.Features.Publishers.Commands.CreatePublisher;

public class CreatePublisherCommand : IRequest<CreatePublisherCommandResponse>
{
    public string Name {get; set;} = string.Empty;
    public string CNPJ {get; set;} = string.Empty;
}

