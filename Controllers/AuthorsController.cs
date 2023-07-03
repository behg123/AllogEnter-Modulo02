using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Univali.Api.Features.Authors.Commands.UpdateAuthor;
using Univali.Api.Features.Authors.Commands.CreateAuthor;
using Univali.Api.Features.Authors.Commands.DeleteAuthor;
using Univali.Api.Features.Authors.Queries.GetAuthorDetail;
using Univali.Api.Features.Authors.Queries.GetAuthorWithCoursesDetail;
using Univali.Api.Models;

namespace Univali.Api.Controllers;

[Route("api/authors")]
[Authorize]
public class AuthorController : MainController
{
    private readonly IMediator _mediator;

    public AuthorController(IMediator mediator)
    {
        _mediator = mediator;
    }

///////////////////////////////////////////////////////////////////////////
// Create
///////////////////////////////////////////////////////////////////////////
    [HttpPost]
    public async Task<ActionResult<AuthorDto>> CreateAuthor(
        [FromBody] CreateAuthorCommand createAuthorCommand
    )
    {
        var createAuthorCommandResponse = await _mediator.Send(createAuthorCommand);

        if (!createAuthorCommandResponse.IsSuccess)
        {
            foreach (var error in createAuthorCommandResponse.Errors)
            {
                string Key = error.Key;
                string[] values = error.Value;

                foreach (var value in values)
                {
                    ModelState.AddModelError(Key, value);
                }
            }
            return ValidationProblem(ModelState);
        }

        return CreatedAtRoute
        (
            "GetAuthorById",
            new { authorId = createAuthorCommandResponse.Author.AuthorId },
            createAuthorCommandResponse.Author
        );
    }

///////////////////////////////////////////////////////////////////////////
// Read
///////////////////////////////////////////////////////////////////////////
    [HttpGet("{authorId}", Name = "GetAuthorById")]
    public async Task<ActionResult<AuthorDto>> GetAuthorById(int authorId)
    {
        var getAuthorQuery = new GetAuthorDetailQuery {AuthorId = authorId};

        var authorToReturn = await _mediator.Send(getAuthorQuery);

        if(authorToReturn == null) return NotFound();

        return Ok(authorToReturn);
    }

    [HttpGet("with-courses/{authorId}", Name = "GetAuthorWithCoursesById")]
    public async Task<ActionResult<GetAuthorWithCoursesDetailDto>> GetAuthorWithCoursesById(int authorId)
    {
        var getAuthorQuery = new GetAuthorWithCoursesDetailQuery {AuthorId = authorId};

        var authorToReturn = await _mediator.Send(getAuthorQuery);

        if(authorToReturn == null) return NotFound();

        return Ok(authorToReturn);
    }
///////////////////////////////////////////////////////////////////////////
// Update
///////////////////////////////////////////////////////////////////////////
    [HttpPut("{authorId}")]
    public async Task<ActionResult> UpdateAuthor(int authorId, UpdateAuthorCommand updateAuthorCommand)
    {
        if(authorId != updateAuthorCommand.AuthorId) return BadRequest();

        var updateAuthorCommandResponse = await _mediator.Send(updateAuthorCommand);

        if(!updateAuthorCommandResponse.IsSuccess)
        {
            foreach(var error in updateAuthorCommandResponse.Errors)
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

        if(!updateAuthorCommandResponse.Exist) return NotFound();

        return NoContent();
    }

///////////////////////////////////////////////////////////////////////////
// Delete
///////////////////////////////////////////////////////////////////////////
    [HttpDelete("{authorId}")]
    public async Task<ActionResult> DeleteAuthor(int authorId)
    {
        if(!await _mediator.Send(new DeleteAuthorCommand { AuthorId = authorId }))
            return NotFound();
        
        return NoContent();
    }
}