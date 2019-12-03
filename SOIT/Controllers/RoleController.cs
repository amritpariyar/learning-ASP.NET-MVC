using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SOIT.Data.ViewModels;
using SOIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOIT.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationUserManager _userManager;
       ApplicationDbContext context;
        public RoleController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
            context = new ApplicationDbContext();
            
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var Roles =context.Roles.ToList();
            //var Roles = context.Roles.ToList();
            return View(Roles);
        }

       
        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        public void sum(int a, int b)
        {

        }
        
        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");            
        }

        #region User
        public ActionResult Users()
        {
            var usersWithRoles = (from user in context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in context.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UserWithRolesViewModel()

                                  {
                                      UserId = p.UserId,
                                      UserName = p.Username,
                                      Email = p.Email,
                                      RoleNames = string.Join(",", p.RoleNames)
                                  });
            return View(usersWithRoles);
        }

        public ActionResult UserDetail(string userid)
        {
            var roles = context.Roles.ToList().Select(r=>r.Name).ToList();
            ViewBag.roles = roles;
            var userList = context.Users.OrderBy(u => u.UserName).ToList().Select(ur => new SelectListItem { Value = ur.Id.ToString(), Text = ur.UserName }).ToList();
            ViewBag.Users = userList;
            return View();
        }

        [HttpPost]
        public JsonResult GetRolesWithUser(string Id)
        {
            var roleList = context.Roles.OrderBy(r => r.Name).ToList()
                .Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            var userRole = _userManager.GetRolesAsync(Id).Result.ToList();
            return Json(userRole, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult UpdateRolesWithUser(string userid,string[] roles)
        {
            try
            {
                string[] roleList = _userManager.GetRoles(userid).ToArray<string>();
                _userManager.RemoveFromRoles(userid, roleList);
                if (roles.Count() > 0)
                {
                    _userManager.AddToRoles(userid, roles);
                }
                
                return Json("success", JsonRequestBehavior.DenyGet);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        #endregion
    }
}