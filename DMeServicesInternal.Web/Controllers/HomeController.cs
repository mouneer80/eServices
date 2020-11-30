using DMeServicesInternal.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace DMeServicesInternal.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

        #region Method:: Report
        public void GetReport()
        {
            ReportParams objReportParams = new ReportParams();

            objReportParams.ReportTitle = "test";
            objReportParams.RptFileName = "CalculateFees.rdlc";
            objReportParams.ReportType = "FeesReport";
        }

        #endregion


    }
}