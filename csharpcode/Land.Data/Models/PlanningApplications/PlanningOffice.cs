using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Land.Data.Models
{
    public class PlanningOffice:CrudInfo
    {
        public PlanningOffice() 
        {
           
        }
        [Key]
        public int Id { get; set; }  
       
        [MaxLength(50)]
        public string Name { get; set; }

        
        public Nullable<int> TownId { get; set; }
        public Nullable<int> PlanningContactId { get; set; }
      
        public virtual PlanningContact PlanningContact { get; set; }
        public virtual Town Town { get; set; }
    }
}
