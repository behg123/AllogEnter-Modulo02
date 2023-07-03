namespace Univali.Api.Features.Courses.Commands.CreateCourse;

public class CreateCourseCommandResponse
{
    public bool IsSuccess;
    public Dictionary<string, string[]> Errors { get; set; }
    public CreateCourseDto Course { get; set; } = default!;

    public CreateCourseCommandResponse()
    {
        IsSuccess = true;
        Errors = new Dictionary<string, string[]>();
    }
}