using DMeServices.Models;
using DMeServices.Models.BuildingServices;
using DMeServices.Models.Common.BuildingServices;
using DMeServices.Models.ViewModels.Internal.Permits;
using RadPdf.Web.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMeServicesInternal.Web.Controllers
{
    public class BuildingControlsController : BaseController
    {
        // GET: BuildingPermits
        public ActionResult Index()
        {
            ControlsViewModel oModel = new ControlsViewModel();

            return View(oModel);
        }




        public ActionResult ControlsList(string Type)
        {
            ControlsViewModel oModel = new ControlsViewModel();
            switch (Type)
            {
                case "NewPermits":
                    oModel.ListBuildingControls = ControlsCom.GetAllControlsByFlowStatus(8);
                    break;

                case "CanceledPermits":
                    oModel.ListBuildingControls = ControlsCom.GetAllControlsByFlowStatus(18);
                    break;

                case "AcceptedPermits":
                    oModel.ListBuildingControls = ControlsCom.GetAllControlsByFlowStatus(19);
                    break;

                case "NotCompletePermits":
                    oModel.ListBuildingControls = ControlsCom.GetAllControlsByFlowStatus(10);
                    break;

                case "AllPermits":
                    oModel.ListBuildingControls = ControlsCom.AllControls();
                    break;
            }



            return PartialView("_ControlsList", oModel);
        }




        public ActionResult ControlDetails(int Id = -99)
        {
            ControlsViewModel oModel = new ControlsViewModel();
            oModel.BuildingControls = ControlsCom.ControlsById(Id);
            ViewBag.DDWelayat = DDWelayat();
            ViewBag.DDRegion = DDRegionSaved(oModel.BuildingPermits.WelayahID);
            //ViewBag.DDArea = DDAreaSaved(oModel.BuildingPermits.RegionID);
            ViewBag.DDBuildingTypes = DDBuildingTypes();
            ViewBag.DDLandUseTypes = DDLandUseTypes();
            ViewBag.DDSquareLetters = DDSquareLetters();
            oModel.ListOfAttachments = PermitsAttachmentsCom.AttachmentsByPermitsID(Id);

            if (oModel.oEmployeeInfo.IsControlHead)
            {
                ViewBag.DDEngineersList = DDEngineers();
                return View("HeadControlDetails", oModel);
            }

            ViewBag.DDControlsStatus = DDControlsStatus();
            return View("EngineerControlDetails", oModel);
        }

        public ActionResult AssignControls(ControlsViewModel oModel)
        {
            BuildingControls controls = ControlsCom.AssignControls(oModel.BuildingControls);
            oModel.BuildingControls = controls;
            return RedirectToAction("Index");
        }



        #region Method :: Display Files 


        public ActionResult GetFile(int ID)
        {
            ViewBag.ID = ID;

            return PartialView("_ViewFile");
        }






        public ActionResult DisplayFiles(int Id = -99)
        {
            //IExcelDataReader reader = null;
            PermitsAttachments Attachment = DMeServices.Models.Common.BuildingServices.PermitsAttachmentsCom.AttachmentsByID(Id);

            string contentType = MimeMapping.GetMimeMapping(Attachment.AttachmentPath);



            if (Attachment != null)
            {
                Stream stream = new MemoryStream(Attachment.AttachmentStream);
                stream.Position = 0;
                
                if (Attachment.AttachmentName.EndsWith(".pdf"))
                {
                    return new FileStreamResult(stream, contentType);
                }
                else if (Attachment.AttachmentName.EndsWith(".jpg"))
                {
                    return new FileStreamResult(stream, contentType);
                }
                else if (Attachment.AttachmentName.EndsWith(".png"))
                {
                    return new FileStreamResult(stream, contentType);
                }
            }
            return new EmptyResult();
        }



        #endregion

      


        #region Method :: DD Engineers

        public static List<SelectListItem> DDEngineers()
        {
            List<SelectListItem> LstEngineers = new List<SelectListItem>();
            List<Employee> AllEngineers = DMeServices.Models.Common.EmployeeCom.EmployeeByJobID(7);
            if (AllEngineers.Count > 0)
            {
                LstEngineers.Add(new SelectListItem() { Text = "أختر المهندس ", Value = "0" });
                foreach (var item in AllEngineers)
                {
                    LstEngineers.Add(new SelectListItem() { Text = item.NAME, Value = item.EMP_NO.ToString() });
                }

            }
            return LstEngineers;
        }

        #endregion

        #region Method :: DD Welayat

        public static List<SelectListItem> DDWelayat()
        {
            List<SelectListItem> LstWelayat = new List<SelectListItem>();
            List<Welyat> AllWelayat = WelayatCom.AllWelayat();
            if (AllWelayat.Count > 0)
            {
                LstWelayat.Add(new SelectListItem() { Text = "أختر الولاية ", Value = "0" });
                foreach (var item in AllWelayat)
                {
                    LstWelayat.Add(new SelectListItem() { Text = item.WelyahArName, Value = item.WelyahID.ToString() });
                }

            }
            return LstWelayat;
        }

        #endregion

        #region Method :: DD Regions

        public JsonResult GetRegions(string id)  
        {  
            List<SelectListItem> states = new List<SelectListItem>();  
            var stateList = this.DDRegions(Convert.ToInt32(id));  
            var stateData = stateList.Select(m => new SelectListItem()  
            {  
                Text = m.RegionArName,  
                Value = m.RegionID.ToString(),  
            });  
            return Json(stateData, JsonRequestBehavior.AllowGet);  
        }

        public List<Regions> DDRegions(int? WelayahID)
        {
            //List<Regions> LstRegions = new List<Regions>();
            List<Regions> AllRegions = RegionsCom.RegionByWelayahID(WelayahID);
            
            return AllRegions;
        }

        public static List<SelectListItem> DDRegionSaved(int? WelayahID)
        {
            List<SelectListItem> LstRegions = new List<SelectListItem>();
            List<Regions> AllRegions = RegionsCom.RegionByWelayahID(WelayahID);
            if (AllRegions.Count > 0)
            {
                LstRegions.Add(new SelectListItem() { Text = "أختر المنطقة / القرية", Value = "0" });
                foreach (var item in AllRegions)
                {
                    LstRegions.Add(new SelectListItem() { Text = item.RegionArName, Value = item.RegionID.ToString() });
                }

            }
            return LstRegions;
        }
        #endregion

        #region Method :: DD SquareLetters

        public static List<SelectListItem> DDSquareLetters()
        {
            List<SelectListItem> LstSquareLetters = new List<SelectListItem>();
            List<SquareLetters> AllSquareLetters = SquareLettersCom.AllSquareLetters();
            if (AllSquareLetters.Count > 0)
            {
                LstSquareLetters.Add(new SelectListItem() { Text = "أختر رقم رمز المربع", Value = "0" });
                foreach (var item in AllSquareLetters)
                {
                    LstSquareLetters.Add(new SelectListItem() { Text = item.ArLetter, Value = item.ID.ToString() });
                }

            }
            return LstSquareLetters;
        }

        #endregion

        #region Method :: DD LandUseTypes

        public static List<SelectListItem> DDLandUseTypes()
        {
            List<SelectListItem> LstLandUseTypes = new List<SelectListItem>();
            List<LandUseTypes> AllLandUseTypes = LandUseTypesCom.AllLandUseTypes();
            if (AllLandUseTypes.Count > 0)
            {
                LstLandUseTypes.Add(new SelectListItem() { Text = "أختر نوع استعمال قطعة الأرض", Value = "0" });
                foreach (var item in AllLandUseTypes)
                {
                    LstLandUseTypes.Add(new SelectListItem() { Text = item.UseType, Value = item.ID.ToString() });
                }

            }
            return LstLandUseTypes;
        }

        #endregion

        #region Method :: DD BuildingTypes

        public static List<SelectListItem> DDBuildingTypes()
        {
            List<SelectListItem> LstBuildingTypes = new List<SelectListItem>();
            List<BuildingTypes> AllBuildingTypes = BuildingTypesCom.AllBuildingTypes();
            if (AllBuildingTypes.Count > 0)
            {
                LstBuildingTypes.Add(new SelectListItem() { Text = "أختر نوع المبنى", Value = "0" });
                foreach (var item in AllBuildingTypes)
                {
                    LstBuildingTypes.Add(new SelectListItem() { Text = item.BuildingType, Value = item.ID.ToString() });
                }

            }
            return LstBuildingTypes;
        }

        #endregion

       



        #region Method :: Save Engineer Permits

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEngineerControls(ControlsViewModel oModel)
        {

            string Result = ControlsCom.SaveEngineerControls(oModel);

            if (Result == "ok")
            {
                var User = DMeServices.Models.Common.UserCom.UserByCivilID((long)oModel.BuildingControls.ConsultantCivilId);

                switch (oModel.BuildingControls.Status)
                {
                    case 18:
                        DMeServices.Models.Common.SmsCom.SendSms("968" + User.MobileNo, " : تم   الغاء المعاملة رقم المعاملة " + oModel.BuildingControls.TransactNo);
                        DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.BuildingControls.OwnerPhoneNo, " : تم   الغاء المعاملة رقم المعاملة " + oModel.BuildingControls.TransactNo);
                        break;

                    case 19:
                        DMeServices.Models.Common.SmsCom.SendSms("968" + User.MobileNo, " : تم قبول المعاملة رقم المعاملة " + oModel.BuildingControls.TransactNo);
                        DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.BuildingControls.OwnerPhoneNo, " : تم قبول المعاملة  رقم المعاملة " + oModel.BuildingControls.TransactNo);
                        break;
                    case 20:
                        DMeServices.Models.Common.SmsCom.SendSms("968" + User.MobileNo, " : يوجد بعض التعديلات علي الخرائط رقم المعاملة " + oModel.BuildingControls.TransactNo);
                        DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.BuildingControls.OwnerPhoneNo, " : يوجد بعض التعديلات علي الخرائط رقم المعاملة " + oModel.BuildingControls.TransactNo);
                        break;
                }

            }


            return RedirectToAction("Index");
        }

        #endregion


       


        #region Method :: List Attachment Details

        public ActionResult SelectAttachment(int Id = -99)
        {
            ControlsViewModel oModel = new ControlsViewModel();
            oModel.ListOfAttachments = DMeServices.Models.Common.BuildingServices.PermitsAttachmentsCom.AttachmentsByPermitsID(Id);
            return PartialView("_ListAttachments", oModel);
        }


        #endregion


        


        #region Method :: DD Controls Status

        public static List<SelectListItem> DDControlsStatus()
        {
            List<SelectListItem> LstControlsStatus = new List<SelectListItem>();
            List<LookupType> ControlsStatus = DMeServices.Models.Common.LookupsTypeCom.LookupByDesc("ControlsStatus");
            if (ControlsStatus.Count > 0)
            {
                LstControlsStatus.Add(new SelectListItem() { Text = "أختر حالة الطلب ", Value = "0" });
                foreach (var item in ControlsStatus)
                {
                    LstControlsStatus.Add(new SelectListItem() { Text = item.LookupNameAr, Value = item.LookupId.ToString() });
                }

            }
            return LstControlsStatus;
        }

        #endregion
    }


}