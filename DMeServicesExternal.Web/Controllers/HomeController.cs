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
            //GetUserDataFromOracleDbTestUser();
            //return View(((User)Session["UserInfo"]));

            var guid = ReadPKISession();
            if(string.IsNullOrWhiteSpace(guid)) return Redirect("https://www.dhofar.gov.om/");
            var user = GetUserDataFromOracleDB(guid);
            if (user != null)
            {
                Session["UserInfo"] = user;
                return View(user);
            }
            return Redirect("https://www.dhofar.gov.om/");
        }

        private string ReadPKISession()
        {
            if (HttpContext.Request.Cookies["SSO"] != null)
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("SSO");
                if (cookie != null) return cookie.Value;
            }
            return null;
        }

        public ActionResult ConsultancyOwnerService()
        {
            GetUserDataFromOracleDbTestUser();
            return RedirectToAction("CompanyList", "BuildingPermits");
        }
        public ActionResult ConsultancyEngineerService()
        {
            GetUserDataFromOracleDbTestUser();
            return RedirectToAction("index", "BuildingPermits");
        }
        public ActionResult LandOwnerService()
        {
            GetUserDataFromOracleDbTestUser();
            return RedirectToAction("LandProjects", "BuildingPermits");
        }
        public ActionResult ContractorService()
        {
            return HttpNotFound();
        }

        private User GetUserDataFromOracleDB(string value)
        {
            throw new NotImplementedException();
        }

        private User GetUserByCivilId(string civilId)
        {
            throw new NotImplementedException();
        }

        private void GetUserDataFromOracleDbTestUser()
        {
            if (Session["UserInfo"] != null) return;
            User oUser = Account.UserLogin("test", "abc@123");
            if (oUser != null && oUser.Id > 0)
            {
                Session["UserInfo"] = oUser;
            }
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