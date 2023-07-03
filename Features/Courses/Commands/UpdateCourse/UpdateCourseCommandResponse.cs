namespace Univali.Api.Features.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandResponse
{
    public bool IsSuccess;
    public Dictionary<string, string[]> Errors { get; set; }
    public bool Exist { get; set; }

    public UpdateCourseCommandResponse()
    {
        IsSuccess = true;
        Errors = new Dictionary<string, string[]>();
    }
}