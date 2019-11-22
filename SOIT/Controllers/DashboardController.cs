using SOIT.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOIT.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
       public ActionResult Index()
        {
            
            return View();
            
        }

        public ActionResult ChartReport()
        {
            return View();
        }

        public ActionResult GetChartData()
        {
            List<int> test1 = new List<int>();
            test1.Add(1000);
            test1.Add(2000);
            test1.Add(2500);
            test1.Add(2000);
            test1.Add(3500);
            test1.Add(4000);
            test1.Add(6000);
            test1.Add(4000);

            List<int> test2 = new List<int>();
            test2.Add(3000);
            test2.Add(4000);
            test2.Add(4000);
            test2.Add(3000);
            test2.Add(5000);
            test2.Add(4000);
            test2.Add(2000);
            test2.Add(3000);

            ChartDataViewModel data1 = new ChartDataViewModel();
            data1.SeriesName = "series1"; data1.SeriesData = test1;
            

            ChartDataViewModel data2 = new ChartDataViewModel();
            data2.SeriesName = "series2"; data2.SeriesData = test2;

            //List<dynamic> data = new List<dynamic>();
            List<ChartDataViewModel> data = new List<ChartDataViewModel>();
            data.Add(data1);
            data.Add(data2);
            return Json(data, JsonRequestBehavior.AllowGet);

            //List<int> test1 = new List<int>();
            //test1.Add(1000);
            //test1.Add(2000);
            //test1.Add(3000);
            //test1.Add(2000);
            //test1.Add(5000);
            //test1.Add(4000);
            //test1.Add(6000);
            //test1.Add(4000);

            //List<int> test2 = new List<int>();
            //test2.Add(3000);
            //test2.Add(4000);
            //test2.Add(4000);
            //test2.Add(3000);
            //test2.Add(5000);
            //test2.Add(4000);
            //test2.Add(2000);
            //test2.Add(3000);
            ////List<dynamic> data = new List<dynamic>();
            //List<List<int>> data = new List<List<int>>();
            //data.Add(test1);
            //data.Add(test2);
            //return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}

