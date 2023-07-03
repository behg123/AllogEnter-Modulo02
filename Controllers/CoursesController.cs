using AutoMapper;
using Univali.Api.Features.Courses.Queries.GetCourseDetail;
using Univali.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Univali.Api.Features.Courses.Commands.CreateCourse;
using Univali.Api.Features.Courses.Commands.DeleteCourse;
using Univali.Api.Features.Courses.Commands.UpdateCourse;
using Microsoft.AspNetCore.Authorization;
using Univali.Api.Features.Courses.Queries.GetCourseWithAuthorsDetail;

namespace Univali.Api.Controllers;

[Route("api/publishers/{publisherId}/courses")]
[Authorize]
public class CourseController : MainController
{
    private readonly IMediator _mediator;

    public CourseController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }


///////////////////////////////////////////////////////////////////////////
// Create
///////////////////////////////////////////////////////////////////////////
    // Você precisa refatorar esse método para inserir o relacionamento com Publisher também
    // O método deve retornar um CreatedAtRoute() ao invés de Ok()
    [HttpPost]
    public async Task<ActionResult<CreateCourseDto>> CreateCourse(int publisherId, CreateCourseCommand createCourseCommand)
    {
        createCourseCommand.PublisherId = publisherId;

        var createCourseCommandResponse = await _mediator.Send(createCourseCommand);

        if (!createCourseCommandResponse.IsSuccess)
        {
            foreach (var error in createCourseCommandResponse.Errors)
            {
                string key = error.Key;
                string[] values = error.Value;

                foreach (var value in values)
                {
                    ModelState.AddModelError(key, value);
                }
            }
            return ValidationProblem(ModelState);
        }

        if (createCourseCommandResponse.Course == null) return NotFound();

        return CreatedAtRoute(
            "GetCourseWhithAuthorsById",
            new { publisherId, createCourseCommandResponse.Course.CourseId },
            createCourseCommandResponse.Course
        );
    }

///////////////////////////////////////////////////////////////////////////
// Read
///////////////////////////////////////////////////////////////////////////
    [HttpGet("{courseId}", Name = "GetCourseById")]
    public async Task<ActionResult<CourseDto>> GetCourseById(int publisherId, int courseId)
    {
        var getCourseDetailQuery = new GetCourseDetailQuery {PublisherId =  publisherId, CourseId = courseId};

        var courseToReturn = await _mediator.Send(getCourseDetailQuery);

        if(courseToReturn == null) return NotFound();

        return Ok(courseToReturn);
    }

    [HttpGet("with-authors/{courseId}", Name = "GetCourseWhithAuthorsById")]
    public async Task<ActionResult<CourseWithAuthorsDto>> GetCourseWhithAuthosById(int publisherId, int courseId)
    {
        var getCourseDetailQuery = new GetCourseWithAuthorsDetailQuery {PublisherId =  publisherId, CourseId = courseId};

        var courseToReturn = await _mediator.Send(getCourseDetailQuery);

        if(courseToReturn == null) return NotFound();

        return Ok(courseToReturn);
    }

///////////////////////////////////////////////////////////////////////////
// Update
///////////////////////////////////////////////////////////////////////////    
    [HttpPut("{courseId}")]
    public async Task<ActionResult> UpdateCourse(int publisherId, int courseId, UpdateCourseCommand updateCourseCommand)
    {
        if (updateCourseCommand.CourseId != courseId) return BadRequest();

        updateCourseCommand.PublisherId = publisherId;

        var updateCourseCommandResponse = await _mediator.Send(updateCourseCommand);

        if(!updateCourseCommandResponse.IsSuccess)
        {
            foreach(var error in updateCourseCommandResponse.Errors)
            {
                string key = error.Key;
                string[] values = error.Value;

                foreach(var value in values)
                {
                    ModelState.AddModelError(key, value);
                }
            }
            return ValidationProblem(ModelState);
        }

        if(!updateCourseCommandResponse.Exist) return NotFound();

        return NoContent();
    }

///////////////////////////////////////////////////////////////////////////
// Delete
///////////////////////////////////////////////////////////////////////////   
    [HttpDelete("{courseId}")]
    public async Task<ActionResult> DeleteCourse(int publisherId, int courseId)
    {

        var deleteCourseCommand = new DeleteCourseCommand{PublisherId = publisherId, CourseId = courseId};

        if(!await _mediator.Send(deleteCourseCommand)) return NotFound();

        return NoContent();
    }


}