using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Land.Data.Models
{
    public class BrokerSolicitor : Contact
    {
        public BrokerSolicitor()
        {
            IsBrokerSolicitor = true;
        }

        public bool IsBrokerSolicitor { get; set; }
    }
}
