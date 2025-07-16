
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Core.DTOs;
using University.Core.Exceptions;
using University.Core.Validations;
using University.Core.Entities;
using University.Core.Interfaces;
using University.Core.Forms.StudentForms;
using Microsoft.Extensions.Logging;

namespace University.Core.Services
{
    public class StudentService
        : IStudentService
    {

        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<StudentService> _logger;
        public StudentService(IStudentRepository studentRepository, ILogger<StudentService> logger)
        {
            _studentRepository = studentRepository;
            _logger = logger;
        }

        public void Create(CreateStudentForm form)
        {

        
            if (form ==null)
                throw new ArgumentNullException(nameof(form));

            var validation = FormValidator.Validate(form);
            if (!validation.IsValid)
            {
                _logger.LogWarning("Validation failed for CreateStudentForm.");
                throw new BuisnessException(validation.Errors);
            }




            // Check for duplicate email
            if (_studentRepository.EmailExists(form.Email))
            {
                _logger.LogWarning("Attempted to create student with duplicate email: {email}", form.Email);
                throw new BuisnessException("Email already exists.");
            }


            var student = new Student()
            {

                Name = form.Name,
                Email = form.Email,
            };

            _studentRepository.Add(student);
            _logger.LogInformation("Student created successfully with email: {email}", form.Email);

        }



        public void Delete(int id)
        {
            var student = _studentRepository.GetById(id); 

            if (student == null)
                throw new NotFoundException($"Student with ID {id} not found.");

            _studentRepository.Delete(student);
            

        }


        public void Update(int id, UpdateStudentForm form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            if (string.IsNullOrWhiteSpace(form.Name))
                throw new BuisnessException("Student name is required.");


            var student = _studentRepository.GetById(id);

            if (student == null)
                throw new NotFoundException($"Student with ID {id} not found.");

            student.Name = form.Name;
            


            _studentRepository.Update(student);
        }

        public List<StudentDTO> GetAll()
        {
            var allStudents = _studentRepository.GetAll();
            var dtos = allStudents.Select(s => new StudentDTO
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email
            }).ToList();

            return dtos;
        }

        public StudentDTO GetById(int id)
        {
            var student = _studentRepository.GetById(id);
            _logger.LogInformation("Somebody tried to call GetById with id {id}", id);

            if (student == null)
                throw new NotFoundException($"Student with ID {id} not found.");


            var dto = new StudentDTO()
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email
            };

            return dto;
        }

        

     
    }


    public interface IStudentService
    {
        StudentDTO GetById(int id);
        List<StudentDTO> GetAll();
        void Create(CreateStudentForm form);
        void Update(int id, UpdateStudentForm form);
        void Delete(int id);

        
    }

}
