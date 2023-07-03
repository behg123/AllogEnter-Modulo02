using AutoMapper;
using MediatR;
using Univali.Api.Repositories;
using FluentValidation;

namespace Univali.Api.Features.Publishers.Commands.UpdatePublisher;

public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, UpdatePublisherCommandResponse>
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdatePublisherCommand> _validator;

    public UpdatePublisherCommandHandler(IPublisherRepository publisherRepository, IMapper mapper, IValidator<UpdatePublisherCommand> validator)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
        _validator = validator;
    }
    public async Task<UpdatePublisherCommandResponse> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
    {

        UpdatePublisherCommandResponse updatePublisherCommandResponse = new();

        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.ToDictionary())
            {
                updatePublisherCommandResponse.Errors.Add(error.Key, error.Value);
            }

            updatePublisherCommandResponse.IsSuccess = false;
            return updatePublisherCommandResponse;
        }        
        System.Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA2AAA");

        var publisherEntity = await _publisherRepository.GetPublisherByIdAsync(request.Id);

        if (publisherEntity == null)
        {
            updatePublisherCommandResponse.IsSuccess = false;
            return updatePublisherCommandResponse;
        }

        _mapper.Map(request, publisherEntity);

        await _publisherRepository.SaveChangesAsync();

        return updatePublisherCommandResponse;
    }

}