using System;
using System.Collections.Generic;
using System.Data.Entity;
using Lincore.GenericClasses.EntityFramework; 


namespace Lincore.GenericClasses.Helpers
{
    public abstract class TRepositoryFactories
    {
        private readonly IDictionary<Type, Func<DbContext, object>> _repositoryFactories;

        /// <summary>
        /// Overide this with the actual GetFactories for the implementation.
        /// </summary>
        /// <returns></returns>
        protected virtual IDictionary<Type, Func<DbContext, object>> GetFactories()
        {
            return new Dictionary<Type, Func<DbContext, object>>
                {
                 //  {typeof(IAttendanceRepository), dbContext => new AttendanceRepository(dbContext)},
                 //  {typeof(IPersonsRepository), dbContext => new PersonsRepository(dbContext)},
                 //  {typeof(ISessionsRepository), dbContext => new SessionsRepository(dbContext)},
                };
        }

        /// <summary>
        /// Initialise the Repository
        /// </summary>
        public TRepositoryFactories()
        {
            _repositoryFactories = GetFactories(); 
        }

        /// <summary>
        /// Inject the factories
        /// </summary>
        /// <param name="factories"></param>
        public TRepositoryFactories(IDictionary<Type, Func<DbContext, object>> factories )
        {
            _repositoryFactories = factories;
        }


        public Func<DbContext, object> GetRepositoryFactory<T>() 
        {
            Func<DbContext, object> factory;
            _repositoryFactories.TryGetValue(typeof(T), out factory);
            return factory; 
        }


        public virtual Func<DbContext, object> DefaultRepositoryFactory<T>() where T : class
        {
            return dbContext => new TBaseRepository<T>(dbContext); 
        }

        public Func<DbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            return GetRepositoryFactory<T>() ?? DefaultRepositoryFactory<T>(); 
        }



    }
}
