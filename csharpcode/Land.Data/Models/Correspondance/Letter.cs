using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace Land.Data.Models
{
    public class Letter : CrudInfo
    {     

        public int Id { get; set; }
        
        public int SiteId { get; set; }
        public Site Site { get; set; }
       
        public String Subject { get; set; }
        public String Body { get; set; }
        public String Document { get; set; }
        public System.DateTime EventDate { get; set; }

        //follow up Action
        public Nullable<int> ActionId { get; set; }
        public virtual Action Action { get; set; }
                       
        public Nullable<int> DeliverTypeId { get; set; }
        public virtual DeliveryType Deliverby { get; set;} 

        public Nullable<int> TemplateId { get; set; }
        public virtual Template Template { get; set; }  
         
        public Nullable<int> ContactId { get; set; }
        public virtual Contact From { get; set; }

        public ICollection<Recipient> ToContacts { get; set; }                 
    }
}

