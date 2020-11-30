using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMeServices.DAL;
using DMeServices.Models;
using DMeServices.Models.ViewModels;

namespace DMeServicesExternal.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
            if (HttpContext.Request.Cookies["SSO"] != null)
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("SSO");
                var civilID = GetUserDataFromOracleDB(cookie.Value);
                User user = GetUserByCivilID(civilID);
                if (user != null)
                {
                    Session["UserInfo"] = user;
                    return View(user);
                }
            }
            return Redirect("https://www.dhofar.gov.om/");
        }

        private string GetUserDataFromOracleDB(string cookieValue)
        {
            throw new NotImplementedException();
        }

        public ActionResult ConsultancyOwnerService()
        {
            User user = new User();
            CompanyViewModel companyViewModel = new CompanyViewModel();
            companyViewModel.CompaniesList = GetCompaniesOwnedByPerson(user.CivilId);
            return View("CompanyList", companyViewModel);
        }

        private List<MociData> GetCompaniesOwnedByPerson(long civilID)
        {
            throw new NotImplementedException();
        }

        public ActionResult ConsultancyEngineerService()
        {
            //TODO: IS he already registered as an consultant engineer
            //TODO: If registered (and still work in this company) display all services links ( Request Permit + ??) 
            //TODO: If Not registered Nothing he can do
            return HttpNotFound();
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

        private User GetUserByCivilID(string civilID)
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