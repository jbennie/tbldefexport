using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Land.Data.Models
{
    public class VendorSolicitor : Contact
    {
        public VendorSolicitor()
        {
            IsVendorSolicitor = true;
        }

        public bool IsVendorSolicitor { get; set; }
    }

}
