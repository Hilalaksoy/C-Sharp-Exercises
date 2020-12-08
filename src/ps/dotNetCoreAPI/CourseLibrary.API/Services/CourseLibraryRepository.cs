using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Entities;

namespace CourseLibrary.API.Services
{
    public class CourseLibraryRepository: ICourseLibraryRepository
    {
        private readonly CourseLibraryContext _context;

        public CourseLibraryRepository(CourseLibraryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Task<IEnumerable<Course>> GetCoursesAsync(Guid authorId)
        {
            throw new NotImplementedException();
        }

        public Task<Course> GetCourseAsync(Guid authorId, Guid courseId)
        {
            throw new NotImplementedException();
        }

        public void AddCourse(Guid authorId, Course course)
        {
            throw new NotImplementedException();
        }

        public void UpdateCourse(Course course)
        {
            throw new NotImplementedException();
        }

        public void DeleteCourse(Course course)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Author>> GetAuthorsWithIdsAsync(IEnumerable<Guid> authorsIds)
        {
            throw new NotImplementedException();
        }

        //public PagedList<Author> GetAuthors(AuthorsResourceParameters authorsResourceParameters)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<Author> GetAuthorAsync(Guid authorId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Author>> GetAuthorsAsync(IEnumerable<Guid> authorsIds)
        {
            throw new NotImplementedException();
        }

        public void AddAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public void AddAuthorRange(IEnumerable<Author> author)
        {
            throw new NotImplementedException();
        }

        public void DeleteAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public void UpdateAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AuthorExistsAsync(Guid authorId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
