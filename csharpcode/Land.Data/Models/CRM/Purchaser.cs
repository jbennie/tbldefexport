using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Land.Data.Models
{
    public class Purchaser : Contact
    {
        public Purchaser()
        {
            IsPurchaser = true;
        }

        public bool IsPurchaser { get; set; }
    }
}
