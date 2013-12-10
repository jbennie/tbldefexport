using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Land.Data.Models
{
    public class Invoice : CrudInfo
    {
        public Invoice()
        {
        }

        public int Id { get; set; }
        public int SiteOrderId { get; set; }
        public int AccountId { get; set; }

        public string OtherReference { get; set; }
        public System.DateTime TaxDate { get; set; }
        public System.DateTime DueDate { get; set; }
        public decimal ValueExvat { get; set; }

     

        public virtual SiteOrder SiteOrder {get; set;}
        public virtual Account Account { get; set; }
    }
}
