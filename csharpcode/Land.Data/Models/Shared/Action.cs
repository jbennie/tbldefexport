using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Land.Data.Models
{
    public class Action: CrudInfo
    {
        public int Id { get; set; }

        public System.DateTime AlertDate { get; set; }
        public String AlertComment { get; set; }

        public Nullable<int> AlertActionTypeId { get; set; }
        public virtual AlertActionType AlertAction { get; set; }      
    }
}
