using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Core.Entities;

namespace University.Core.Interfaces
{

    public interface ICourseRepository : IGenericRepository<Course>
    {
        // Additional methods specific to Course can be defined here
    }
}
