using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Data.Contexts;
using University.Data.Entities;

namespace University.Data.Repositories
{
    public class StudentRepository: IStudentRepository
    {
        private readonly UniversityDbContext _context;
        public StudentRepository(UniversityDbContext context)
        {
            _context = context;
        }
        public Student GetById(int id)
        {
            return _context.Students.Find(id);
        }
        public List<Student> GetAll()
        {
            return _context.Students.ToList();
        }
        public void Add(Student student)
        {
            if (student == null)
                throw new ArgumentException(nameof(student));

            student.CreatedTime = DateTime.Now;
            _context.Students.Add(student);
            _context.SaveChanges();
        }
        public void Update(Student student)
        {
            if (student == null)
                throw new ArgumentException(nameof(student));

            student.LastUpdatedTime = DateTime.Now;
            _context.Students.Update(student);
            _context.SaveChanges();
        }
        public void Delete(Student student)
        {
            if (student ==null)
                throw new ArgumentException(nameof(student));

            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

    


    public interface IStudentRepository
    {
        Student GetById(int id);
        List<Student> GetAll();


        void Add(Student student);
        void Update(Student student);
        void Delete(Student student);
        void SaveChanges();


    }
}
