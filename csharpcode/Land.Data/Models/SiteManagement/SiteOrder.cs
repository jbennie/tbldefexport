using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Land.Data.Models
{
    public class SiteOrder: CrudInfo
    {
        public int Id { get; set; }
        public int SiteID { get; set; }

        public int Quantity { get; set; }
        public string Description { get; set; }
        public double UnitValueExVat { get; set; }

        public virtual Site Site { get; set; }

    }
}
