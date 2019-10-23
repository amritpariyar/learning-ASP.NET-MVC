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
    public class ProvinceRepo : EntityBaseRepository<Province>,IProvinceRepo
    {
        public ProvinceRepo(IDbFactory dbFactory):base(dbFactory)
        {

        }
        //SOITEntities db = new SOITEntities();
        //private SOITEntities db;
        //public ProvinceRepo()
        //{
        //    db = new SOITEntities();
        //}
        //public List<Province> _GetProvinceList()
        //{
        //    var provinceList = db.Province.ToList();
        //    return provinceList;
        //}

        //public string _GetSystemTime()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
