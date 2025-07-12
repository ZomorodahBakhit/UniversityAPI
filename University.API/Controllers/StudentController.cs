using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Core.Services;
using University.Data.Contexts;
using University.Data.Repositories;
using AutoWrapper.Wrappers;
using University.Core.Forms;
using System.Net;

namespace University.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
           
            _studentService = studentService;
        }


        [HttpGet("{id}")]
        public ApiResponse GetById(int id)
        {
            var student = _studentService.GetById(id);
            if (student == null)
                throw new KeyNotFoundException($"Student with ID {id} not found.");

            return new ApiResponse(student);


        }


        [HttpGet()]
        public ApiResponse GetAll()
        {
            var student = _studentService.GetAll();
            if (student == null)
                throw new KeyNotFoundException("There are no students");

            return new ApiResponse(student);
        }





        [HttpPost]
        public ApiResponse Create([FromBody] CreateStudentForm form)
        {
            _studentService.Create(form);
            return new ApiResponse(HttpStatusCode.Created);
        }




        [HttpPut("{id}")]
        public ApiResponse Update(int id, [FromBody] UpdateStudentForm form)
        {
            _studentService.Update(id, form);
            return new ApiResponse(HttpStatusCode.OK);
        }


        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            _studentService.Delete(id);
            return new ApiResponse(HttpStatusCode.OK);
        }
    }
}
