using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SOIT.Data;
using SOIT.Repos.Infrastructure;
using SOIT.Repos.Interface;
using SOIT.Repos.Repository;
//using SOIT.Models.Data;
using SOIT.Services;

namespace SOIT.Controllers
{
    public class ProvinceController : Controller
    {
        //public SOITEntities db;
        //ProvinceServices provinceServices;
        IEntityBaseRepository<Province> _proviceRepo;
        IUnitOfWork _unitOfWork;
        public ProvinceController(IEntityBaseRepository<Province> provinceRepo,IUnitOfWork unitOfWork)
        {
            this._proviceRepo = provinceRepo;
            this._unitOfWork = unitOfWork;
            //provinceServices = new ProvinceServices(db);
        }

        //private SOITEntities db = new SOITEntities();

        // GET: Province
        public ActionResult Index()
        {
            var provList = this._proviceRepo.All.ToList();
            return View(provList);

            //ProvinceRepo provRepo = new ProvinceRepo();
            //string systemTime = "";
            //List<Province> provList = provRepo._GetProvinceList();
            //var provinceList = provinceServices.GetAllProvince(); //remove later
            //return View(provList);

            //var provinceList = provinceServices.GetAllProvince();
            //return View(provinceList);
            
            //return View(db.Province.ToList());
        }

        // GET: Province/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Province province = this._proviceRepo.All.Where(a => a.Id == id).FirstOrDefault();
            //Province province = provinceServices.GetProvinceById(id);
            //Province province = db.Province.Find(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        // GET: Province/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Province/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Province province)
        {
            if (ModelState.IsValid)
            {
                this._proviceRepo.Add(province);
                this._unitOfWork.Commit();
                //bool isSaved = this.provinceServices.SaveProvince(province);
                //db.Province.Add(province);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(province);
        }

        // GET: Province/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Province province = db.Province.Find(id);
            //Province province = this.provinceServices.GetProvinceById(id);
            Province province = this._proviceRepo.All.Where(a => a.Id == id).FirstOrDefault();
            
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        // POST: Province/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Province province)
        {
            if (ModelState.IsValid)
            {
                this._proviceRepo.Edit(province);
                this._unitOfWork.Commit();
                //this.provinceServices.UpdateProvince(province);
                //db.Entry(province).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(province);
        }

        // GET: Province/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Province province = db.Province.Find(id);
            //Province province = this.provinceServices.GetProvinceById(id);
            Province province = this._proviceRepo.All.Where(a => a.Id == id).FirstOrDefault();
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        // POST: Province/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Province province = db.Province.Find(id);
            //db.Province.Remove(province);
            //db.SaveChanges();
            //this.provinceServices.DeleteProvince(id);
            Province province = this._proviceRepo.All.Where(a => a.Id == id).FirstOrDefault();
            this._proviceRepo.Delete(province);
            this._unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
