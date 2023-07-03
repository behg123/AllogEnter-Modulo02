using AutoMapper;
using Univali.Api.Repositories;
using MediatR;

namespace Univali.Api.Features.Courses.Queries.GetCourseWithAuthorsDetail;

public class GetCourseWithAuthorsDetailQueryHandler : IRequestHandler<GetCourseWithAuthorsDetailQuery, GetCourseWithAuthorsDetailDto>
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public GetCourseWithAuthorsDetailQueryHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<GetCourseWithAuthorsDetailDto> Handle(GetCourseWithAuthorsDetailQuery request, CancellationToken cancellationToken)
    {
        if(!await _publisherRepository.PublisherExistsAsync(request.PublisherId)) return null!;

        var courseFromDatabase = await _publisherRepository.GetCourseWithAuthorsByIdAsync(request.CourseId);

        if(courseFromDatabase == null) return null!;

        return _mapper.Map<GetCourseWithAuthorsDetailDto>(courseFromDatabase);
    }
}