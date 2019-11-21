using SOIT.Data;
using SOIT.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOIT.Areas.HR.Controllers
{
    public class EmployeeController : Controller
    {
        public SOITEntities dbcontext;
        public EmployeeController()
        {
            dbcontext = new SOITEntities();
        }
        // GET: Employee
        public ActionResult Index()
        {

            List<employee> empdata = dbcontext.employee.ToList();
            return View(empdata);
        }

        public ActionResult Create()
        {
            EmployeeFamilyViewModel empdata = new EmployeeFamilyViewModel();

            //employee empdata = new employee();
            //familyDetail familydetail = new familyDetail();

            //commonViewModel emoployeedetails.emaploye = empdata;

            return View(empdata);
        }

        public ActionResult Update(int Id)
        {
            employee empdata = dbcontext.employee.Where(a => a.id == Id).FirstOrDefault();
            if (empdata == null)
            {
                return HttpNotFound();
            }
            
            return View("Create", empdata);



        }

        [HttpPost]
        public ActionResult Create(EmployeeFamilyViewModel empdata, HttpPostedFileBase photo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (empdata.id == 0) // create section
                    {
                        string photopath = Server.MapPath("~/UserImage");
                        if (!Directory.Exists(photopath))
                        {
                            Directory.CreateDirectory(photopath);
                        }
                        string[] imageName = photo.FileName.Split('.').ToArray();
                        string extension = imageName[(imageName.Length - 1)].ToString();
                        string fileName = DateTime.Now.ToString("yyMMddmmhhss") + "_" + photo.FileName + "." + extension;
                        string filePath = Path.Combine(photopath, fileName);
                        photo.SaveAs(filePath); //saving photo 
                        empdata.photo = fileName;
                        empdata.createdBy = User.Identity.Name;
                        empdata.createdDate = DateTime.Now;

                        employee newEmpData = new employee();
                        newEmpData.fullName = empdata.fullName;
                        newEmpData.dob = empdata.dob;
                        newEmpData.gender = empdata.gender;
                        newEmpData.maritalStatus = empdata.maritalStatus;
                        newEmpData.bloodGroup = empdata.bloodGroup;
                        newEmpData.photo = empdata.photo;
                        dbcontext.employee.Add(newEmpData);

                        dbcontext.SaveChanges();

                        foreach(var item in empdata.FamilyDetails)
                        {
                            familyDetail famData = new familyDetail();
                            famData.relationship = item.relationship;
                            famData.employeeId = empdata.id;
                            famData.name = item.name;
                            dbcontext.familyDetail.Add(famData);
                        }
                        //educaton
                        //
                        dbcontext.SaveChanges();
                        
                        //bool isCreated = this._userProfileService.CreateUserProfile(userprofiles);
                        return RedirectToAction("Index");
                    }

                    else //update section
                    {
                        employee previousRecord = dbcontext.employee.Where(a => a.id == empdata.id).AsNoTracking().FirstOrDefault();
                        //UserProfile previousRecord = this._userProfileService.GetUserProfileById(userprofiles.Id);
                        //check new file selected or not
                        if (photo != null && photo.ContentLength > 0)
                        {
                            string photosavingpath = Server.MapPath("~/UserImage/");
                            //delete old one
                            if (!string.IsNullOrEmpty(previousRecord.photo) && Directory.Exists(photosavingpath))
                            {
                                string fullphotopath = photosavingpath + previousRecord.photo;
                                System.IO.File.Delete(fullphotopath);
                            }
                            //save file
                            string[] imageName = photo.FileName.Split('.').ToArray();
                            string extension = imageName[(imageName.Length - 1)].ToString();
                            string fileName = DateTime.Now.ToString("yyMMddmmhhss") + "_" + photo.FileName + "." + extension;
                            string filePath = Path.Combine(photosavingpath, fileName);
                            photo.SaveAs(filePath); //saving photo 
                            empdata.photo = fileName;
                        }
                        else
                        {
                            empdata.photo = previousRecord.photo;
                        }
                        empdata.modifiedBy = empdata.modifiedBy + ";" + User.Identity.Name;

                        empdata.modifiedDate = empdata.modifiedDate + ";" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        //empdata.modifiedDate = empdata.modifiedDate;
                        //dbcontext.Entry<employee>(empdata).State = EntityState.Added;

                        //dbcontext.Entry<employee>(empdata).State = EntityState.Modified;

                        dbcontext.SaveChanges();

                        //bool isUpdated = this._userProfileService.UpdateUserProfile(userprofiles);
                        return RedirectToAction("Index");
                    }

                }
                //List<Province> provinceList = this._provinceServices.GetAllProvinces();
                //ViewBag.Province = new SelectList(provinceList, "Id", "Name");
                return View(empdata);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}