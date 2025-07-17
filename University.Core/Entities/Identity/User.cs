using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;    

namespace University.Core.Entities.Identity
{
    public class User : IdentityUser<int>
    {

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;  

    }
}
