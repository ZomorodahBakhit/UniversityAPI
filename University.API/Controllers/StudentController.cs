using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Core.Services;

using AutoWrapper.Wrappers;
using System.Net;
using University.Core.DTOs;
using University.Core.Exceptions;
using University.API.Filters;
using University.Core.Forms.StudentForms;
using Microsoft.AspNetCore.Authorization;

namespace University.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(APIExceptionFilter))]
    public class StudentController : ControllerBase
    {

        

        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            
            _studentService = studentService;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StudentDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ApiResponse GetById(int id)
        {
            
                var student = _studentService.GetById(id);
                return new ApiResponse(student);

        }


        [HttpGet()]
        [ProducesResponseType(typeof(List<StudentDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize(Roles ="Student")]//3
        public ApiResponse GetAll()
        {

                var student = _studentService.GetAll();
                
                return new ApiResponse(student);
            

        }





        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ApiResponse Create([FromBody] CreateStudentForm form)
        {


                _studentService.Create(form);
                return new ApiResponse(HttpStatusCode.Created);
            

        }




        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Student not found
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse Update(int id, [FromBody] UpdateStudentForm form)
        {

           
                _studentService.Update(id, form);
                return new ApiResponse(HttpStatusCode.OK);
            


            
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Student not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public ApiResponse Delete(int id)
        {
             _studentService.Delete(id);
             return new ApiResponse(HttpStatusCode.OK);
            
        }
    }
}
