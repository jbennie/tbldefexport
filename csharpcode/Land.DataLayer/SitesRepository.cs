using System;
using System.Collections.Generic;
using System.Linq;
using Land.Data.Models; 
using Land.Data.Contracts; 
using Lincore.GenericClasses.Helpers; 
using Lincore.GenericClasses.EntityFramework;
using System.Data.Entity; 


namespace Land.DataLayer.Repos
{
    public class SitesRepository : TBaseRepository<Site>, ISitesRepository
    {

        public SitesRepository(DbContext context)
            : base(context)
        {
            // do nothing. 
        }

        public IQueryable<Site> GetSite()
        {
            return DbSet.Select(s => new Site
            {
               Id = s.Id
            }); 
        }

    }
}
