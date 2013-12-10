using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Land.Data.Models
{
    public class Template:CrudInfo
    {
        public Template()
        {
        }

        public int Id { get; set; }
               
        /// <summary>
        /// short name of template
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Template Markup
        /// </summary>
        public string Markup { get; set; }
       
        /// <summary>
        /// Template Group
        /// </summary>
        public Nullable<int> TemplateTypeId { get; set; }
        public virtual TemplateType TemplateType { get; set; }
    }
}
