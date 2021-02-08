using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController:ControllerBase
    {
        private ICourseLibraryRepository _courseLibraryRepository;

        public AuthorsController(ICourseLibraryRepository  courseLibraryRepository)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                                       throw new ArgumentNullException(nameof(courseLibraryRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _courseLibraryRepository.GetAuthors();
            return Ok(authors);
        }

        //[HttpGet("{authorId:guid}")] rota kısıtlamaları kullanılabilir.
        [HttpGet("{authorId}")]
        public async Task<IActionResult> GetAuthor(Guid authorId)
        {
            var author = await _courseLibraryRepository.GetAuthorAsync(authorId);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

    }
}
