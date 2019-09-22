using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOIT.Controllers
{
    public class HomeController : Controller
    {
        //[RequestFilter(Roles ="Admin")]        
        public ActionResult Index()
        {
            return View();
        }

       // [HandleError(View="Error")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            throw new Exception("Exception Page Redirection");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}