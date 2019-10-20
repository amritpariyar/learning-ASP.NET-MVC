using Microsoft.AspNet.Identity.EntityFramework;
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
       ApplicationDbContext context;
        public RoleController()
        {
            context = new ApplicationDbContext();
            
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
        #endregion
    }
}