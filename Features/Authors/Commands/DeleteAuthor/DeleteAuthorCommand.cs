using MediatR;

namespace Univali.Api.Features.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommand : IRequest<bool>
{
    public int AuthorId { get; set; } 
}