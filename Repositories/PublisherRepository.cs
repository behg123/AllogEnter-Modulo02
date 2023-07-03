using Microsoft.EntityFrameworkCore;
using Univali.Api.DbContexts;
using Univali.Api.Entities;
using Univali.Api.Models;

namespace Univali.Api.Repositories;

public class PublisherRepository : IPublisherRepository
{
    private readonly PublisherContext _context;

    public PublisherRepository(PublisherContext context)
    {
        _context = context;
    }

////////////////////////////////////////////////////////////////////////////
// Publishers
////////////////////////////////////////////////////////////////////////////
    ///////////////////////////
    // Create
    public void AddPublisher(Publisher publisher)
    {
        _context.Publishers.Add(publisher);
    }

    ///////////////////////////
    // Read    
    public async Task<IEnumerable<Publisher>> GetPublishersAsync()
    {
        return await _context.Publishers.OrderBy(p => p.Id).ToListAsync();
    }
    public async Task<Publisher?> GetPublisherByIdAsync(int publisherId)
    {
        return await _context.Publishers.FirstOrDefaultAsync(p => p.Id == publisherId);
    }
    public async Task<Publisher?> GetPublisherWithCoursesByIdAsync(int publisherId)
    {
        return await _context.Publishers.Include(p => p.Courses).FirstOrDefaultAsync(p => p.Id == publisherId);
    }

    ///////////////////////////
    // Remove   
    public void RemovePublisher(int publisherId)
    {
        var publisherEntity = _context.Publishers.FirstOrDefault(c => c.Id == publisherId);

        if (publisherEntity == null) return;

        _context.Publishers.Remove(publisherEntity);
    }

    ///////////////////////////
    // Utils
    public async Task<bool> PublisherExistsAsync(int publisherId)
    {
        return await _context.Publishers.AnyAsync(publisher => publisher.Id == publisherId);
    }


////////////////////////////////////////////////////////////////////////////
// Courses
////////////////////////////////////////////////////////////////////////////
    ///////////////////////////
    // Create
    public async Task<bool> AddCourseAsync(int publisherId, Course course)
    {
        var publisher = await GetPublisherByIdAsync(publisherId);

        if (publisher == null) return false;

        publisher.Courses.Add(course);

        return true;
    }

    ///////////////////////////
    // Read   
    public async Task<Course?> GetCourseByIdAsync(int courseId)
    {
        return await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == courseId);
    }
    public async Task<Course?> GetCourseWithAuthorsByIdAsync(int courseId)
    {
        return await _context.Courses
            .Include(c => c.Authors)
            .FirstOrDefaultAsync(c => c.CourseId == courseId);
    }
    public async Task<IEnumerable<Course>> GetCoursesAsync()
    {
        return await _context.Courses.ToListAsync();
    }
    public async Task<IEnumerable<Course>> GetCoursesAsync(string? category, string? searchQuery)
    {
        if(string.IsNullOrWhiteSpace(category) && string.IsNullOrEmpty(searchQuery)) return await GetCoursesAsync();
        
        var collection = _context.Courses as IQueryable<Course>;

        if(!string.IsNullOrWhiteSpace(category))
        {
            category = category.Trim();
            collection = collection.Where(c => c.Category == category);

        }

        if(!string.IsNullOrWhiteSpace(searchQuery)){
            searchQuery = searchQuery.Trim();
            collection = collection.Where(c => c.Category.Contains(searchQuery) || c.Title.Contains(searchQuery) || c.Description.Contains(searchQuery)); 

        }

        return await collection.ToListAsync();

    }

    ///////////////////////////
    // Delete
    public void DeleteCourse(int courseId)
    {
        var courseEntity = _context.Courses.FirstOrDefault(c => c.CourseId == courseId);

        if (courseEntity == null) return;

        _context.Courses.Remove(courseEntity);
    }


////////////////////////////////////////////////////////////////////////////
// Authors
////////////////////////////////////////////////////////////////////////////
    ///////////////////////////
    // Create
    public void AddAuthor(Author author)
    {
        _context.Authors.Add(author);
    }

    ///////////////////////////
    // Read
    public async Task<List<Author>> GetAuthorsById(IEnumerable<int> authorIds)
    {
        return await _context.Authors.Where(a => authorIds.Contains(a.AuthorId)).ToListAsync();
    }
    public async Task<Author?> GetAuthorByIdAsync(int authorId)
    {
        return await _context.Authors.FirstOrDefaultAsync(author => author.AuthorId == authorId);
    }
    public async Task<Author?> GetAuthorWithCoursesByIdAsync(int authorId)
    {
        return await _context.Authors.Include(a => a.Courses).FirstOrDefaultAsync(author => author.AuthorId == authorId);
    }

    ///////////////////////////
    // Delete
    public void DeleteAuthor(Author author)
    {
        _context.Authors.Remove(author);
    }


////////////////////////////////////////////////////////////////////////////
// Save Changes
////////////////////////////////////////////////////////////////////////////
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }



}
