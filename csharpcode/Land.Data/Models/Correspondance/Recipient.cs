using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Land.Data.Models
{
    public class Recipient
    {
        public int Id { get; set; }        
        public int ContactId { get; set; }
        public int CorrespondanceId { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual Letter Correspondance { get; set; }
    }
}
