using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Core.Entities;

namespace University.Core.Interfaces
{

    public interface IStudentRepository : IGenericRepository<Student>
    {
        // Additional methods specific to Student can be defined here
        bool EmailExists(string email);
    }
}
