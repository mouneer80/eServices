using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMeServices.DAL;
using DMeServices.Models;
using DMeServices.Models.Common.Account;
using DMeServices.Models.Common.BuildingServices;
using DMeServices.Models.ViewModels;

namespace DMeServicesExternal.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
            //if (HttpContext.Request.Cookies["SSO"] != null)
            //{
            //    HttpCookie cookie = HttpContext.Request.Cookies.Get("SSO");
            //    var civilID = GetUserDataFromOracleDB(cookie.Value);
            //    User user = GetUserByCivilId(civilID);
            //    if (user != null)
            //    {
            //        Session["UserInfo"] = user;
            //        return View(user);
            //    }
            //}
            //return Redirect("https://www.dhofar.gov.om/");
        }

        private User GetUserDataFromOracleDb()
        {
            User oUser = Account.UserLogin("test", "abc@123");
            if (oUser != null && oUser.Id > 0)
            {
                Session["UserInfo"] = oUser;
            }
            return oUser;
        }

        public ActionResult ConsultancyOwnerService()
        {
            User user = GetUserDataFromOracleDb();
            CompanyViewModel companyViewModel = new CompanyViewModel
            {
                CompaniesList = MociCompaniesData.CompaniesByOwnerCivilId(user.CivilId)
            };
            return View("CompanyList", companyViewModel);
        }

        private List<MociData> GetCompaniesOwnedByPerson(long civilId)
        {
            throw new NotImplementedException();
        }

        public ActionResult ConsultancyEngineerService()
        {
            User oUser = GetUserDataFromOracleDb();
            return RedirectToAction("index", "BuildingPermits");
        }
        public ActionResult LandOwnerService()
        {
            //TODO: IS he already registered as a land owner
            //TODO: If registered display all services links ( Request several kinds of Permits + ??) 
            //TODO: If Not registered Nothing he can do(??)
            return HttpNotFound();
        }
        public ActionResult ContractorService()
        {
            return HttpNotFound();
        }

        private User GetUserByCivilId(string civilId)
        {
            throw new NotImplementedException();
        }

        //Old Index Controller before IDP
        //public ActionResult Index()
        //{

        //    return View();
        //}
        //End of Old Index

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}