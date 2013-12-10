using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Land.Data.Models
{
    public class Negotiator: Contact
    {

        public Negotiator()
        {
            IsNegotiator = true;
        }
        public bool IsNegotiator { get; set; }

   

    }
}
