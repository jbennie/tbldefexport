using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Land.Data.Models
{
    public class PlanningContact:Contact
    {
        public PlanningContact()
        {
            IsPlanningContact = true;
        }
        public bool IsPlanningContact { get; set; }
    }
}
