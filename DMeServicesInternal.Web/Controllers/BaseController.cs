using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using DMeServices.Models;


namespace DMeServicesInternal.Web.Controllers
{
    public class BaseController : Controller
    {
        public Employee oEmployeeInfo
        {
            get { return (Employee)Session["EmployeeInfo"]; }
            set { Session["EmployeeInfo"] = value; }
        }

        #region Override Method :: OnAuthorization
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (Session["SystemLanguage"] != null)
            {
                string sLanguage = Session["SystemLanguage"].ToString();
                System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(sLanguage);
                System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(sLanguage);
            }
            else
            {
                string sLanguage = "en-US";
                Session["SystemLanguage"] = sLanguage;
                System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(sLanguage);
                System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(sLanguage);
            }

            if (Session["EmployeeInfo"] != null)
            {
                oEmployeeInfo = (Employee)Session["EmployeeInfo"];
            }
            else
            {
                string FullName = WindowsIdentity.GetCurrent().Name.ToString();

                string Emoplyee_No = FullName.Substring(6);
                var isNumeric = int.TryParse(Emoplyee_No, out int n);
                if (!isNumeric)
                {
                    Response.Redirect("~/Account/Login");
                }
                else
                {
                    Employee oEmployee = DMeServices.Models.Common.EmployeeCom.EmployeeByID(Emoplyee_No);
                    Session["EmployeeInfo"] = oEmployee;
                }


            }

            base.OnAuthorization(filterContext);
        }
        #endregion









    }
}