using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Land.Data.Models
{
    public class Buyer: Contact
    {
        public Buyer()
        {
            IsBuyer = true;
        }

        public bool IsBuyer { get; set; }
     
    }
}