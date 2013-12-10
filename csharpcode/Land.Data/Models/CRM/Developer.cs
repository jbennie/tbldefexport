using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Land.Data.Models
{
    public class Developer: Contact
    {

        public Developer()
        {
            IsDeveloper = true;
        }
        public bool IsDeveloper { get; set; }
  
    }
}

