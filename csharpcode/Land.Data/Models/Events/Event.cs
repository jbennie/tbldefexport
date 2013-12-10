using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Land.Data.Models
{
    public class Event: CrudInfo
    {
        public Event() 
        {
            AcknowledgeReminder = false;
            Notes = new List<Note>(); 
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<Note> Notes { get; set; }
        public string Taggroup { get; set; } 
        public Nullable<DateTime> EventDateTime { get; set; }
        public Nullable<DateTime> ReminderDateTime { get; set; }
        public bool AcknowledgeReminder { get; set; }

    }
}
