using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Land.Data.Models
{
    public class SiteSaleOption:CrudInfo
    {
        public int Id { get; set; }  
        public int SiteId { get; set; }
        
        public string Description { get; set; }
        public string Reference { get; set; }
       
        public double Duration { get; set; }
        public Nullable<decimal> Premium { get; set; }
        public Nullable<decimal> MVPercent { get; set; }
        public Nullable<decimal> Fee { get; set; }
        public Nullable<decimal> Commission { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> OptionStartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> OptionExpiryDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ContractLongStopDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ProjectedLongStopDate { get; set; }

        public virtual Site Site { get; set; }
    }
}
