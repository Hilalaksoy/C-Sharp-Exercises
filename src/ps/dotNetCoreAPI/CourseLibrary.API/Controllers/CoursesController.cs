using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;


namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private IMapper _mapper;
        public CoursesController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<CourseDto>> GetCoursesForAuthor(Guid authorId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courses =  _courseLibraryRepository.GetCoursesAsync(authorId).Result;
            return _mapper.Map<List<CourseDto>>(courses);
        }

        [HttpGet("{courseId}", Name = "GetCourseForAuthor")]
        public ActionResult<CourseDto> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
               return NotFound();
            }

            var course = _courseLibraryRepository.GetCourseAsync(authorId, courseId).Result;

            if (course == null)
            { 
               return NotFound();
            }

            return Ok(_mapper.Map<CourseDto>(course));
        }

        [HttpPost]
        public  ActionResult<CourseDto> CreateCourseForAuthor(Guid authorId, CreateCourseDto createCourseDto)
        {
            if (! _courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseEntity = _mapper.Map<Entities.Course>(createCourseDto);
            var newCourse = _courseLibraryRepository.AddCourse(authorId, courseEntity).Result;
            _courseLibraryRepository.SaveAsync();

            var courseDto = _mapper.Map<CourseDto>(newCourse);
            return courseDto;
        }
    }
}
