using AutoMapper;
using Univali.Api.Features.Courses.Queries.GetCourseDetail;
using Univali.Api.Models;
using Univali.Api.Entities;
using Univali.Api.Features.Courses.Commands.CreateCourse;
using Univali.Api.Features.Courses.Commands.UpdateCourse;
using Univali.Api.Features.Courses.Queries.GetCourseWithAuthorsDetail;


namespace Univali.Api.Profiles;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<Course, CourseDto>();
        CreateMap<Course, CreateCourseDto>();
        CreateMap<Course, GetCourseDetailDto>();
        CreateMap<Course, GetCourseWithAuthorsDetailDto>();
        CreateMap<CreateCourseCommand, Course>().ReverseMap();
        CreateMap<UpdateCourseCommand, Course>();
        CreateMap<CourseForUpdateDto, UpdateCourseCommand>();
    }
}