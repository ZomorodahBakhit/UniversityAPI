﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace University.Core.Exceptions
{
    public class BusinessException: Exception

    {
        public Dictionary<string, List<string>> Errors { get; set; } 
        
        public BusinessException(string message) : base(message)
        {

            Errors =  new Dictionary<string, List<string>>();


        }
        public BusinessException(Dictionary<string, List<string>> _errors)
        {

            Errors = _errors ??  new Dictionary<string, List<string>>();

        }
        
    }
}
