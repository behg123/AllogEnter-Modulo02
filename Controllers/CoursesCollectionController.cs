using Microsoft.AspNetCore.Mvc;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Controllers;


[Route("api/courses-collection")]
public class CoursesCollectionController : MainController
{
    private readonly IPublisherRepository _publisherRepository;
    
    public CoursesCollectionController(IPublisherRepository publisherRepository){
        _publisherRepository = publisherRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses(string? category = "", string searchQuery = "")
    {
        return Ok(await _publisherRepository.GetCoursesAsync(category, searchQuery));
    }
}