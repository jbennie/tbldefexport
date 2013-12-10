using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Land.Data.Models
{
    public class PlanningApproval: CrudInfo
    {
        public int Id { get; set; } 
        public int PlanningApplicationId { get; set; } 
        
        public Nullable<int> LandPlanningApprovalStateTypeId { get; set; }
        public string ReferenceNumber { get; set; }
        public string OfficerRecommendation { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> RejectionDate { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> AppealDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FullApprovalDate { get; set; }

       
        
        public virtual LandPlanningApprovalStateTypes LandPlanningApprovalStateType { get; set; }

        public virtual PlanningApplication PlanningApplication { get; set; }
    }
}
