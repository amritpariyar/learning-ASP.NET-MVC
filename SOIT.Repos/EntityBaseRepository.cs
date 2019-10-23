using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using SOIT.Repos.Interface;
using SOIT.Data;
using SOIT.Repos.Infrastructure;

namespace SOIT.Repos.Repository
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class
    {
        private SOITEntities dataContext;
        private IDbSet<T> dbset;
        public EntityBaseRepository(IDbFactory dbFactory)
        {
            this.DbFactory= dbFactory;
            dbset = DbContext.Set<T>();
        }
        protected IDbFactory DbFactory { get;private set; }

        protected SOITEntities DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }

       
        public IQueryable<T> All
        {
            get
            {
                return GetAll();
            }
        }

        public void Add(T Entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry<T>(Entity);
            dbset.Add(Entity);
        }

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public void Delete(T Entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry<T>(Entity);
            dbset.Remove(Entity);
        }

        public void Edit(T Entity)
        {            
            DbEntityEntry dbEntityEntry = DbContext.Entry<T>(Entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }
    }
}
