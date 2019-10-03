using SOIT.Models.Data;
using SOIT.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOIT.Controllers
{
    public class UserProfileController : Controller
    {
        SOITEntities dbcontext;
        public UserProfileController()
        {
            dbcontext = new SOITEntities();
        }
        // GET: UserProfile
        public ActionResult Index()
        {
            var userProfiles = dbcontext.UserProfile.ToList();
            return View(userProfiles);
        }

        public ActionResult Create()
        {
            ViewBag.Province = new SelectList(dbcontext.Province.ToList(), "Id", "Name");
            UserProfile userProfile = new UserProfile();
            return View(userProfile);
        }

        [HttpPost]
        public ActionResult Create(UserProfile userProfile,HttpPostedFileBase Photo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //create section
                    if (userProfile.Id == 0)
                    {
                        string photosavingpath = Server.MapPath("~/UserImage");
                        if (!Directory.Exists(photosavingpath))
                        {
                            Directory.CreateDirectory(photosavingpath);
                        }
                        //save file
                        string[] imageName = Photo.FileName.Split('.').ToArray();
                        string extension = imageName[(imageName.Length - 1)].ToString();
                        string fileName = DateTime.Now.ToString("yyMMddmmhhss") + "_" + Photo.FileName;
                        string filePath = Path.Combine(photosavingpath, fileName);
                        Photo.SaveAs(filePath);
                        //Photo.SaveAs(photosavingpath+"/"+ fileName);

                        userProfile.Photo = fileName;
                        userProfile.CreatedBy = User.Identity.Name;
                        userProfile.CreatedDate = DateTime.Now;


                        dbcontext.UserProfile.Add(userProfile);
                        dbcontext.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else //Update section
                    {
                        UserProfile previousRecord = dbcontext.UserProfile.Where(a => a.Id == userProfile.Id).AsNoTracking().FirstOrDefault();
                        //check new file selected or not
                        if (Photo != null && Photo.ContentLength > 0)
                        {
                            string photosavingpath = Server.MapPath("~/UserImage");
                            //delete old one
                            if (!string.IsNullOrEmpty(previousRecord.Photo) && Directory.Exists(photosavingpath))
                            {
                                System.IO.File.Delete(Path.Combine(photosavingpath , previousRecord.Photo));
                            }
                            //save file
                            string[] imageName = Photo.FileName.Split('.').ToArray();
                            string extension = imageName[(imageName.Length - 1)].ToString();
                            string fileName = DateTime.Now.ToString("yyMMddmmhhss") + "_" + Photo.FileName;
                            string filePath = Path.Combine(photosavingpath, fileName);
                            Photo.SaveAs(filePath);
                            userProfile.Photo = fileName;
                        }
                        userProfile.ModifiedBy = userProfile.ModifiedBy + ";" + User.Identity.Name;
                        userProfile.ModifiedDate = userProfile.ModifiedDate + ";" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        //dbcontext.Entry<UserProfile>(userProfile).State = EntityState.Added;
                        dbcontext.Entry<UserProfile>(userProfile).State = EntityState.Modified;
                        dbcontext.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                ViewBag.Province = new SelectList(dbcontext.Province.ToList(), "Id", "Name");
                return View(userProfile);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public ActionResult Edit(int Id)
        {
            //UserProfile userProfile = dbcontext.UserProfile.FirstOrDefault(a=>a.Id==Id);
            //UserProfile userProfile = dbcontext.UserProfile.Find(Id);
            //UserProfile userProfile = (from up in dbcontext.UserProfile
            //                           where up.Id == Id
            //                           select up);
            //string query = "select Id, Name from.......,.";
            //UserProfile userProfile = dbcontext.Database.SqlQuery<UserProfile>(query).FirstOrDefault();

            UserProfile userProfile = dbcontext.UserProfile.Where(a => a.Id == Id).FirstOrDefault();
            ViewBag.Province = new SelectList(dbcontext.Province.ToList(), "Id", "Name",userProfile.Province);
            return View("Create",userProfile);
        }

        [HttpPost]
        public JsonResult ChangeStatus(int Id)
        {
            try
            {
                UserProfile userProfile = dbcontext.UserProfile
                .Where(a => a.Id == Id)
                .FirstOrDefault();
                if (userProfile.Status == true)
                {
                    userProfile.Status = false;
                }
                else
                {
                    userProfile.Status = true;
                }

                dbcontext.Entry<UserProfile>(userProfile).State = EntityState.Modified;
                dbcontext.SaveChanges();
                return new JsonResult()
                {
                    Data = "success",
                    MaxJsonLength = Int32.MaxValue,
                    ContentType = "application/json",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                //return Json("sucess",JsonRequestBehavior.AllowGet);
                //return View();
            }
            catch (Exception ex)
            {
                return new JsonResult()
                {
                    Data = "failed",
                    MaxJsonLength = Int32.MaxValue,
                    ContentType = "application/json",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public JsonResult GetUsersQualification(int Id)
        {
            List<UserQualificationViewModel> userQualification = dbcontext
                 .UserQualification
                 .Where(t => t.UserProfileId == Id)
                 .Select(t => new UserQualificationViewModel()
                 {
                     Id = t.Id,
                     Title = t.Title,
                     Institution = t.Institution,
                     IsCertification = t.IsCertification.Value,
                     IsEducation = t.IsEducation.Value,
                     ReceiveDate = t.ReceiveDate
                 }).ToList();


            //var userQualification = dbcontext
            //     .UserQualification
            //     .Where(t => t.UserProfileId == Id)
            //     .Select(t => new {
            //         t.Id,
            //         t.Title,
            //         t.Institution,
            //         t.IsCertification,
            //         t.IsEducation,
            //         t.ReceiveDate
            //     })
            //     .ToList();

            return new JsonResult()
            {
                Data = userQualification,
                ContentType = "application/json",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        [HttpPost]
        public JsonResult SaveUserQualification(int userProfileId, string title, string institution, string receiveDate,string quali_certi)
        {
            UserQualification qualification = new UserQualification();
            qualification.UserProfileId = userProfileId;
            qualification.Title = title;
            qualification.Institution = institution;
            qualification.ReceiveDate = receiveDate;
            if (quali_certi == "Q")
            {
                qualification.IsEducation = true;
            }
            else
            {
                qualification.IsEducation = false;
            }
            if (quali_certi == "C")
            {
                qualification.IsCertification = true;
            }
            else
            {
                qualification.IsCertification = false;
            }
            qualification.CreatedBy = User.Identity.Name;
            qualification.CraetedDate = DateTime.Now;
            dbcontext.UserQualification.Add(qualification);
            dbcontext.SaveChanges();
            
            return new JsonResult()
            {
                Data = qualification,
            };
        }
    }
}