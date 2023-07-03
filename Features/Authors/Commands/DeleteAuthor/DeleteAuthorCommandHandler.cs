using AutoMapper;
using Univali.Api.Repositories;
using MediatR;

namespace Univali.Api.Features.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
{
    private readonly IPublisherRepository _authorRepository;
    private readonly IMapper _mapper;

    public DeleteAuthorCommandHandler(IPublisherRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorEntity = await _authorRepository.GetAuthorByIdAsync(request.AuthorId);

        if(authorEntity == null) return false;

        _authorRepository.DeleteAuthor(authorEntity);

        return await _authorRepository.SaveChangesAsync();

    }
}