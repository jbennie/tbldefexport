using System;
using System.Collections.Generic;

namespace Land.Data.Models
{
    public class PlanningAttachment:CrudInfo
    {
        public PlanningAttachment() 
        {
            FileReference = "test01";
            SearchWords = "";
        }
        public int Id { get; set; }
        public string FileReference { get; set; }
        public string SearchWords { get; set; }
        public int PlanningApplicationId { get; set; }
        public virtual PlanningApplication PlanningApplication { get; set; }
    }
}
