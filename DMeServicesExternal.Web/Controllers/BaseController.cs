using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DMeServices.Models;


namespace DMeServicesExternal.Web.Controllers
{
    public class BaseController : Controller
    {
        public User oUserInfo
        {
            get { return (User)Session["UserInfo"]; }
            set { Session["UserInfo"] = value; }
        }

        //public Employee oEmployeeInfo
        //{
        //    get { return (Employee)Session["EmployeeInfo"]; }
        //    set { Session["EmployeeInfo"] = value; }
        //}

        #region Override Method :: OnAuthorization
        //protected override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    if (Session["SystemLanguage"] != null)
        //    {
        //        string sLanguage = Session["SystemLanguage"].ToString();
        //        System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(sLanguage);
        //        System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(sLanguage);
        //    }
        //    //else
        //    //{
        //    //    string sLanguage = "en-US";
        //    //    Session["SystemLanguage"] = sLanguage;
        //    //    System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(sLanguage);
        //    //    System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(sLanguage);
        //    //}


        //    //if (Session["UserInfo"] != null)
        //    //{
        //    //    oUserInfo = (User)Session["UserInfo"];
        //    //}
        //    //else
        //    //{
        //    //    filterContext.Result = new RedirectToRouteResult(
        //    //                 new RouteValueDictionary
        //    //                      {
        //    //                         { "Controller", "UsersLogin" },
        //    //                         { "Action", "Login" },
        //    //                         {"returnUrl",Request.Url.PathAndQuery}
        //    //                      });
        //    //    //Response.Redirect("~/UsersLogin/Login");
                
        //    //}


        //    //base.OnAuthorization(filterContext);


        //    if (Session["EmployeeInfo"] != null)
        //    {
        //        oEmployeeInfo = (Employee)Session["EmployeeInfo"];
        //    }
        //    else
        //    {
        //        string FullName = WindowsIdentity.GetCurrent().Name.ToString();
        //        if (FullName == null)
        //        {
        //            Response.Redirect("~/Account/Login");
        //        }
        //        string Emoplyee_No = FullName.Substring(6);
        //        Employee oEmployee = DMeServices.Models.Common.EmployeeCom.EmployeeByID(Emoplyee_No);
        //        Session["EmployeeInfo"] = oEmployee;
        //    }


        //    base.OnAuthorization(filterContext);
        //}
        #endregion
    }
}