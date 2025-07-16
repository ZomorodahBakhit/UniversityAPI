using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Core.Forms.CourseForms
{
    public class UpdateCourseForm
    {
        [Required]
        public string CourseName { get; set; }    
    }
}
