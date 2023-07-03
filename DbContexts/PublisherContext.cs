using Microsoft.EntityFrameworkCore;
using Univali.Api.Entities;

namespace Univali.Api.DbContexts;

// function migrateDB($migrationName){dotnet ef migrations add $migrationName --context PublisherContext}
// function updateDB($contextName){dotnet ef database update --context $contextName}
public class PublisherContext : DbContext
{

    public DbSet<Publisher> Publishers { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;

    public PublisherContext(DbContextOptions<PublisherContext> options)
    : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var publisher = modelBuilder.Entity<Publisher>();

        publisher.HasMany(a => a.Courses)
        .WithOne(c => c.Publisher)
        .HasForeignKey(c => c.PublisherId);

        publisher.Property(c => c.Name).HasMaxLength(80).IsRequired();

        publisher.Property(c => c.CNPJ).HasMaxLength(14).IsFixedLength();

        publisher.HasData(
            new Publisher
            {
                Id = 1,
                Name = "Steven Spielberg Production Company",
                CNPJ = "14698277000144",
            },
            new Publisher
            {
                Id = 2,
                Name = "James Cameron Corporation",
                CNPJ = "12135618000148",
            },
            new Publisher
            {
                Id = 3,
                Name = "Quentin Tarantino Production",
                CNPJ = "64167199000120",
            }
        );

        var author = modelBuilder.Entity<Author>();

        author
            .HasMany(a => a.Courses)
            .WithMany(c => c.Authors)
            .UsingEntity<AuthorCourse>(
                ac => ac.ToTable("PublishersCourses")
               .Property(ac => ac.CreatedOn).HasDefaultValueSql("NOW()")
            );
        
        author
            .Property(a => a.Name)
            .HasMaxLength(80)
            .IsRequired();
        author
            .Property(p => p.Cpf)
            .HasMaxLength(11)
            .IsFixedLength();
        author.HasData(
            new Author
            {
                AuthorId = 1,
                Name = "Grace Hopper",
                Cpf = "23743614626",
            },
            new Author
            {
                AuthorId = 2,
                Name = "John Backus",
                Cpf = "13047822638",
            },
            new Author
            {
                AuthorId = 3,
                Name = "Bill Gates",
                Cpf = "41275433375",
            },
            new Author
            {
                AuthorId = 4,
                Name = "Jim Berners-Lee",
                Cpf = "68999916405",
            },
            new Author
            {
                AuthorId = 5,
                Name = "Linus Torvalds",
                Cpf = "46786017673",
            }
        );

        var course = modelBuilder.Entity<Course>();

        course
            .Property(c => c.Title)
            .HasMaxLength(100)
            .IsRequired();
        course
            .Property(c => c.Price)
            .HasPrecision(7,2)
            .IsRequired();
        course
            .Property(c => c.Description)
            .IsRequired();

        course.HasData(
            new Course
            {
                CourseId = 1,
                Title = "ASP.NET Core Web Api",
                Price = 97.00m,
                Description = "In this course, you'll learn how to build an API with ASP.NET Core that connects to a database via Entity Framework Core from scratch.",
                Category = "Backend",
                PublisherId = 1
            },
            new Course
            {
                CourseId = 2,
                Title = "Entity Framework Fundamentals",
                Price = 197.00m,
                Description = "In this course, Entity Framework Core 6 Fundamentals, you’ll learn to work with data in your .NET applications.",
                Category = "Backend",
                PublisherId = 1
            },
            new Course
            {
                CourseId = 3,
                Title = "Getting Started with Linux",
                Price = 47.00m,
                Description = "You've heard that Linux is the future of enterprise computing and you're looking for a way in.",
                Category = "Operating Systems",
                PublisherId = 2
            }
        );

        var authorCourse = modelBuilder.Entity<AuthorCourse>();

        authorCourse.HasData(
            new AuthorCourse { AuthorId = 1, CourseId = 1 },
            new AuthorCourse { AuthorId = 1, CourseId = 3 },
            new AuthorCourse { AuthorId = 2, CourseId = 1 },
            new AuthorCourse { AuthorId = 2, CourseId = 2 },
            new AuthorCourse { AuthorId = 4, CourseId = 1 },
            new AuthorCourse { AuthorId = 5, CourseId = 3 }
        );

        base.OnModelCreating(modelBuilder);
    }
}