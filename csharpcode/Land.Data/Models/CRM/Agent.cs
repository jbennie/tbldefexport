using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Land.Data.Models
{
    public class Agent: Contact
    {
        public Agent()
        {
            IsAgent = true;
        }

        public bool IsAgent { get; set; }
     
    }
}
