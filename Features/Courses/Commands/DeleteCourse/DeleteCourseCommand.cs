using MediatR;

namespace Univali.Api.Features.Courses.Commands.DeleteCourse;

public class DeleteCourseCommand : IRequest<bool>
{
    public int CourseId { get; set; }
    public int PublisherId { get; set; }
}