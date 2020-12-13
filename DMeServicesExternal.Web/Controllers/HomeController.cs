﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMeServices.DAL;
using DMeServices.Models;
using DMeServices.Models.Common.Account;
using DMeServices.Models.Common.BuildingServices;
using DMeServices.Models.ViewModels;
using Oracle.ManagedDataAccess.Client;

namespace DMeServicesExternal.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //GetUserDataFromOracleDbTestUser();
            //return View(((User)Session["UserInfo"]));
            //Response.Cookies.Add(CreateStudentCookie());
            var guid = ReadPkiSession();
            if (!string.IsNullOrWhiteSpace(guid))
            {
                var user = GetUserDataFromOracleDb(guid);
                if (user != null)
                {
                    Session["UserInfo"] = user;
                    return View(user);
                }
            }
            return Redirect("https://www.dhofar.gov.om/sso/default.aspx?noguid=1&returnUrl=" + Request.Url.AbsoluteUri);
        }
        private string ReadPkiSession()
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
        private User GetUserDataFromOracleDb(string value)
        {
            DataSet dataSet = new DataSet();
            using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["oracleDB"].ConnectionString))
            {
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                using (OracleDataAdapter dataAdapter = new OracleDataAdapter())
                {
                    command.CommandText = string.Format("select * from SSODM.SSO where GUID = '{0}'", value);
                    dataAdapter.SelectCommand = command;
                    dataAdapter.Fill(dataSet);
                }
            }
            User user = BuildUserObject(dataSet.Tables[0]);
            return user;
        }
        private User BuildUserObject(DataTable dt)
        {
            string[] names = SplitName(dt.Rows[0]["omancardTitleFullNameAr"].ToString());
            User user = new User()
            {
                FirstName = names[0],
                SecondName = names[1],
                ThirdName = names[2],
                LastName = String.Join(" ", names, 3,names.Length -4),
                CivilId = long.Parse(dt.Rows[0]["omanIDCivilNumber"].ToString()),
                //CivilId = 3437335,
                CompanyName = dt.Rows[0]["omancardSponsorNameAr"].ToString()
            };
            return user;
        }
        private string[] SplitName(string fullName)
        {
            return fullName.Split(new char[] {' '});
        }
        // For Testing To be removed
        private void GetUserDataFromOracleDbTestUser()
        {
            if (Session["UserInfo"] != null) return;
            User oUser = Account.UserLogin("test", "abc@123");
            if (oUser != null && oUser.Id > 0)
            {
                Session["UserInfo"] = oUser;
            }
        }
        private HttpCookie CreateStudentCookie()
        {
            HttpCookie studentCookies = new HttpCookie("SSO");
            studentCookies.Value = "23e4d55d-1049-4445-979d-6cdf79bfa25d";
            studentCookies.Expires = DateTime.Now.AddHours(1);
            return studentCookies;
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