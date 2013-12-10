using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Lincore.GenericInterfaces.EntityFramework;

namespace Lincore.GenericClasses.EntityFramework
{
    public class TBaseRepository<T> : IRepository<T> where T : class
    {
        // ============== Consts ==================================
        // ============== Variables and Properties ================
        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        // ============== Constructors ============================
        public TBaseRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext is null");

            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        // ============== Methods =================================
        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }
        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }
        public virtual void Add(T entity)
        {

            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }
        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }
        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return; // Assume already deleted
            Delete(entity);
        }
    }
}
