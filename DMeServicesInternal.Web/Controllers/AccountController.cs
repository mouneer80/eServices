using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMeServices.Models.ViewModels;
using System.Web.Mvc;
using DMeServices.Models;
using DMeServices.Models.Common.Account;

namespace DMeServicesInternal.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            LoginViewModel oUser = new LoginViewModel();

            return View(oUser);
        }




        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {

            Employee oEmployee = Account.EmployeeLogin(login.userName, login.Password);
            if (oEmployee != null && oEmployee.EMP_NO > 0)
            {
                Session["EmployeeInfo"] = oEmployee;

                if (oEmployee.IsEngineer || oEmployee.IsEngineerHead || oEmployee.IsEngineerManager)
                {
                    Session["Show"] = null;
                    return RedirectToAction("Index", "BuildingPermits");
                }

                if (oEmployee.IsSupervisionInspector || oEmployee.IsSupervisionHead)
                {
                    Session["Show"] = 1;
                    return RedirectToAction("Index", "BuildingSupervision");
                }


                if (oEmployee.IsHealthHead)
                {
                    return RedirectToAction("Index", "HealthRenewal");
                }

                if (oEmployee.IsInspectors)
                {
                    return RedirectToAction("InspectorRenew", "HealthRenewal");
                }

                if (oEmployee.IsCollectors)
                {
                    return RedirectToAction("RevenueData", "HealthRenewal");
                }

                if (oEmployee.IsManager)
                {
                    return RedirectToAction("PermitFinish", "HealthRenewal");
                }



            }

                ViewBag.LoginErrorMessage = "خطأ في اسم المستخدم أو كلمة المرور";
                return View();

        }







        public ActionResult Logout()
        {
            Session["EmployeeInfo"] = null;
            Session.Clear();
            Session.Abandon();

            return RedirectToRoute(new
            {
                controller = "Account",
                action = "Login"
            });

        }








    }
}