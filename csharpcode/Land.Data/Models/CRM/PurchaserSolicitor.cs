using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Land.Data.Models
{
        public class PurchaserSolicitor : Contact
        {
            public PurchaserSolicitor()
            {
                IsPurchaserSolicitor = true;
            }

            public bool IsPurchaserSolicitor { get; set; }
        }
}
