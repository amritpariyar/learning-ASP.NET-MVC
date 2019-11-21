using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOIT.Controllers
{
    public class testController : Controller
    {
        // GET: test
        public ActionResult Index()
        {
            List<int> test = new List<int>();
            test.Add(12);
            test.Add(13);
            test.Add(11);
            test.Add(12);
            test.Add(12);
            test.Add(12);

            return View();
        }
    }
}