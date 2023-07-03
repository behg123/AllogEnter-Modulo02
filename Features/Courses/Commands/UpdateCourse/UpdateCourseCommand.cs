using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Univali.Api.Features.Courses.Commands.UpdateCourse;

public class UpdateCourseCommand : IRequest<UpdateCourseCommandResponse>
{
    public int CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    
    public int PublisherId { get; set; }
}