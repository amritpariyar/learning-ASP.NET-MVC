using SOIT.Data;
using SOIT.Data.ViewModels;
using SOIT.Repos.Infrastructure;
using SOIT.Repos.Interface;
using SOIT.Repos.Repository;
using SOIT.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOIT.Controllers
{
    //[Authorize]
    //[RequestFilter]
    public class UserProfileController : Controller
    {
        //public SOITEntities db;
        ////instead of directly accessing db here;
        //// we will use service UserProfileService;
        //// declare service used within this controller.
        //public UserProfileService _userProfileService;
        //public ProvinceServices _provinceServices;
        //public UserProfileController()
        //{
        //    db = new SOITEntities();
        //    _userProfileService = new UserProfileService(db);
        //    _provinceServices = new ProvinceServices(db);
        //}

        IEntityBaseRepository<UserProfile> _userProfileRepo;
        IEntityBaseRepository<UserQualification> _userQualificationRepo;
        IEntityBaseRepository<Province> _proviceRepo;
        IUnitOfWork _unitOfWork;
        public UserProfileController(IEntityBaseRepository<UserProfile> _userProfileRepo,IEntityBaseRepository<UserQualification> _userQualificationRepo,IEntityBaseRepository<Province> _proviceRepo,
        IUnitOfWork _unitOfWork)
        {
            this._userProfileRepo = _userProfileRepo;
            this._userQualificationRepo = _userQualificationRepo;
            this._proviceRepo = _proviceRepo;
            this._unitOfWork = _unitOfWork;
        }

        // GET: UserProfile
        public ActionResult Index()
        {
            List<UserProfile> userProfiles = this._userProfileRepo.All.ToList();
            return View(userProfiles);

            //UserProfileRepo profileRepo = new UserProfileRepo(new IDbFactory());
            //List<UserProfile> userProfiles = profileRepo.All.ToList();
            //string systemTime = "";

            //UserProfileRepo userProfileRepo = new UserProfileRepo();
            //List<UserProfile> userProfiles = userProfileRepo._GetAllUseProfile();
            //return View(userProfiles);

            //List<UserProfile> userProfiles = this._userProfileService.GetAllUserProfiles();
            //return View(userProfiles);

            //var userProfiles = dbcontext.UserProfile.ToList();
            //userProfiles = userProfiles
            //    .Where(a => !a.IsDeleted.HasValue || (a.IsDeleted.HasValue && !a.IsDeleted.Value))
            //    .ToList();
            //return View(userProfiles);

            //List<UserProfile> nonDeletedRecords = new List<UserProfile>();
            //foreach(var item in userProfiles)
            //{
            //    if (!item.IsDeleted.HasValue || !item.IsDeleted.Value)
            //        nonDeletedRecords.Add(item);
            //}
            //userProfiles.ForEach(a =>
            //{
            //    if (!a.IsDeleted.HasValue || !a.IsDeleted.Value)
            //        nonDeletedRecords.Add(a);

            //});
            //return View(nonDeletedRecords);

        }

        //[Authorize(Roles ="HR",Users ="amrit")]
        public ActionResult Create()
        {
            //ViewBag.Province = new SelectList(dbcontext.Province.ToList(), "Id", "Name");
            //as we need province list here, we user provinceService
            //List<Province> provinceList = this._provinceServices.GetAllProvince();
            //ViewBag.Province = new SelectList(provinceList, "Id", "Name");
            //UserProfile userProfile = new UserProfile();
            //return View(userProfile);

            List<Province> provinceList = this._proviceRepo.All.ToList();
            ViewBag.Province = new SelectList(provinceList, "Id", "Name");
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

                        //bool isCreated= this._userProfileService.CreateUserProfile(userProfile);                        
                        //return RedirectToAction("Index");

                        this._userProfileRepo.Add(userProfile);
                        this._unitOfWork.Commit();
                        return RedirectToAction("Index");
                    }
                    else //Update section
                    {
                        // first get previouse recocrd , so create fetching method on service first.
                        //UserProfile previousRecord = this._userProfileService.GetUserProfileById(userProfile.Id);
                        UserProfile previousRecord = this._userProfileRepo.All.Where(a=>a.Id==userProfile.Id).FirstOrDefault();
                        
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

                        //bool isUpdated = this._userProfileService.UpdateUserProfile(userProfile);
                        this._userProfileRepo.Edit(userProfile);
                        
                        return RedirectToAction("Index");
                    }
                }
                //var provinceList = this._provinceServices.GetAllProvince();
                var provinceList = this._proviceRepo.All.ToList();
                ViewBag.Province = new SelectList(provinceList, "Id", "Name");
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

            //UserProfile userProfile = dbcontext.UserProfile.Where(a => a.Id == Id).FirstOrDefault();
            //UserProfile userProfile = this._userProfileService.GetUserProfileById(Id);
            //var provinceList = this._provinceServices.GetAllProvince();
            //ViewBag.Province = new SelectList(provinceList, "Id", "Name",userProfile.Province);
            //return View("Create",userProfile);

            UserProfile userProfile = this._userProfileRepo.All.Where(a=>a.Id==Id).FirstOrDefault();
            var provinceList = this._proviceRepo.All.ToList();
            ViewBag.Province = new SelectList(provinceList, "Id", "Name", userProfile.Province);
            return View("Create", userProfile);
        }

        [HttpPost]
        public JsonResult ChangeStatus(int Id)
        {
            try
            {
                //UserProfile userProfile = dbcontext.UserProfile
                //.Where(a => a.Id == Id)
                //.FirstOrDefault();
                //UserProfile userProfile = this._userProfileService.GetUserProfileById(Id);
                UserProfile userProfile = this._userProfileRepo.All.Where(a=>a.Id==Id).FirstOrDefault();
                if (userProfile.Status == true)
                {
                    userProfile.Status = false;
                }
                else
                {
                    userProfile.Status = true;
                }

                //dbcontext.Entry<UserProfile>(userProfile).State = EntityState.Modified;
                //dbcontext.SaveChanges();
                //bool isUpdated = this._userProfileService.UpdateUserProfile(userProfile);
                this._userProfileRepo.Edit(userProfile);
                this._unitOfWork.Commit();
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
            //fetch UserQualificationViewModel data using UserProfileService.
            //List<UserQualificationViewModel> userQualification = this._userProfileService.GetUserQualificationByUserprofileId(Id);
            List<UserQualificationViewModel> userQualification = (from uq in this._userQualificationRepo.All
                                                                  where uq.UserProfileId == Id
                                                                  select new UserQualificationViewModel()
                                                                  {
                                                                      Id=uq.Id,
                                                                      Title=uq.Title,
                                                                      Institution=uq.Institution,
                                                                      IsCertification=uq.IsCertification.Value,
                                                                      IsEducation=uq.IsEducation.Value,
                                                                      ReceiveDate=uq.ReceiveDate
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
            qualification.CreatedBy = User.Identity.Name;// User.Identity.Name;
            qualification.CraetedDate = DateTime.Now;
            this._userQualificationRepo.Add(qualification);
            this._unitOfWork.Commit();
            return new JsonResult()
            {
                Data = qualification,
            };

            //UserQualification qualification = this._userProfileService.SaveUserQualification(userProfileId, title, institution, receiveDate, quali_certi,User.Identity.Name);

            //return new JsonResult()
            //{
            //    Data = qualification,
            //};
        }

        public ActionResult Delete(int Id)
        {
            try
            {
                UserProfile previousRecord = new UserProfile();
                //previousRecord = dbcontext.UserProfile.Find(Id);
                previousRecord = this._userProfileRepo.All.Where(a => a.Id == Id).FirstOrDefault();
                //modify field, IsDeleted, DeletedBy, DeletedDate
                previousRecord.IsDeleted = true;
                previousRecord.DeletedBy = User.Identity.Name;
                previousRecord.DeletedDate = DateTime.Now;
                //set entity state previous record as modified
                this._userProfileRepo.Edit(previousRecord);
                //savechanges on db.
                this._unitOfWork.Commit();

                //bool isDeleted = this._userProfileService.DeleteUserProfile(Id,User.Identity.Name);
                //return RedirectToAction("Index", "UserProfile");

                //Send Deleted success/failed message.
                //ViewBag,ViewData,TempData

                TempData["DeleteMessage"] = "Record Deleted Successfully!!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["DeleteMessage"] = "Record Delete Action Failed!! Exception:"+ex.Message ;
                return RedirectToAction("Index");
            }                      
        }

        [HttpPost]
        public JsonResult DeleteUserQualification(int Id)
        {
            UserQualification userQualification = new UserQualification();
            userQualification = this._userQualificationRepo.All.Where(a => a.Id == Id).FirstOrDefault();
            this._userQualificationRepo.Delete(userQualification);
            this._unitOfWork.Commit();

            // declare classObject 
            //bool isDeleted = this._userProfileService.DeleteUserQualification(Id);
            //return success message as json result.
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}