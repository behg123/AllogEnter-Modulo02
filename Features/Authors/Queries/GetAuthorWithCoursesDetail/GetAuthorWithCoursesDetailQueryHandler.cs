namespace Univali.Api.Features.Authors.Queries.GetAuthorWithCoursesDetail;

using AutoMapper;
using Univali.Api.Repositories;
using MediatR;

public class GetAuthorWithCoursesDetailQueryHandler : IRequestHandler<GetAuthorWithCoursesDetailQuery, GetAuthorWithCoursesDetailDto>
{
    private readonly IPublisherRepository _authorRepository;
    private readonly IMapper _mapper;

    public GetAuthorWithCoursesDetailQueryHandler(IPublisherRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<GetAuthorWithCoursesDetailDto> Handle(GetAuthorWithCoursesDetailQuery request, CancellationToken cancellationToken)
    {
        var authorEntity = await _authorRepository.GetAuthorWithCoursesByIdAsync(request.AuthorId);

        if(authorEntity == null) return null!;

        var authorToReturn = _mapper.Map<GetAuthorWithCoursesDetailDto>(authorEntity);

        return authorToReturn;
    }
}