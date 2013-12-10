using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Land.Data.Models
{
    public class Owner: Contact
    {
         public Owner()
        {
            IsOwner = true;
        }
        public bool IsOwner { get; set; }

    }
}
