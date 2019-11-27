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

            List<employee> empdata = dbcontext.employee.Where(a=>a.createdBy==User.Identity.Name).ToList();
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

        [HttpPost]
        public ActionResult Create(EmployeeFamilyViewModel empdata, HttpPostedFileBase photo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (empdata.id == 0) // create section
                    {
                        if (photo != null && photo.ContentLength > 0)
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
                        }

                        employee newEmpData = new employee();
                        newEmpData.fullName = empdata.fullName;
                        newEmpData.dob = empdata.dob;
                        newEmpData.gender = empdata.gender;
                        newEmpData.maritalStatus = empdata.maritalStatus;
                        newEmpData.bloodGroup = empdata.bloodGroup;
                        newEmpData.photo = empdata.photo;
                        newEmpData.createdBy = User.Identity.Name;
                        newEmpData.createdDate = DateTime.Now;
                        dbcontext.employee.Add(newEmpData);

                        dbcontext.SaveChanges();

                        foreach (var item in empdata.FamilyDetails)
                        {
                            familyDetail famData = new familyDetail();
                            famData.relationship = item.relationship;
                            famData.employeeId = newEmpData.id;
                            famData.name = item.name;
                            famData.occupation = item.occupation;
                            famData.dob = item.dob;
                            famData.createdBy = User.Identity.Name;
                            famData.createdDate = DateTime.Now;
                            dbcontext.familyDetail.Add(famData);
                        }
                        //educaton
                        //
                        foreach (var item in empdata.Experiences)
                        {
                            experience exp = new experience()
                            {
                                employeeId = newEmpData.id,
                                nameOfEmployer = item.nameOfEmployer,
                                empStatus = item.empStatus,
                                durationFrom = item.durationFrom,
                                durationTo = item.durationTo,
                                tenure = item.tenure,
                                status = true,
                                createdBy = User.Identity.Name,
                                createdDate = DateTime.Now
                            };
                            dbcontext.experience.Add(exp);
                        }
                        dbcontext.SaveChanges();

                        //bool isCreated = this._userProfileService.CreateUserProfile(userprofiles);
                        return RedirectToAction("Index");
                    }

                    else //update section
                    {
                        employee previousRecord = dbcontext.employee.Where(a => a.id == empdata.id).FirstOrDefault();
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
                            previousRecord.photo = fileName;
                        }
                        previousRecord.modifiedBy = empdata.modifiedBy + ";" + User.Identity.Name;
                        previousRecord.modifiedDate = empdata.modifiedDate + ";" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        dbcontext.Entry<employee>(previousRecord).State = EntityState.Modified;
                        //for family detail table
                        foreach (var fd in empdata.FamilyDetails)
                        {
                            familyDetail prevFamilyDetail = new familyDetail();
                            prevFamilyDetail = dbcontext.familyDetail.Find(fd.id);
                            prevFamilyDetail.name = fd.name;
                            prevFamilyDetail.relationship = fd.relationship;
                            prevFamilyDetail.occupation = fd.occupation;
                            prevFamilyDetail.dob = fd.dob;
                            dbcontext.Entry<familyDetail>(prevFamilyDetail).State = EntityState.Modified;
                        }

                        //for experience table
                        //first delete previous recorded experiences, then add current
                        List<experience> prevExperiences = dbcontext.experience.Where(a => a.employeeId == empdata.id).ToList();
                        dbcontext.experience.RemoveRange(prevExperiences);
                        
                        foreach (var item in empdata.Experiences)
                        {
                            experience exp = new experience()
                            {
                                employeeId = empdata.id,
                                nameOfEmployer = item.nameOfEmployer,
                                empStatus = item.empStatus,
                                durationFrom = item.durationFrom,
                                durationTo = item.durationTo,
                                tenure = item.tenure,
                                status = true,
                                createdBy = User.Identity.Name,
                                createdDate = DateTime.Now
                            };
                            dbcontext.experience.Add(exp);
                        }

                        dbcontext.SaveChanges();

                        //bool isUpdated = this._userProfileService.UpdateUserProfile(userprofiles);
                        return RedirectToAction("Index");
                    }

                }
                //List<Province> provinceList = this._provinceServices.GetAllProvinces();
                //ViewBag.Province = new SelectList(provinceList, "Id", "Name");
                return View(empdata);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult Update(int Id)
        {
            employee empdata = dbcontext.employee.Where(a => a.id == Id).FirstOrDefault();
            List<familyDetail> famdetail = dbcontext.familyDetail.Where(a => a.employeeId == Id).ToList();
            List<experience> experiences = dbcontext.experience.Where(a => a.employeeId == Id).ToList();
            if (empdata == null)
            {
                return HttpNotFound();
            }

            EmployeeFamilyViewModel empFamilyViewModel = new EmployeeFamilyViewModel()
            {
                id = empdata.id,
                fullName = empdata.fullName,
                gender = empdata.gender,
                dob = empdata.dob,
                email = empdata.email,
                bloodGroup = empdata.bloodGroup,
                cAddress = empdata.cAddress,
                FamilyDetails = famdetail,
                Experiences=experiences
            };


            return View("Create", empFamilyViewModel);
        }
    }
}