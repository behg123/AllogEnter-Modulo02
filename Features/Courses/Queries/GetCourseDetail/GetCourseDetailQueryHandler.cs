using AutoMapper;
using Univali.Api.Repositories;
using MediatR;

namespace Univali.Api.Features.Courses.Queries.GetCourseDetail;

public class GetCourseDetailQueryHandler : IRequestHandler<GetCourseDetailQuery, GetCourseDetailDto>
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public GetCourseDetailQueryHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<GetCourseDetailDto> Handle(GetCourseDetailQuery request, CancellationToken cancellationToken)
    {
        if(!await _publisherRepository.PublisherExistsAsync(request.PublisherId)) return null!;

        var courseFromDatabase = await _publisherRepository.GetCourseByIdAsync(request.CourseId);

        return _mapper.Map<GetCourseDetailDto>(courseFromDatabase);
    }
}