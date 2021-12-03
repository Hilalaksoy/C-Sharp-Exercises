using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Models;
using CourseLibrary.API.Profiles;
using CourseLibrary.API.ResorceParameters;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController:ControllerBase
    {
        private ICourseLibraryRepository _courseLibraryRepository;
        private IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository  courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                                       throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors([FromQuery] AuthorsResourceParameters authorsResourceParameters)
        {
            var authors = await _courseLibraryRepository.GetAuthorsAsync(authorsResourceParameters);
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

        [HttpPost]
        public async Task<AuthorDto> CreateAuthor(CreateAuthorDto createAuthorDto)
        {
            var author = _mapper.Map<Author>(createAuthorDto);
            var newAuthor =  await _courseLibraryRepository.AddAuthor(author);
            _courseLibraryRepository.SaveAsync();
            var authorDto =  _mapper.Map<AuthorDto>(newAuthor);
            // return CreatedAtRoute("GetAuthor", new {authorId = authorDto.Id }, authorDto);
            return authorDto;
        }

    }
}
