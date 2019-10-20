using SOIT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SOIT.Services
{
    public class ProvinceServices
    {
        public SOITEntities db;
        public ProvinceServices(SOITEntities db)
        {
            this.db = db;
          //  db = new SOITEntities();
        }

        public List<Province> GetAllProvince()
        {
            List<Province> provinceList = db.Province.ToList();
            return provinceList;
        }

        public Province GetProvinceById(int? id)
        {
            Province province = db.Province.Find(id);
            return province;
        }

        public bool SaveProvince(Province province)
        {
            try
            {
                db.Province.Add(province);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void UpdateProvince(Province province)
        {
            db.Entry(province).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteProvince(int id)
        {
            Province province = db.Province.Find(id);
            db.Province.Remove(province);
            db.SaveChanges();
        }
    }
}
