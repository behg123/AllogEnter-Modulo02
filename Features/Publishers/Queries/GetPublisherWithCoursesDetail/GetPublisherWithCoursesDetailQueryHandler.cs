using AutoMapper;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Publishers.Queries.GetPublisherWithCoursesDetail;

public class GetPublisherWithCoursesDetailQueryHandler : IRequestHandler<GetPublisherWithCoursesDetailQuery, GetPublisherWithCoursesDetailDto>
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public GetPublisherWithCoursesDetailQueryHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<GetPublisherWithCoursesDetailDto> Handle(GetPublisherWithCoursesDetailQuery request, CancellationToken cancellationToken)
    {
        var publisherFromDatabase = await _publisherRepository.GetPublisherWithCoursesByIdAsync(request.Id);

        return _mapper.Map<GetPublisherWithCoursesDetailDto>(publisherFromDatabase);
    }
}