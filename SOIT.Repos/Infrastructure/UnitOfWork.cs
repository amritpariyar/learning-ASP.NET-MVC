using SOIT.Data;
using SOIT.Repos.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banijya.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private SOITEntities _dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this._dbFactory = dbFactory;
        }

        protected SOITEntities DbContext
        {
            get { return _dbContext ?? (_dbContext = _dbFactory.Init()); }
        }
        public void Commit()
        {
            using (var dbContextTransaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    DbContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }                
        }
    }
}
