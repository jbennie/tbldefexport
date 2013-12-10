using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Land.Data.Models
{
    public class Plot:CrudInfo
    {

        public Plot(): base()
        {
            plotcount = 1;
            IsSocialHousing = false;
            totalareasqm = 1;              
        }
        
        public int Id { get; set; }
        public int SiteId { get; set; }

        [Display(Name = "How many of the chosen plot type will be built on this plot?")]
        public int plotcount { get; set; }

        [Display(Name = "Plot Type")]
        public Nullable<int> LandPlotTypeId { get; set; }

        [Display(Name = "Is this plot for social housing?")]
        public bool IsSocialHousing { get; set; }

        [Display(Name = "Notes")]
        public string notes { get; set; }

        [Display(Name = "Reason")]
        public string reason { get; set; }

        [Display(Name = "Comment")]
        public string comment { get; set; }

        [Display(Name = "Total Square Meters")]
        public double totalareasqm { get; set; }
        
        public virtual Site Site { get; set; }
        public virtual LandPlotType LandPlotType { get; set; }
    }
}
