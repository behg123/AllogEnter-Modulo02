using AutoMapper;
using Univali.Api.Repositories;
using MediatR;
using Univali.Api.Entities;
using FluentValidation;


namespace Univali.Api.Features.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, UpdateAuthorCommandResponse>
{
    private readonly IMapper _mapper; 
    private readonly IPublisherRepository _authorRepository;
    private readonly IValidator<UpdateAuthorCommand> _validator;

    public UpdateAuthorCommandHandler(IMapper mapper, IPublisherRepository authorRepository, IValidator<UpdateAuthorCommand> validator)
    {
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<UpdateAuthorCommandResponse> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        UpdateAuthorCommandResponse updateAuthorCommandResponse = new();

        var validationResult = _validator.Validate(request);

        if(!validationResult.IsValid)
        {
            foreach(var error in validationResult.ToDictionary())
            {
                updateAuthorCommandResponse.Errors.Add(error.Key, error.Value);
            }

            updateAuthorCommandResponse.IsSuccess = false;
            return updateAuthorCommandResponse;
        }



        var authorEntity = await _authorRepository.GetAuthorByIdAsync(request.AuthorId); 

        if(authorEntity == null)
        {
            updateAuthorCommandResponse.Exist = false;
            return updateAuthorCommandResponse;
        }                    

        _mapper.Map(request, authorEntity);

        updateAuthorCommandResponse.Exist = await _authorRepository.SaveChangesAsync();

        return updateAuthorCommandResponse;
    }
}