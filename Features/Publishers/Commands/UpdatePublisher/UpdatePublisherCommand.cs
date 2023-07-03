using MediatR;

namespace Univali.Api.Features.Publishers.Commands.UpdatePublisher;

public  class UpdatePublisherCommand: IRequest<UpdatePublisherCommandResponse>
{
    public int Id {get; set;}
    public string Name { get; set; } = string.Empty;    
    public string CNPJ { get; set; } = string.Empty;
}