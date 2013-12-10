using System;
using System.Collections.Generic;
using System.Data.Entity;
using Lincore.GenericClasses.EntityFramework;
using Lincore.GenericClasses.Helpers;
using Land.Data.Contracts;
using Land.DataLayer.Repos;

 


namespace Land.DataLayer.Helpers
{
    public class LandRepositoryFactory : TRepositoryFactories
    {
        protected override IDictionary<Type, Func<DbContext, object>> GetFactories()
        {
            return new Dictionary<Type, Func<DbContext, object>>
            {
                  {typeof(ISitesRepository), dbContext => new SitesRepository(dbContext)}
                //,  {typeof(IPersonsRepository), dbContext => new PersonsRepository(dbContext)}
                //,  {typeof(ISessionsRepository), dbContext => new SessionsRepository(dbContext)}
            };
        }

        public LandRepositoryFactory()
            : base()
        {
        }

        public LandRepositoryFactory(IDictionary<Type, Func<DbContext, object>> factories)
            : base(factories)
        {

        }

    }
}
