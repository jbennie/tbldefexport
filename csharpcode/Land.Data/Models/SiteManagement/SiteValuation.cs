using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Land.Data.Models
{
    public class SiteValuation:CrudInfo
    {
        public SiteValuation()
        {
            RTM = true;
        }

        public int Id { get; set; }
        public int SiteID { get; set; }

        public string Description { get; set; }

        [Display(Name = "Guide Price")]
        public Nullable<decimal> GuidePrice { get; set; }
        [Display(Name = "Projected GDV")]
        public Nullable<decimal> ProjectedGDV { get; set; }

        [Display(Name = "Valued on")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ValuationDate { get; set; }
        [Display(Name = "Valued by")]
        public String ValuationBy { get; set; }

        [Display(Name = "Right to Market")]
        public bool RTM { get; set; }

        [Display(Name = "RTM %")]
        public Nullable<decimal> RTMPercent { get; set; }

        [Display(Name = "RTM Commission")]
        public Nullable<decimal> RTMCommission { get; set; }
        
        [Display(Name = "Final Purchase Price")]
        public Nullable<decimal> FinalPurchasePrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]

        [Display(Name = "Purchase Date")]
        public Nullable<System.DateTime> PurchaseDate { get; set; }


        public virtual Site Site { get; set; }
    }
}
