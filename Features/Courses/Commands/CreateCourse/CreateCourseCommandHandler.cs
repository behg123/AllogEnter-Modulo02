using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Courses.Commands.CreateCourse;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CreateCourseCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IPublisherRepository _publisherRepository;
    private readonly IValidator<CreateCourseCommand> _validator;

    public CreateCourseCommandHandler(IMapper mapper, IPublisherRepository publisherRepository, IValidator<CreateCourseCommand> validator)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<CreateCourseCommandResponse> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        CreateCourseCommandResponse createCourseCommandResponse = new();

        var validationResult = _validator.Validate(request);

        if(!validationResult.IsValid)
        {
            foreach(var error in validationResult.ToDictionary())
            {
                createCourseCommandResponse.Errors.Add(error.Key, error.Value);
            }

            createCourseCommandResponse.IsSuccess = false;
            return createCourseCommandResponse;
        }

        if(!await _publisherRepository.PublisherExistsAsync(request.PublisherId)) 
        {
            createCourseCommandResponse.Course = null!;
            return createCourseCommandResponse;
        }

        var authorIds = request.Authors.Select(a => a.AuthorId);
        var existingAuthors = await _publisherRepository.GetAuthorsById(authorIds);

        var courseEntity = _mapper.Map<Course>(request);                                                                                                                                                                                                                                                                                                                                                                                                                                                   
        courseEntity.Authors = existingAuthors;

        await _publisherRepository.AddCourseAsync(request.PublisherId, courseEntity);
        
        await _publisherRepository.SaveChangesAsync();

        createCourseCommandResponse.Course = _mapper.Map<CreateCourseDto>(courseEntity);

        return createCourseCommandResponse;
    }
}

//  System.Console.WriteLine("================================================================");
//  System.Console.WriteLine(JsonSerializer.Serialize(courseEntity, new JsonSerializerOptions{WriteIndented = true}));
//     System.Console.WriteLine("================================================================");
