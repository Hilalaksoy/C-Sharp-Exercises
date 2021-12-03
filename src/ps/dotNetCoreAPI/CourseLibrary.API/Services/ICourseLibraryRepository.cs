using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.API.Entities;
using CourseLibrary.API.ResorceParameters;


namespace CourseLibrary.API.Services
{
    public interface ICourseLibraryRepository
    {
        Task<IEnumerable<Course>> GetCoursesAsync(Guid authorId);
        Task<Course> GetCourseAsync(Guid authorId, Guid courseId);
        Task<Course> AddCourse(Guid authorId, Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(Course course);
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<IEnumerable<Author>> GetAuthorsAsync(AuthorsResourceParameters authorsResourceParameters);
        Task<IEnumerable<Author>> GetAuthorsWithIdsAsync(IEnumerable<Guid> authorsIds);
        //PagedList<Author> GetAuthors(AuthorsResourceParameters authorsResourceParameters);

        Task<Author> GetAuthorAsync(Guid authorId);
        Task<IEnumerable<Author>> GetAuthorsAsync(IEnumerable<Guid> authorsIds);
        Task<Author> AddAuthor(Author author);
        void AddAuthorRange(IEnumerable<Author> author);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
        Task<bool> AuthorExistsAsync(Guid authorId);
        bool AuthorExists(Guid authorId);
        void SaveAsync();

    }
}
