using System;
using Land.Data.Contracts;
using Lincore.GenericInterfaces.EntityFramework; 
using Lincore.GenericClasses.EntityFramework;
using Lincore.GenericClasses.Helpers; 
using Land.Data;
using Land.Data.Models;


namespace Land.DataLayer
{
    public class LandUOW : ILandUOW, IDisposable
    { 
        
        // Context and RepoProvider. 
        protected SiteManagementContext DbContext { get; set; }
        protected IRepositoryProvider RepositoryProvider { get; set; }
                
        // Constructor 
        public LandUOW(IRepositoryProvider repositoryProvider)
        {
            CreateDbContext(); 

            repositoryProvider.DbContext = DbContext;
            RepositoryProvider = repositoryProvider; 
        }

        #region Internal Functions   
        //Context Setup
        protected void CreateDbContext()
        {
            DbContext = new SiteManagementContext(); 
            DbContext.Configuration.ProxyCreationEnabled = false;
            DbContext.Configuration.LazyLoadingEnabled = false;
            DbContext.Configuration.ValidateOnSaveEnabled = false;
        }

        // ==================== Private Functions ===========================
        private IRepository<T> GetStandardRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }

        private T GetRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }


        #endregion

        #region IUOW
        public void Commit()
        {
            DbContext.SaveChanges();
        }
        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }
        #endregion 

     
        // public ISitesRepository Sites { get { return GetRepo<ISitesRepository>(); } }

        // UOW Repositories in Scope 
        public IRepository<Town> Towns { get { return GetStandardRepo<Town>(); } }
        public IRepository<Site> Sites { get { return GetStandardRepo<Site>(); } }
        public IRepository<Plot> Plots { get { return GetStandardRepo<Plot>(); } }
        public IRepository<Contact> Contacts { get { return GetStandardRepo<Contact>(); }}

        public IRepository<Owner> Owners
        {
            get { return GetStandardRepo<Owner>(); }
        }
        public IRepository<Negotiator> Negotiators
        {
            get { return GetStandardRepo<Negotiator>(); }
        }
        public IRepository<Agent> Agents
        {
            get { return GetStandardRepo<Agent>(); }
        }

        public IRepository<LandPlanningType> LandPlanningTypes { get { return GetStandardRepo<LandPlanningType>(); } }
        public IRepository<LandFieldType> FieldTypes
        {
            get { return GetStandardRepo<LandFieldType>(); }
        }
        public IRepository<LandPriorityType> PriorityTypes
        {
            get { return GetStandardRepo<LandPriorityType>(); }
        }
        public IRepository<LandProjectType> ProjectTypes
        {
            get { return GetStandardRepo<LandProjectType>(); }
        }
        public IRepository<LandProbabilityType> ProbabilityTypes
        {
            get { return GetStandardRepo<LandProbabilityType>(); }
        }      
        public IRepository<LandRoleType> Roles
        {
            get { return GetStandardRepo<LandRoleType>(); }
        }
        public IRepository<LandStatusType> StatusTypes
        {
            get { return GetStandardRepo<LandStatusType>(); }
        }

        public IRepository<SiteValuation> Valuations
        {
            get { return GetStandardRepo<SiteValuation>(); }
        }
        public IRepository<SiteSaleOption> Options
        {
            get { return GetStandardRepo<SiteSaleOption>(); }
        }
        public IRepository<SiteOffer> Offers
        {
            get { return GetStandardRepo<SiteOffer>(); }
        }

    }
}
