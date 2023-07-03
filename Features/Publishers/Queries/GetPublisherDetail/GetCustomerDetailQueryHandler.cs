using AutoMapper;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Publishers.Queries.GetPublisherDetail;

public class GetPublisherDetailQueryHandler : IRequestHandler<GetPublisherDetailQuery, GetPublisherDetailDto>
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public GetPublisherDetailQueryHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<GetPublisherDetailDto> Handle(GetPublisherDetailQuery request, CancellationToken cancellationToken)
    {
        var publisherFromDatabase = await _publisherRepository.GetPublisherByIdAsync(request.Id);
        return _mapper.Map<GetPublisherDetailDto>(publisherFromDatabase);
    }
}