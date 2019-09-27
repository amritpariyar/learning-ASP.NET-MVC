using SOIT.Models.Data;
using System;
using System.Collections.Generic;
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
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                dbcontext.UserProfile.Add(userProfile);
                dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Province = new SelectList(dbcontext.Province.ToList(), "Id", "Name");
            return View();
        }
    }
}