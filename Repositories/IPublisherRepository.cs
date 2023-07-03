using Univali.Api.Entities;

namespace Univali.Api.Repositories;

public interface IPublisherRepository
{
////////////////////////////////////////////////////////////////////////////
// Publisher
////////////////////////////////////////////////////////////////////////////
    void AddPublisher(Publisher publisher);
    Task<IEnumerable<Publisher>> GetPublishersAsync();
    Task<Publisher?> GetPublisherByIdAsync(int publisherId);
    public Task<Publisher?> GetPublisherWithCoursesByIdAsync(int publisherId);
    void RemovePublisher(int publisherId);
    Task<bool> PublisherExistsAsync(int publisherId);

////////////////////////////////////////////////////////////////////////////
// Course
////////////////////////////////////////////////////////////////////////////
    Task<bool> AddCourseAsync(int publisherId, Course course);
    Task<Course?> GetCourseByIdAsync(int courseId);
    public Task<Course?> GetCourseWithAuthorsByIdAsync(int courseId);
    void UpdateCourse(Course course);
    void DeleteCourse(int courseId);

////////////////////////////////////////////////////////////////////////////
// Author
//////////////////////////////////////////////////////////////////////////// 
    void AddAuthor(Author author);
    Task<Author?> FindAuthorAsync(int id);
    Task<Author?> GetAuthorByIdAsync(int authorId);
    Task<List<Author>> GetAuthorsById(IEnumerable<int> authorIds);
    Task<Author?> GetAuthorWithCoursesByIdAsync(int authorId);
    void UpdateAuthor(Author author);
    void DeleteAuthor(Author author);

////////////////////////////////////////////////////////////////////////////
// Save Changes
//////////////////////////////////////////////////////////////////////////// 
    Task<bool> SaveChangesAsync();
}