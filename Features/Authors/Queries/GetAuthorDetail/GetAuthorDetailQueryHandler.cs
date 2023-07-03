namespace Univali.Api.Features.Authors.Queries.GetAuthorDetail;

using AutoMapper;
using Univali.Api.Repositories;
using MediatR;

public class GetAuthorDetailQueryHandler : IRequestHandler<GetAuthorDetailQuery, GetAuthorDetailDto>
{
    private readonly IPublisherRepository _authorRepository;
    private readonly IMapper _mapper;

    public GetAuthorDetailQueryHandler(IPublisherRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<GetAuthorDetailDto> Handle(GetAuthorDetailQuery request, CancellationToken cancellationToken)
    {
        var authorEntity = await _authorRepository.GetAuthorByIdAsync(request.AuthorId);

        if(authorEntity == null) return null!;

        var authorToReturn = _mapper.Map<GetAuthorDetailDto>(authorEntity);

        return authorToReturn;
    }
}