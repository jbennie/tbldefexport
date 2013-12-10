using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Land.Data.Models
{
   
    public class SiteExchange: CrudInfo
    {
        public SiteExchange()
        {
        }

        public int Id { get; set; }

        [Display(Name = "Sale Start Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> SaleStartDate { get; set; }

        [Display(Name = "STC Agreed Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> AgreedSTCDate { get; set; }

        [Display(Name = "Projected Exchange Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ProjectedExchangeDate { get; set; }

        [Display(Name = "Projected Completion Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ProjectedCompletionDate { get; set; }

        [Display(Name = "Actual Exchange Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ActualExchangeDate { get; set; }

        [Display(Name = "Actual Completion Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ActualCompletionDate { get; set; }

        [Display(Name = "Searches Submitted Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> SearchesSubmitedDate { get; set; }

        [Display(Name = "Preliminary Equiry Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> PreliminaryEnquiryDate { get; set; }

        [Display(Name = "Draft Contract Submitted Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DraftContractSubmitedDate { get; set; }
        
        [Display(Name = "Projected Release Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ProjectedReleaseDate { get; set; }

        [Display(Name = "Instruction Confirmed Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> InstructionConfirmedOnDate { get; set; }

        [Display(Name = "Commission value")]
        public Nullable<double> Commission { get; set; }

        [Display(Name = "Fee %")]
        public Nullable<double> NettFeePercent { get; set; }

        [Display(Name = "Condition of Exchange")]
        public String ConditionsOfExchange { get; set; }
        
        public Nullable<int> PurchaserId { get; set; }
        public Nullable<int> VendorSolicitorId { get; set; }
        public Nullable<int> PurchaserSolicitorId { get; set; }
        public Nullable<int> BuyerId { get; set; }
             
        public virtual Purchaser Purchaser { get; set; }
        public virtual VendorSolicitor VendorSolicitor { get; set; }
        public virtual PurchaserSolicitor PurchaserSolicitor { get; set; }      
        public virtual Buyer Buyer { get; set; }

       //Site Offer May be an optional step in the process. 
        public Nullable<int> SiteOfferId { get; set; }
        public SiteOffer SiteOffer { get; set; }

        [Required]
        public int SiteId { get; set; }
        public Site Site { get; set; }
    }
   
}
