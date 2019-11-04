using SOIT.Data;
using SOIT.Repos.Infrastructure;
using SOIT.Repos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOIT.Repos.Repository
{
    public class UserQualificationRepo : EntityBaseRepository<UserQualification>, IUserQualificationRepo
    {
        public UserQualificationRepo(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
