using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Entities;
using CourseLibrary.API.ResorceParameters;
using Microsoft.EntityFrameworkCore;

namespace CourseLibrary.API.Services
{
    public class CourseLibraryRepository: ICourseLibraryRepository
    {
        private readonly CourseLibraryContext _context;

        public CourseLibraryRepository(CourseLibraryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Course>> GetCoursesAsync(Guid authorId)
        {
            return await _context.Courses.Where(x => x.AuthorId == authorId).ToListAsync();
        }

        public async Task<Course> GetCourseAsync(Guid authorId, Guid courseId)
        {
            return await _context.Courses.FirstOrDefaultAsync(x => x.AuthorId == authorId && x.Id == courseId);
        }

        public async Task<Course> AddCourse(Guid authorId, Course course)
        {
            //var author = await GetAuthorAsync(authorId);
            var newCourse = new Course
            {
                Id = Guid.NewGuid(),
                Title = course.Title,
                Description = course.Description,
                AuthorId = authorId,
            };
            _context.Courses.Add(newCourse);
            return newCourse;
        }

        public void UpdateCourse(Course course)
        {
            throw new NotImplementedException();
        }

        public void DeleteCourse(Course course)
        {
            throw new NotImplementedException();
        }

        public  async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
           var authors=_context.Authors.ToListAsync();
           return  await authors;
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync(AuthorsResourceParameters authorsResourceParametersy)
        {
            if (string.IsNullOrWhiteSpace(authorsResourceParametersy.MainCategory) && string.IsNullOrWhiteSpace(authorsResourceParametersy.SearchQuery))
            {
                return await GetAuthorsAsync();
            }

            var collection = _context.Authors as IQueryable<Author>;

            if (!string.IsNullOrWhiteSpace(authorsResourceParametersy.MainCategory))
            {
                var mainCategory = authorsResourceParametersy.MainCategory.Trim();
                  collection = collection.Where(x => x.MainCategory == mainCategory);
            }

            if (!string.IsNullOrWhiteSpace(authorsResourceParametersy.SearchQuery))
            {
                var searchQuery = authorsResourceParametersy.SearchQuery.Trim();
                collection = collection.Where(x =>
                    x.MainCategory.Contains(searchQuery) || x.FirstName.Contains(searchQuery) || x.LastName.Contains(searchQuery));
            }

            return await  collection.ToListAsync();
        }


        public Task<IEnumerable<Author>> GetAuthorsWithIdsAsync(IEnumerable<Guid> authorsIds)
        {
            throw new NotImplementedException();
        }

        //public PagedList<Author> GetAuthors(AuthorsResourceParameters authorsResourceParameters)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<Author> GetAuthorAsync(Guid authorId)
        {
            //return await _context.Authors.FirstOrDefaultAsync(i => i.Id == authorId);
            return await _context.Authors.FindAsync(authorId);
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync(IEnumerable<Guid> authorsIds)
        {
            var authors = new List<Author>();
            foreach (var authorId in authorsIds)
            {
                var author = await _context.Authors.FindAsync(authorId);
                authors.Add(author);
            }

            return authors;
        }

        public async Task<Author> AddAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException();
            }

            var newAuthor = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth,
                MainCategory = author.MainCategory
            };

            foreach (var course in author.Courses)
            {
                course.Id = Guid.NewGuid();
            }

            newAuthor.Courses = author.Courses;
            await _context.Authors.AddAsync(newAuthor);
            return newAuthor;
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

        public async Task<bool> AuthorExistsAsync(Guid authorId)
        {
            var author = await _context.Authors.FindAsync(authorId);
            return author != null;
        }

        public bool AuthorExists(Guid authorId)
        {
            var author = _context.Authors.Find(authorId);
            return author != null;
        }

        public void SaveAsync()
        {
           _context.SaveChanges();
        }

    }
}
