using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Land.Data.Models
{
    public class SiteOffer : CrudInfo
    {
        public class SiteOfferConfiguration : EntityTypeConfiguration<SiteOffer>
        {
            public SiteOfferConfiguration()
            {
                //  this.HasOptional<SiteExchange>(so => so.SiteExchange)
                //    .WithOptionalDependent(se => se.SiteOffer)
                //    .Map(p => p.MapKey("SiteExchangeId"));                    
            }
        }

        public SiteOffer()
        {
        }

        public int Id { get; set; }
        public int SiteId { get; set; }

        [Display(Name = "Date of Offer")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public System.DateTime OfferDate { get; set; }

        [Display(Name = "Offer Amount")]
        public decimal OfferAmount { get; set; }


        [Display(Name = "Offer Units")]
        public int OfferUnits { get; set; }

        [Display(Name = "Offer Notes")]
        public string OfferNotes { get; set; }

        [Display(Name = "Offer is ")]
        public Nullable<int> ConditionalTypeId { get; set; }
        public virtual ConditionalType ConditionalType { get; set; }
        
        [Display(Name = "Condition Notes")]
        public string ConditionalNotes { get; set; }

        [Display(Name = "Offer Decission")]
        public Nullable<int> DecissionTypeId { get; set; }
        public virtual DecissionType DecissionType { get; set; }

        [Display(Name = "Offer Decission Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> OfferDecisionDate { get; set; }

        [Display(Name = "Offer is subject to ")]
        public Nullable<int> OfferSubjecttoTypeID { get; set; }
        public virtual OfferSubjecttoType OfferSubjecttoType { get; set; }

        public virtual ICollection<SiteExchange> SiteExchanges { get; set; }
        public virtual Site Site { get; set; }
    }
}
