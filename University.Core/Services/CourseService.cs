using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using University.Core.DTOs;
using University.Core.Entities;
using University.Core.Exceptions;
using University.Core.Forms.CourseForms;
using University.Core.Interfaces;
using University.Core.Validations;

namespace University.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<CourseService> _logger;

        public CourseService(ICourseRepository courseRepository, ILogger<CourseService> logger)
        {
            _courseRepository = courseRepository;
            _logger = logger;
        }

        public void Create(CreateCourseForm form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            var validation = FormValidator.Validate(form);
            if (!validation.IsValid)
                throw new BusinessException(validation.Errors);

            var course = new Course
            {
                CourseName = form.CourseName,
                Description = form.Description
            };

            _courseRepository.Add(course);
        }

        public void Update(int id, UpdateCourseForm form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            if (string.IsNullOrWhiteSpace(form.CourseName))
                throw new BusinessException("Course name is required.");

            var course = _courseRepository.GetById(id);
            if (course == null)
                throw new NotFoundException($"Course with ID {id} not found.");

            course.CourseName = form.CourseName;
           // course.Description = form.Description;

            _courseRepository.Update(course);
        }

        public void Delete(int id)
        {
            var course = _courseRepository.GetById(id);
            if (course == null)
                throw new NotFoundException($"Course with ID {id} not found.");

            _courseRepository.Delete(course);
        }

        public List<CourseDTO> GetAll()
        {
            var courses = _courseRepository.GetAll();

            return courses.Select(c => new CourseDTO
            {
                Id = c.Id,
                CourseName = c.CourseName,
                Description = c.Description
            }).ToList();
        }

        public CourseDTO GetById(int id)
        {
            _logger.LogInformation("Fetching Course by ID {id}", id);

            var course = _courseRepository.GetById(id);
            if (course == null)
                throw new NotFoundException($"Course with ID {id} not found.");

            return new CourseDTO
            {
                Id = course.Id,
                CourseName = course.CourseName,
                Description = course.Description
            };
        }
    }

    public interface ICourseService
    {
        CourseDTO GetById(int id);
        List<CourseDTO> GetAll();
        void Create(CreateCourseForm form);
        void Update(int id, UpdateCourseForm form);
        void Delete(int id);
    }
}
