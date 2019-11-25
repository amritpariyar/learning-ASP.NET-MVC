using SOIT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOIT.Controllers
{
    public class ReportsController : Controller
    {
        private SOITEntities dbContext;
        public ReportsController()
        {
            dbContext = new SOITEntities();
        }
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EmployeeRecord()
        {
            return View();
        }

        public JsonResult GetEmployeeRecordData()
        {
            var employees = (from emp in dbContext.employee
                             select new
                             {
                                 emp.fullName,
                                 emp.gender,
                                 emp.email,
                                 emp.emergencyContactNo,
                                 emp.mobile,
                                 emp.pAddress
                             }).ToList();
            return new JsonResult()
            {
                Data = employees,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue,
                ContentType = "application/json"
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}