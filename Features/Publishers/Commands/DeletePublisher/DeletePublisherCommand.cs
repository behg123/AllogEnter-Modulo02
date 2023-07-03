using MediatR;

namespace Univali.Api.Features.Publishers.Commands.DeletePublisher;

public class DeletePublisherCommand : IRequest<bool>
{
    public int PublisherId {get;set;}

    public DeletePublisherCommand(int publisherId)
    {
        PublisherId = publisherId;
    }
}