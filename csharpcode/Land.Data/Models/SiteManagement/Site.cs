using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Text; 



namespace Land.Data.Models
{
    public class Site:CrudInfo
    {

       
    public class SiteConfiguration : EntityTypeConfiguration<Site>
    {
        public SiteConfiguration()
        {
          //  this.HasOptional(s => s.Negotiator); 
        }
    }

        public Site()
        {
            this.Plots = new List<Plot>();
            this.SiteValuations = new List<SiteValuation>();
            this.PlanningApplications = new List<PlanningApplication>();
            this.SiteOffers = new List<SiteOffer>();
            this.SiteSaleOptions = new List<SiteSaleOption>();
            this.SiteOrders = new List<SiteOrder>();
            this.SiteExchanges = new List<SiteExchange>(); 
           
            SiteCreatedDate = DateTime.Now;
       }

        [Key]
        [Display(Name="Site Number")]
        public int Id { get; set; }
        public int referenceid { get; set; } // used for historical id.

        [Required]
        [Display(Name="Site Name")]
        public string Name { get; set; }

        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
       // [DataType(DataType.Date)]
        public System.DateTime SiteCreatedDate { get; set; }

       
        [Display(Name = "Line One")]
        public string AddressLineOne { get; set; }
        [Display(Name = "Line Two")]
        public string AddressLineTwo { get; set; }
        [Display(Name = "Line Three")]
        public string AddressLineThree { get; set; }
        [Display(Name = "County")]
        public string AddressCounty { get; set; }
        [Display(Name = "Country")]
        public string AddressCountry { get; set; }
        [Display(Name = "Postcode")]
        public string AddressPostcode { get; set; }

        public string  Londitude { get; set; }
        public string  Latitude { get; set; }

        [Display(Name = "Site Has Planning")]
        public Boolean Hasplanning { get; set; }

        [Display(Name = "Dead File Number")]
        public string  DeadFileNumber { get; set; }
        [Display(Name = "Dead File Box Number")]
        public string  DeadFileBoxNumber { get; set; }
       
        [Display(Name = "Negotiator")]
        public Nullable<int> NegotiatorId { get; set; }
        
        [Display(Name = "Agent")]
        public Nullable<int> AgentId { get; set; }
        
        [Display(Name = "Owner")]
        public Nullable<int> OwnerId { get; set; }
      
        [Display(Name = "Town")]
        public Nullable<int> TownId { get; set; }


        
       // public string CurrentUseOfLand { get; set; }

        
        public Nullable<int> LandCurrentuseTypeId { get; set; }
        
        public Nullable<int> LandFieldTypeId { get; set; }
     
        public Nullable<int> LandProjectTypeId { get; set; }
        
        public Nullable<int> LandProbabilityTypeId { get; set; }
       
        public Nullable<int> LandPriorityTypeId { get; set; }
        
        public Nullable<int> LandStatusTypeId { get; set; }        
    
        public Nullable<int> LandSummaryTypeId { get; set; }

        public virtual Negotiator Negotiator { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual Agent Agent { get; set; }
        public virtual Town Town { get; set; }
          
        public virtual ICollection<Plot> Plots { get; set; } 
        public virtual ICollection<SiteValuation> SiteValuations { get; set; }  
        public virtual ICollection<PlanningApplication> PlanningApplications { get; set; }
        public virtual ICollection<SiteOffer> SiteOffers { get; set; }
        public virtual ICollection<SiteSaleOption> SiteSaleOptions { get; set; }
        public virtual ICollection<SiteOrder> SiteOrders { get; set; }
        public virtual ICollection<SiteExchange> SiteExchanges { get; set; }
       // public virtual ICollection<Letter> Letters { get; set; }
        public virtual ICollection<Greensheet> Greensheets { get; set; }

        [Display(Name = "Current Use of Land")]
        public virtual LandCurrentuseType LandCurrentuseType { get; set; }
        [Display(Name = "Field Type")]
        public virtual LandFieldType LandFieldType { get; set; }
           [Display(Name = "Project Type")]
        public virtual LandPriorityType LandPriorityType { get; set; }
        [Display(Name = "Probability")]
        public virtual LandProbabilityType LandProbabilityType { get; set; }
         [Display(Name = "Priority")]
        public virtual LandProjectType LandProjectType { get; set; }
        [Display(Name = "Status")]
        public virtual LandStatusType LandStatusType { get; set; } 
        
        [Display(Name = "Summary")]
        public virtual LandSummaryType LandSummaryType { get; set; }

        #region Address Functions 
         
        public string ReportAddress { get { return GetReportAddress(); } }

        public string GetReportAddress() 
        {

            StringBuilder sb = new StringBuilder();

            appendString(this.AddressLineOne, ref sb);
            appendString(this.AddressLineTwo, ref sb);
            appendString(this.AddressLineThree, ref sb);
            appendString(this.AddressCounty, ref sb);
            appendString(this.AddressPostcode, ref sb);
            appendString(this.AddressCountry, ref sb);

            return sb.ToString(); 
        }


        private void appendString(String s, ref StringBuilder sb)
        {
            if (!String.IsNullOrEmpty(s))
            {
                if (!String.IsNullOrEmpty(sb.ToString()))
                {
                    sb.Append(" ,");
                }
                sb.Append(s);
            }
        }

        #endregion

        #region  SearchScore

        /// <summary>
        /// used when searching, no need to store in the db, the value is replaced on each search. 
        /// </summary>
        public int StoredScore = 0;
     
        /// <summary>
        /// Calculate the generic score
        /// </summary>
        /// <param name="filterstr"></param>
        /// <returns></returns>
        public int CalcScore(ref string filterstr)
        {
            int score = 0;

            score += DataUtilities.getscore(Name, ref filterstr);
            score += DataUtilities.getscore(AddressLineOne, ref filterstr);
            score += DataUtilities.getscore(AddressLineTwo, ref filterstr);
            score += DataUtilities.getscore(AddressLineThree, ref filterstr);
            score += DataUtilities.getscore(Id.ToString(), ref filterstr);
            score += DataUtilities.getscore(DeadFileBoxNumber, ref filterstr);
            score += DataUtilities.getscore(DeadFileNumber, ref filterstr);

            StoredScore = score; 
            return score;
        }


        /// <summary>
        /// Find reasons to include and item in the result set
        /// if a valid string is passed, include items that do match the string. 
        /// if and invalid string is passed, include all results. 
        /// </summary>
        /// <param name="filterstr"></param>
        /// <returns></returns>
        public bool FilterIn(string infilterstr) 
        {
           
            if (string.IsNullOrEmpty(infilterstr)) return true; 

            string filterstr = infilterstr.ToLower(); 
            int score = 0;

            score += CalcScore(ref filterstr);  

            if (Agent != null)
            {
                score += Agent.CalcScore(ref filterstr);
            }

            if(Owner != null)
            {
                score += Owner.CalcScore(ref filterstr);
            }

            if (Town != null){
                score += Town.CalcScore(ref filterstr);
            }

            if (LandStatusType != null)
            {
                score += LandStatusType.CalcScore(ref filterstr); 
            }

            if (LandFieldType != null)
            {
                score += LandStatusType.CalcScore(ref filterstr);
            }
            
            if (score >= 1)
            {
                return true;
            }          

            return false;
        }
        #endregion
    }
}
