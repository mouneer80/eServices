﻿using DMeServices.Models;
using DMeServices.Models.Common.Account;
using DMeServices.Models.ViewModels;
using System.Web.Mvc;

namespace DMeServicesExternal.Web.Controllers
{
    public class UsersLoginController : Controller
    {
        public object MembershipService { get; private set; }

        // GET: UsersLogin
        public ActionResult Login()
        {
            ViewBag.returnUrl = Request.QueryString["returnUrl"];
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel login, string returnUrl)
        { 
            User oUser = Account.UserLogin(login.userName, login.Password);
            if (oUser != null && oUser.Id > 0)
            {
                Session["UserInfo"] = oUser;
                //return RedirectToAction("Index", "BuildingPermits");
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.LoginErrorMessage = "خطأ في اسم المستخدم أو كلمة المرور";
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session["UserInfo"] = null;
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}