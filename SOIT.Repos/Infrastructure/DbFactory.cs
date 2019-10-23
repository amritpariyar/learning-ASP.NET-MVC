using SOIT.Data;
using SOIT.Repos;

namespace SOIT.Repos.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        SOITEntities dbContext;
        public SOITEntities Init()
        {
            return dbContext ?? (dbContext = new SOITEntities());
        }

        protected override void DisposeCore()
        {
            if(dbContext!=null)
            {
                dbContext.Dispose();
            }
        }        
    }
}
