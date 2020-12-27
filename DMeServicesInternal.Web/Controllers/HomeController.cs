using DMeServices.Models.ViewModels.Internal.Permits;
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
            if (Session["EmployeeInfo"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToRoute(new
                {
                    controller = "Account",
                    action = "Login"
                });
                
            }
        }


            
        public ActionResult DashBoard()
        {
            PermitsViewModel dashboard = new PermitsViewModel();

            dashboard.BuildingPermits_count = DMeServices.Models.Common.BuildingServices.PermitsCom.GetAllPermitsByflowStatus(8).Count;
            dashboard.Companies_count = DMeServices.Models.Common.BuildingServices.MociCompaniesData.AllCompanies().Count;
            dashboard.Users_count = DMeServices.Models.Common.UserCom.AllUsers().Count;
            return View("About", dashboard);
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