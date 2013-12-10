using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lincore.GenericInterfaces.EntityFramework;
using Land.Data.Models; 


namespace Land.Data.Contracts
{
    public interface ISitesRepository 
    {
        IQueryable<Site> GetSite(); //example.  
    }
}
