namespace Univali.Api.Features.Authors.Queries.GetAuthorDetail;

using MediatR;

public class GetAuthorDetailQuery : IRequest<GetAuthorDetailDto>
{
    public int AuthorId { get; set; }
}