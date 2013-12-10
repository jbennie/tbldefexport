using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Land.Data.Models
{
    public class SiteRequirement: CrudInfo
    {

        public SiteRequirement() {
            Requirements = new List<SiteRequirement>();
            Notes = new List<Note>(); 
        }

        public int Id { get; set; }
        
        public Contact Contact { get; set; }
        public Nullable<int> ContactId { get; set; }

        public ICollection<Note> Notes { get; set; }
        public ICollection<SiteRequirement> Requirements { get; set; }

    }
}
