using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Land.Data.Models
{
    public class Log
    {

        public Log() 
        {

        }

        [Key]      
        public int Id{get; set;}
              
        [Required]
        DateTime Date{get; set;}
        
        [MaxLength(255), Required]
        public string Thread{get; set;}

        [MaxLength(50), Required]
        public string Level{get; set;}

        [MaxLength(255), Required]
        public string Logger{get; set;}

        [MaxLength(4000), Required]
        public string Message{get; set;}

        [MaxLength(2000) ]               
        public string Exception {get; set;}
    }
}
