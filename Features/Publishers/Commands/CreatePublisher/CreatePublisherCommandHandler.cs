using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;
using FluentValidation;

namespace Univali.Api.Features.Publishers.Commands.CreatePublisher;

public class CreatePublisherCommandHandler: IRequestHandler<CreatePublisherCommand, CreatePublisherCommandResponse>
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;
    private readonly IValidator <CreatePublisherCommand> _validator;

    public CreatePublisherCommandHandler(IPublisherRepository publisherRepository, IMapper mapper, IValidator<CreatePublisherCommand> validator)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<CreatePublisherCommandResponse> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
    {
        CreatePublisherCommandResponse createPublisherCommandRespose =  new();

        var validationResult = _validator.Validate(request);

        if(!validationResult.IsValid)
        {
            foreach(var error in validationResult.ToDictionary())
            {
                createPublisherCommandRespose.Errors.Add(error.Key, error.Value);
            }

            createPublisherCommandRespose.IsSuccess = false;
            return createPublisherCommandRespose;
        }

        var publisherEntity = _mapper.Map<Publisher>(request);

        _publisherRepository.AddPublisher(publisherEntity);
        await _publisherRepository.SaveChangesAsync();

        createPublisherCommandRespose.Publisher = _mapper.Map<CreatePublisherDto>(publisherEntity);
        return createPublisherCommandRespose;
    }
}