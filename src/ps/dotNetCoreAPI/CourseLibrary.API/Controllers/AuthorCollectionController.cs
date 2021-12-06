using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/author-collection")]
    public class AuthorCollectionController: ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private IMapper _mapper;
        public AuthorCollectionController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository;
            _mapper = mapper;
        }

        [HttpGet("({ids})")]
        public ActionResult<List<AuthorDto>> GetAuthorCollection([FromRoute] [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var authorEntities =  _courseLibraryRepository.GetAuthorsAsync(ids).Result;

            if (ids.Count() != authorEntities.Count())
            {
                return NotFound();
            }

            return _mapper.Map<List<AuthorDto>>(authorEntities);
        }

        [HttpPost]
        public ActionResult<List<AuthorDto>> CreateAuthorCollection(List<CreateAuthorDto> createAuthorDtos)
        {
            var authors = _mapper.Map<List<Author>>(createAuthorDtos);
            foreach (var author in authors )
            {

                _courseLibraryRepository.AddAuthor(author);
                _courseLibraryRepository.SaveAsync();
            }

            return Ok();
        }
    }
}
