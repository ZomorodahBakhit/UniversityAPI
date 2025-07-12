using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Core.DTOs;
using University.Core.Forms;
using University.Data.Entities;
using University.Data.Repositories;

namespace University.Core.Services
{
    public class StudentService
        : IStudentService
    {

        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void Create(CreateStudentForm form)
        {

        
            if (form ==null)
                throw new ArgumentNullException(nameof(form));

            if (string.IsNullOrWhiteSpace(form.Name))
                throw new ArgumentException("Student name is required.", nameof(form.Name));

            if (string.IsNullOrWhiteSpace(form.Email))
                throw new ArgumentException("Student email is required.", nameof(form.Email));

            var student = new Student()
            {

                Name = form.Name,
                Email = form.Email,
            };

            _studentRepository.Add(student);
            _studentRepository.SaveChanges();
        }



        public void Delete(int id)
        {
            var student = _studentRepository.GetById(id); 

            if (student == null)
                throw new KeyNotFoundException($"Student with ID {id} not found.");

            _studentRepository.Delete(student);
            _studentRepository.SaveChanges();

        }


        public void Update(int id, UpdateStudentForm form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            if (string.IsNullOrWhiteSpace(form.Name))
                throw new ArgumentException("Student name is required.", nameof(form.Name));


            var student = _studentRepository.GetById(id);

            if (student == null)
                throw new KeyNotFoundException($"Student with ID {id} not found.");

            student.Name = form.Name;
            


            _studentRepository.Update(student);
            _studentRepository.SaveChanges();
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

            if (student == null)
                throw new KeyNotFoundException($"Student with ID {id} not found.");


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
