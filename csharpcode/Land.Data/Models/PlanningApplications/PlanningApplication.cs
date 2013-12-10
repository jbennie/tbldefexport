using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Land.Data.Models
{
    public class PlanningApplication:CrudInfo
    {
        public PlanningApplication()
        {
            this.PlanningApprovals = new List<PlanningApproval>();
            this.PlanningAttachments = new List<PlanningAttachment>();
     //       this.Letters = new List<Letter>();
        }

        public int Id { get; set; }
        public int SiteId { get; set; }
     
        //  public int ContactId { get; set; } 
        public Nullable<int> PlanningOfficeId { get; set; }
        public Nullable<int> LandPlanningTypeId { get; set; }

        [Display(Name = "Planning Description")]
        public string PlanningDescription { get; set; }

        [Display(Name = "Planning Application Date")]
        public System.DateTime ApplicationDate { get; set; }
        
        [Display(Name = "App Submition Date")]
        public Nullable<System.DateTime> ApplicationSubmitionDate { get; set; }

        [Display(Name = "Committer Meeting Date")]
        public Nullable<System.DateTime> CommitteeMeetingDate { get; set; }
        
        public virtual PlanningOffice PlanningOffice {get; set;}
        public virtual LandPlanningType LandPlanningType { get; set; }
        public virtual Site Site { get; set; }
        public virtual ICollection<PlanningApproval> PlanningApprovals { get; set; }
        public virtual ICollection<PlanningAttachment> PlanningAttachments { get; set; }
     //   public virtual ICollection<Letter> Letters { get; set; }
    }
}
