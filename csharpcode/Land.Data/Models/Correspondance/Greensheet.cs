using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Land.Data.Models
{
    public class Greensheet: CrudInfo
    {

        public Greensheet() 
        {
            Events = new List<Event>();
            Letters = new List<Letter>();
            Notes = new List<Note>(); 
        }

        public int Id { get; set; } 
       
        public Site Site {get; set;}
        public int SiteId { get; set; }

        public Contact Contact { get; set; }
        public int ContactId { get; set; }
      
        public LandOfferInterestType Interest { get; set; }
        public int InterestId { get; set; }
     
        public SiteOffer Offer { get; set; }
        public Nullable<int> OfferId { get; set; }

        public ICollection<Note> Notes { get; set; }
        public ICollection<Letter> Letters { get; set; }
        public ICollection<Event> Events { get; set; }

   
      
    }
}
