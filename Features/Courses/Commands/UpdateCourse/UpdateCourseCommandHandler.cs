using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Features.Courses.Commands.UpdateCourse;
using Univali.Api.Repositories;

namespace CoursesPlatform.Api.Features.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, UpdateCourseCommandResponse>
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateCourseCommand> _validator;

    public UpdateCourseCommandHandler(IMapper mapper, IPublisherRepository publisherRepository, IValidator<UpdateCourseCommand> validator)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<UpdateCourseCommandResponse> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        UpdateCourseCommandResponse updateCourseCommandResponse = new();

        var validationResult = _validator.Validate(request);

        if(!validationResult.IsValid)
        {
            foreach(var error in validationResult.ToDictionary())
            {
                updateCourseCommandResponse.Errors.Add(error.Key, error.Value);
            }

            updateCourseCommandResponse.IsSuccess = false;
            return updateCourseCommandResponse;
        }

        if(!await _publisherRepository.PublisherExistsAsync(request.PublisherId))
        {
            updateCourseCommandResponse.Exist = false;
            return updateCourseCommandResponse;
        }

        var courseEntity = await _publisherRepository.GetCourseByIdAsync(request.CourseId);
        
        if(courseEntity == null)
        {
            updateCourseCommandResponse.Exist = false;
            return updateCourseCommandResponse;
        }

        _mapper.Map(request, courseEntity);

        updateCourseCommandResponse.Exist = await _publisherRepository.SaveChangesAsync();

        return updateCourseCommandResponse;
    }
}