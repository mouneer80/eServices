using DMeServices.Models.Common;
using DMeServices.Models.ViewModels.Permits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using DMeServices.Models;
using DMeServices.Models.BuildingServices;
using DMeServices.Models.Common.BuildingServices;
using DMeServices.Models.ViewModels;
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

            //ViewBag.CourierList = new List<SelectListItem>
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
                case 1:
                    ViewBag.Result = "تم تسجيل بنجاح";
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
            if (!string.IsNullOrWhiteSpace(oModel.CompanyData.COMMERCIAL_NAME))
            {
                oModel.CompanyData.CIVIL_ID = oModel.oUserInfo.CivilId.ToString();
                oModel.CompanyData.FULL_NAME = oModel.oUserInfo.FullName;
                if (oModel.CompanyData.COMMERCIAL_NO != null)
                    oModel.CompanyData.ID = long.Parse((oModel.CompanyData.COMMERCIAL_NO.Value).ToString() + (oModel.oUserInfo.CivilId).ToString());

                ViewBag.Result = MociCompaniesData.SaveCompany(oModel.CompanyData);
                return RedirectToAction("CompanyList", "BuildingPermits");
            }
            return View("AddCompany");
        }

        public ActionResult AddCompany()
        {
            //object result = GetCompanyDataByCr();
            UserViewModel oModel = new UserViewModel();
            return View(oModel);
        }

        [HttpGet]
        public string GetCompanyDataByCr(string id)
        {
            var client = new RestClient("http://10.21.4.4:8087/dm/rest/services/companydata?crNumber=" + id);
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            IRestResponse restResponse = client.Execute(request);
            //JsonDeserializer deserialize = new JsonDeserializer();
            //var x = deserialize.Deserialize<CompanyOverviewResult>(restResponse);
            return restResponse.Content;
        }

        public ActionResult CompanyDetails(long id, int cid)
        {
            var companyViewModel = new CompanyViewModel()
            {
                CompanyData = MociCompaniesData.GetCompanyByCrCid(id, cid),
                ConsultantsList = UserCom.GetUsersListByCr(id)
            };
            return View(companyViewModel);
        }

        #region Method :: Save Consultant
        [HttpPost]
        public ActionResult SaveConsultant()
        {
            var consultantCivilId = System.Web.HttpContext.Current.Request.Form["ConsultantCivilId"];
            var consultantFullName = System.Web.HttpContext.Current.Request.Form["ConsultantFullName"];
            var consultantJobName = System.Web.HttpContext.Current.Request.Form["ConsultantJobName"];
            var consultantPhoneNumber = System.Web.HttpContext.Current.Request.Form["ConsultantPhoneNumber"];
            var consultantEmail = System.Web.HttpContext.Current.Request.Form["ConsultantEmail"];
            var commercialNo = System.Web.HttpContext.Current.Request.Form["CommercialNo"];

            var CivilId = int.Parse(consultantCivilId);
            var MobileNo = int.Parse(consultantPhoneNumber);
            var ConsultantCrNo = int.Parse(commercialNo);

            var oModel = new CompanyViewModel();

            oModel.Consultant = new User();


            oModel.Consultant.CivilId = CivilId;
            oModel.Consultant.FullName = consultantFullName;
            oModel.Consultant.JobName = consultantJobName;
            oModel.Consultant.MobileNo = MobileNo;
            oModel.Consultant.Email = consultantEmail;
            oModel.Consultant.ConsultantCrNo = ConsultantCrNo;

            oModel.ConsultantsList = (List<User>)TempData["Consultants"] ?? new List<User>();

            if (oModel.ConsultantsList.Count == 0)
            {
                oModel.ConsultantsList = UserCom.GetUsersListByCr(int.Parse(commercialNo));
            }
            if (ConsultantOccupationCom.IsOccupassionValid(consultantJobName))
            {
                oModel.ConsultantsList.Add(oModel.Consultant);
                TempData["Consultants"] = oModel.ConsultantsList;
                ViewBag.Message = null;
            }
            else
            {
                ViewBag.Message = "لا يمكن اضافة هذه المهنة";
            }
            return PartialView("_ListConsultants", oModel);
        }
        #endregion

        #region Method :: Delete Consultant

        public ActionResult DeleteConsultant(int id)
        {
            var oModel = new CompanyViewModel { ConsultantsList = (List<User>)TempData["Consultants"] };
            oModel.Consultant = oModel.ConsultantsList[id];
            oModel.ConsultantsList.Remove(oModel.Consultant);
            TempData["Consultants"] = oModel.ConsultantsList;
            return PartialView("_ListConsultants", oModel);
        }
        #endregion

        public ActionResult EditCompany(CompanyViewModel oModel)
        {
            oModel.ConsultantsList = (List<User>)TempData["Consultants"];
            string result = MociCompaniesData.SaveCompany(oModel);
            if (!string.IsNullOrWhiteSpace(result))
            {
                ViewBag.Message = result;
                TempData["Consultants"] = null;
                return RedirectToAction("CompanyList", "BuildingPermits");
            }

            return View("CompanyDetails");
        }

        [HttpGet]
        public string GetEmployeeDataByCivilID(string id, string cr)
        {
            var client = new RestClient("http://10.21.4.4:8085/api/momp/GetWorkerDetails/" + cr + "/" + id);
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            IRestResponse restResponse = client.Execute(request);
            //JsonDeserializer deserialize = new JsonDeserializer();
            //var x = deserialize.Deserialize<CompanyOverviewResult>(restResponse);
            return restResponse.Content;
        }
    }
}