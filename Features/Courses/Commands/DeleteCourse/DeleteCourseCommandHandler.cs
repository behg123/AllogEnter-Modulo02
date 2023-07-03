using AutoMapper;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Courses.Commands.DeleteCourse;

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, bool>
{
    private readonly IPublisherRepository _publisherRepository;

    public DeleteCourseCommandHandler(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        if(!await _publisherRepository.PublisherExistsAsync(request.PublisherId)) return false;
        
        _publisherRepository.DeleteCourse(request.CourseId);
        
        return await _publisherRepository.SaveChangesAsync();
    }
}