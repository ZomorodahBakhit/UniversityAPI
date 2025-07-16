using System.Linq;
using University.Core.Entities;
using University.Core.Interfaces;
using University.Data.Contexts;

namespace University.Data.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(UniversityDbContext context) : base(context)
        {
        }

       

        // CreatedTime / UpdatedTime logic
        public  void Add(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            course.SetCreated();
            base.Add(course);
        }

        public  void Update(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            course.SetUpdated();
            base.Update(course);
        }
    }


}
