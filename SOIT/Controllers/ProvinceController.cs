using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SOIT.Data;
//using SOIT.Models.Data;
using SOIT.Services;

namespace SOIT.Controllers
{
    public class ProvinceController : Controller
    {
        public SOITEntities db;
        ProvinceServices provinceServices;
        public ProvinceController()
        {
            provinceServices = new ProvinceServices(db);
        }

        //private SOITEntities db = new SOITEntities();

        // GET: Province
        public ActionResult Index()
        {
            var provinceList = provinceServices.GetAllProvince();
            return View(provinceList);
            
            //return View(db.Province.ToList());
        }

        // GET: Province/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Province province = provinceServices.GetProvinceById(id);
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
                bool isSaved = this.provinceServices.SaveProvince(province);
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
            Province province = this.provinceServices.GetProvinceById(id);
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
                this.provinceServices.UpdateProvince(province);
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
            Province province = this.provinceServices.GetProvinceById(id);
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
            this.provinceServices.DeleteProvince(id);
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
