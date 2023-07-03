using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Univali.Api.Features.Publishers.Commands.DeletePublisher;
using Univali.Api.Features.Publishers.Commands.CreatePublisher;
using Univali.Api.Features.Publishers.Commands.UpdatePublisher;
using Univali.Api.Features.Publishers.Queries.GetPublisherDetail;
using Univali.Api.Features.Publishers.Queries.GetPublisherWithCoursesDetail;
using Univali.Api.Models;


namespace Univali.Api.Controllers;

[Route("api/publishers")]
[Authorize]
public class PublishersController : MainController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;


    public PublishersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

///////////////////////////////////////////////////////////////////////////
// Create
///////////////////////////////////////////////////////////////////////////
    [HttpPost]
    public async Task<ActionResult<PublisherDto>> CreatePublisher(
        CreatePublisherCommand createPublisherCommand
        )
    {
        var createPublisherCommandResponse = await _mediator.Send(createPublisherCommand);

        if(!createPublisherCommandResponse.IsSuccess)
        {
            foreach(var error in createPublisherCommandResponse.Errors)
            {
                string key = error.Key;
                string[] values = error.Value;

                foreach(var value in values)
                {
                    ModelState.AddModelError(key,value);
                }
            }
            return ValidationProblem(ModelState);
        }

        return CreatedAtRoute
        (
            "GetPublisherById",
            new { publisherId = createPublisherCommandResponse.Publisher.Id },
            createPublisherCommandResponse.Publisher
        );

    }


///////////////////////////////////////////////////////////////////////////
// Read
///////////////////////////////////////////////////////////////////////////
    [HttpGet("{publisherId}", Name = "GetPublisherById")]
    public async Task<ActionResult<PublisherDto>> GetPublisherById(int publisherId)
    {
        var getPublisherDetailQuery = new GetPublisherDetailQuery { Id = publisherId };

        var publisherToReturn = await _mediator.Send(getPublisherDetailQuery);

        if (publisherToReturn == null) return NotFound();

        return Ok(publisherToReturn);
    }
    
    [HttpGet("with-courses/{publisherId}")]
    public async Task<ActionResult<PublisherWithCoursesDto>> GetPublisherWithCoursesById(int publisherId)
    {
        var getPublisherWhithCoursesDetailQuery = new GetPublisherWithCoursesDetailQuery { Id = publisherId };

        var publisherToReturn = await _mediator.Send(getPublisherWhithCoursesDetailQuery);

        if (publisherToReturn == null) return NotFound();

        return Ok(publisherToReturn);
    }

///////////////////////////////////////////////////////////////////////////
// UPDATE
///////////////////////////////////////////////////////////////////////////
    [HttpPut("{publisherId}")]
    public async Task<ActionResult> UpdatePublisher(
        int publisherId, [FromBody] UpdatePublisherCommand updatePublisherCommand)
    {
        if(updatePublisherCommand.Id != publisherId) return BadRequest();

        var updatePublisherCommandResponse = await _mediator.Send(updatePublisherCommand);

        if(!updatePublisherCommandResponse.IsSuccess)
        {
            foreach(var error in updatePublisherCommandResponse.Errors)
            {
                string key = error.Key;
                string[] values = error.Value;

                foreach(var value in values)
                {
                    ModelState.AddModelError(key,value);
                }
            }
            return ValidationProblem(ModelState);
        }
        
        return NoContent();
    }

///////////////////////////////////////////////////////////////////////////
// DELETE
///////////////////////////////////////////////////////////////////////////
    [HttpDelete("{publisherId}")]
    public async Task<ActionResult> DeletePublisher(int publisherId)
    {
        await _mediator.Send(new DeletePublisherCommand(publisherId));

        return NoContent();
    }

}
