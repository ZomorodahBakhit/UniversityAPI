using University.Core.Entities;
using University.Core.Interfaces;
using University.Data.Contexts;

namespace University.Data.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(UniversityDbContext context): base(context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));//Ensure the context is not null to avoid NullReferenceException.

        }
        
        public void Add(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            student.SetCreated();//This is defined in the Student class to set the CreatedTime property.
            base.Add(student);
        }
        public void Update(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            student.SetUpdated();//This is defined in the Student class to set the LastUpdatedTime property.
            base.Update(student);
        }


        public bool EmailExists(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty", nameof(email));

            return _set.Any(s => s.Email == email); // Use _set from GenericRepository
        }

    }

    


}
