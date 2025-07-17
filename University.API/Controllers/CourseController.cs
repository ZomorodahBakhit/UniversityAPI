using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using University.API.Filters;
using University.Core.DTOs;
using University.Core.Exceptions;
using University.Core.Forms.CourseForms;
using University.Core.Services;

namespace University.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(APIExceptionFilter))]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        /// <summary>
        /// Get a course by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CourseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse GetById(int id)
        {
            var course = _courseService.GetById(id);
            return new ApiResponse(course);
        }

       
        [HttpGet]
        [ProducesResponseType(typeof(List<CourseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles ="Teacher")]//3
        public ApiResponse GetAll()
        {
            var courses = _courseService.GetAll();
            return new ApiResponse(courses);
        }

        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse Create([FromBody] CreateCourseForm form)
        {
            _courseService.Create(form);
            return new ApiResponse(HttpStatusCode.Created);
        }

        /// <summary>
        /// Update a course
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse Update(int id, [FromBody] UpdateCourseForm form)
        {
            _courseService.Update(id, form);
            return new ApiResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Delete a course
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse Delete(int id)
        {
            _courseService.Delete(id);
            return new ApiResponse(HttpStatusCode.OK);
        }
    }
}
