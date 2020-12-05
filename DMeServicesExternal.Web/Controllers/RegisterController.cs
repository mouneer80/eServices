using DMeServices.Models.Common;
using DMeServices.Models.ViewModels.Permits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMeServices.Models.Common.BuildingServices;
using RestSharp;
using RestSharp.Deserializers;

namespace DMeServicesExternal.Web.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            UserViewModel oModel = new UserViewModel();

           var list = new List<SelectListItem>

      //      ViewBag.CourierList = new List<SelectListItem>
         {
        new SelectListItem{ Text="الأفراد", Value = "1", Selected = true },
        new SelectListItem{ Text="الاستشاري", Value = "2" }
        //new SelectListItem{ Text="Option 3", Value = "3", Selected = true },
         };

             ViewData["RegisterType"] = list;
             return View(oModel);
        }
        public ActionResult SaveUser(UserViewModel oModel)
        {

            if (!ModelState.IsValid)
            {

                var list = new List<SelectListItem>
                  {
                    new SelectListItem{ Text="الأفراد", Value = "1" },
                    new SelectListItem{ Text="الاستشاري", Value = "2" }

                   };

                ViewData["RegisterType"] = list;

                var viewModel = new UserViewModel
                {
                    oUser = oModel.oUser
                };

            

                return View("Index", viewModel);

            }


            int vResult;

            if (oModel.oUser.Id > 0)
            {
                vResult = UserCom.UpdateUserInfo(oModel);
            }
            else
            {
                vResult = UserCom.RegisterNewUser(oModel);
            }
            switch (vResult)
            {
                case 1: ViewBag.Result = "تم تسجيل بنجاح";
                    break;
                case 99:
                    ViewBag.Result = "بيانات المستخدم موجوده مسبقا";
                    break;

                case 0:
                    ViewBag.Result = "بيانات المستخدم غير موجودة";
                    break;
            }

            return RedirectToAction("Login", "UsersLogin");
           // return View();


        }

        public ActionResult SaveCompany(UserViewModel oModel)
        {
            oModel.CompanyData.CIVIL_ID = oModel.oUserInfo.CivilId.ToString();
            if (oModel.CompanyData.COMMERCIAL_NO != null)
                oModel.CompanyData.ID = oModel.CompanyData.COMMERCIAL_NO.Value;
            ViewBag.Result = MociCompaniesData.SaveCompany(oModel.CompanyData);
            return RedirectToAction("CompanyList", "BuildingPermits");
        }

        public ActionResult AddCompany()
        {
            //object result = GetCompanyDataByCr();
            UserViewModel oModel = new UserViewModel();
            return View(oModel);
        }
        public static object GetCompanyDataByCr(string cr= "1000132")
        {
            var client = new RestClient("http://10.21.4.4:8087/dm/rest/services/companydata?crNumber="+ cr);
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            IRestResponse restResponse = client.Execute(request);
            JsonDeserializer deserial = new JsonDeserializer();
            var x = deserial.Deserialize<CompanyOverviewResult>(restResponse);

            return (x.CompanyOverview);
        }
    }
}