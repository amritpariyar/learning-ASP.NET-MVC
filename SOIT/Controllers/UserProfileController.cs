using SOIT.Models.Data;
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
                        string fileName = DateTime.Now.ToString("yyMMddmmhhss") + "_" + Photo.FileName + "." + extension;
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
                        UserProfile previousRecord = dbcontext.UserProfile.Where(a => a.Id == userProfile.Id).FirstOrDefault();
                        //check new file selected or not
                        if(Photo!=null && Photo.ContentLength > 0)
                        {
                            string photosavingpath = Server.MapPath("~/UserImage");
                            //delete old one
                            if(!string.IsNullOrEmpty(previousRecord.Photo) && Directory.Exists(photosavingpath))
                            {
                                System.IO.File.Delete(photosavingpath + previousRecord.Photo);
                            }                            
                            //save file
                            string[] imageName = Photo.FileName.Split('.').ToArray();
                            string extension = imageName[(imageName.Length - 1)].ToString();
                            string fileName = DateTime.Now.ToString("yyMMddmmhhss") + "_" + Photo.FileName + "." + extension;
                            string filePath = Path.Combine(photosavingpath, fileName);
                            Photo.SaveAs(filePath);
                            userProfile.Photo = fileName;
                        }
                        userProfile.ModifiedBy = userProfile.ModifiedBy + ";" + User.Identity.Name;
                        userProfile.ModifiedDate = userProfile.ModifiedDate + ";" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        dbcontext.Entry<UserProfile>(userProfile).State = EntityState.Added;
                        //dbcontext.Entry<UserProfile>(userProfile).State = EntityState.Modified;
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
    }
}