using AutoMapper;
using Univali.Api.Repositories;
using MediatR;
using FluentValidation;
using Univali.Api.Features.Customers.Commands.CreateCustomer;


namespace Univali.Api.Features.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, CreateAuthorCommandResponse>
{
    private readonly IPublisherRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateAuthorCommand> _validator;

    public CreateAuthorCommandHandler(IPublisherRepository authorRepository, IMapper mapper, IValidator<CreateAuthorCommand> validator)
    {
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<CreateAuthorCommandResponse> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        CreateAuthorCommandResponse createAuthorCommandResponse = new();

        var validationResult = _validator.Validate(request);

        if(!validationResult.IsValid)
        {
            foreach(var error in validationResult.ToDictionary())
            {
                createAuthorCommandResponse.Errors.Add(error.Key, error.Value);
            }

            createAuthorCommandResponse.IsSuccess = false;
            return createAuthorCommandResponse;
        }

        var authorEntity = _mapper.Map<Entities.Author>(request);

        _authorRepository.AddAuthor(authorEntity);

        await _authorRepository.SaveChangesAsync();

        createAuthorCommandResponse.Author = _mapper.Map<CreateAuthorDto>(authorEntity);

        return createAuthorCommandResponse;
    }
}