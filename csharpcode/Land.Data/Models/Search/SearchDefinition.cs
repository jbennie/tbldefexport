using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Land.Data.Models.Search
{
    public class SearchDefinition
    {
        public int Id {get; set;}
        public string PropertyClass {get; set;}
        public int  PlotNumber {get; set;}
        public string PlotSize {get; set;}     
    }
}
