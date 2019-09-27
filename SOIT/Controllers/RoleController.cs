using Microsoft.AspNet.Identity.EntityFramework;
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
    }
}