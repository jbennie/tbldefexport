using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WebMatrix.WebData.Resources;

namespace Land.Data.Models
{
    public abstract class CrudInfo
    {
        public CrudInfo() 
        {        
        }
       
        public string ChangedBy { get; set; }
        public string CreatedBy { get; set; }

       // [DataType(DataType.Date)]
        public System.DateTime Modified { get; set; }

       // [DataType(DataType.Date)]
        public System.DateTime Created { get; set; }


        public void SetCreatedInfo()        
        {
            if (WebMatrix.WebData.WebSecurity.Initialized)
            {
                CreatedBy = WebMatrix.WebData.WebSecurity.CurrentUserName;
                ChangedBy = WebMatrix.WebData.WebSecurity.CurrentUserName;
            }
            else 
            {
                CreatedBy = "system";
                ChangedBy = "system"; 
            }
            Created = DateTime.Now;
            Modified = DateTime.Now; 
        }

        public void UpdatedCreatedInfo()
        {
            if (WebMatrix.WebData.WebSecurity.Initialized)
            {
                ChangedBy = WebMatrix.WebData.WebSecurity.CurrentUserName;
            }
            else 
            {
                ChangedBy = "system"; 
            }
            Modified = DateTime.Now;
        }


      
    }
}
