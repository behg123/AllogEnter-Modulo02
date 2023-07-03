namespace Univali.Api.Features.Authors.Queries.GetAuthorWithCoursesDetail;

using MediatR;

public class GetAuthorWithCoursesDetailQuery : IRequest<GetAuthorWithCoursesDetailDto>
{
    public int AuthorId { get; set; }
}