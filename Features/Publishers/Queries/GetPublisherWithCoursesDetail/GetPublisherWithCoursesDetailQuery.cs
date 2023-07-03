using MediatR;

namespace Univali.Api.Features.Publishers.Queries.GetPublisherWithCoursesDetail;

public class GetPublisherWithCoursesDetailQuery : IRequest<GetPublisherWithCoursesDetailDto>
{
    public int Id {get; set;}
}