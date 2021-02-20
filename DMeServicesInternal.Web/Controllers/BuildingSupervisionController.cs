﻿using DMeServices.Models;
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
    public class BuildingSupervisionController : BaseController
    {
        // GET: BuildingSupervisions
        public ActionResult Index()
        {
            SupervisionViewModel oModel = new SupervisionViewModel();

            return View(oModel);
        }

        public ActionResult SupervisionsList(string Type)
        {
            SupervisionViewModel oModel = new SupervisionViewModel();
            switch (Type)
            {
                case "NewSupervisions":
                    oModel.ListBuildingSupervision = SupervisionCom.GetAllSupervisionsByFlowStatus(8);
                    break;

                case "CanceledSupervisions":
                    oModel.ListBuildingSupervision = SupervisionCom.GetAllSupervisionsByFlowStatus(18);
                    break;

                case "AcceptedSupervisions":
                    oModel.ListBuildingSupervision = SupervisionCom.GetAllSupervisionsByFlowStatus(19);
                    break;

                case "NotCompleteSupervisions":
                    oModel.ListBuildingSupervision = SupervisionCom.GetAllSupervisionsByFlowStatus(10);
                    break;

                case "AllSupervisions":
                    oModel.ListBuildingSupervision = SupervisionCom.AllSupervisions();
                    break;
            }
            return PartialView("_SupervisionsList", oModel);
        }

        public ActionResult SupervisionDetails(int Id = -99)
        {
            SupervisionViewModel oModel = new SupervisionViewModel();
            oModel.BuildingSupervision = SupervisionCom.SupervisionsById(Id);
            ViewBag.DDWelayat = DDWelayat();
            ViewBag.DDRegion = DDRegionSaved(oModel.BuildingSupervision.BldPermits.WelayahID);
            //ViewBag.DDArea = DDAreaSaved(oModel.BuildingPermits.RegionID);
            ViewBag.DDBuildingTypes = DDBuildingTypes();
            ViewBag.DDLandUseTypes = DDLandUseTypes();
            ViewBag.DDSquareLetters = DDSquareLetters();
            oModel.ListOfAttachments = PermitsAttachmentsCom.AttachmentsByPermitsID(oModel.BuildingSupervision.BldPermitID, (int)oModel.BuildingSupervision.BldPermits.OwnerCivilId);
            ViewBag.DDSupervisionsStatus = DDSupervisionsStatus();
            oModel.Payments = PaymentsCom.PaymentsBySupervisionID(oModel.BuildingSupervision.ID);
            oModel.PaymentDetailsList = PaymentsCom.MapsPaymentDetailsBySupervisionID(oModel.BuildingSupervision.ID);
            if (oModel.oEmployeeInfo.IsSupervisionHead)
            {
                ViewBag.DDEngineersList = DDEngineers();
                return View("HeadSupervisionDetails", oModel);
            }


            return View("InspectorDetails", oModel);
        }

        public ActionResult AssignSupervision(SupervisionViewModel oModel)
        {
            BuildingSupervision Supervisions = SupervisionCom.AssignSupervisions(oModel.BuildingSupervision);
            oModel.BuildingSupervision = Supervisions;
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





        #region Method :: Save Engineer Supervision

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveInspectorDetails(SupervisionViewModel oModel)
        {

            string Result = SupervisionCom.SaveInspectorDetails(oModel);

            if (Result == "ok")
            {
                var User = DMeServices.Models.Common.UserCom.UserByCivilID((int)oModel.BuildingSupervision.ConsultantCivilId);

                switch (oModel.BuildingSupervision.Status)
                {
                    case 18:
                        DMeServices.Models.Common.SmsCom.SendSms("968" + User.MobileNo, " : تم   الغاء المعاملة رقم المعاملة " + oModel.BuildingSupervision.TransactNo);
                        DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.BuildingSupervision.OwnerPhoneNo, " : تم   الغاء المعاملة رقم المعاملة " + oModel.BuildingSupervision.TransactNo);
                        break;

                    case 19:
                        DMeServices.Models.Common.SmsCom.SendSms("968" + User.MobileNo, " : تم قبول المعاملة رقم المعاملة " + oModel.BuildingSupervision.TransactNo);
                        DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.BuildingSupervision.OwnerPhoneNo, " : تم قبول المعاملة  رقم المعاملة " + oModel.BuildingSupervision.TransactNo);
                        break;
                    case 20:
                        DMeServices.Models.Common.SmsCom.SendSms("968" + User.MobileNo, " : يوجد بعض التعديلات علي الخرائط رقم المعاملة " + oModel.BuildingSupervision.TransactNo);
                        DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.BuildingSupervision.OwnerPhoneNo, " : يوجد بعض التعديلات علي الخرائط رقم المعاملة " + oModel.BuildingSupervision.TransactNo);
                        break;
                }
            }
            return RedirectToAction("Index");
        }

        #endregion





        #region Method :: List Attachment Details

        public ActionResult SelectAttachment(int Id = -99)
        {
            SupervisionViewModel oModel = new SupervisionViewModel();
            oModel.ListOfAttachments = DMeServices.Models.Common.BuildingServices.PermitsAttachmentsCom.AttachmentsByPermitsID(Id, (int)oModel.BuildingPermits.OwnerCivilId);
            return PartialView("_ListAttachments", oModel);
        }


        #endregion





        #region Method :: DD Supervisions Status

        public static List<SelectListItem> DDSupervisionsStatus()
        {
            List<SelectListItem> LstSupervisionsStatus = new List<SelectListItem>();
            List<LookupType> SupervisionsStatus = DMeServices.Models.Common.LookupsTypeCom.LookupByDesc("PermitsStatus");
            if (SupervisionsStatus.Count > 0)
            {
                LstSupervisionsStatus.Add(new SelectListItem() { Text = "أختر حالة الطلب ", Value = "0" });
                foreach (var item in SupervisionsStatus)
                {
                    LstSupervisionsStatus.Add(new SelectListItem() { Text = item.LookupNameAr, Value = item.LookupId.ToString() });
                }

            }
            return LstSupervisionsStatus;
        }

        #endregion

        #region Method :: Fees and Payments

        public JsonResult GetFees(string id)
        {
            var fee = ServiceFeesCom.TypeByID(int.Parse(id));

            return Json(fee.ServiceFees.ToString(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuildingSupervisionFees(int Id = -99)
        {
            SupervisionViewModel oModel = new SupervisionViewModel();
            oModel.BuildingSupervision = SupervisionCom.SupervisionsById(Id);
            oModel.BuildingPermits = PermitsCom.PermitsByID(oModel.BuildingSupervision.BldPermitID);
            TempData["PaymentDetails"] = new List<PaymentDetails>();
            ViewBag.DDServices = DDServiceFees();
            return View("SupervisionPaymentDetails", oModel);
        }

        public static List<SelectListItem> DDServiceFees()
        {
            List<SelectListItem> LstServicesFees = new List<SelectListItem>();
            List<ServiceFee> AllServicesFees = ServiceFeesCom.AllServiceFees();
            if (AllServicesFees.Count > 0)
            {
                LstServicesFees.Add(new SelectListItem() { Text = "أختر نوع الخدمة ", Value = "0" });
                foreach (var item in AllServicesFees)
                {
                    LstServicesFees.Add(new SelectListItem() { Text = item.ServiceName, Value = item.ServiceID.ToString() });
                }
            }
            return LstServicesFees;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSupervisionsFees(SupervisionViewModel oModel)
        {
            oModel.Payment = new Payments();
            oModel.PaymentDetailsList = (List<PaymentDetails>)TempData["PaymentDetails"];
            decimal total = 0;
            foreach (var item in oModel.PaymentDetailsList)
            {
                total += (decimal)item.TotalAmount;
            }
            oModel.Payment.PaymentTotalAmount = total;
            TempData["PaymentDetails"] = null;
            string Result = PaymentsCom.SavePaymentDetailsForSupervision(oModel);
            oModel.BuildingSupervision.Status = 28;
            //oModel.BuildingSupervision.PaymentID = int.Parse(Result);
            SupervisionCom.SaveInspectorDetails(oModel);
            ViewBag.PaymentID = Result;
            return RedirectToAction("SupervisionDetails", "BuildingSupervision", new { Id = oModel.BuildingSupervision.ID });
        }

        [HttpPost]
        public ActionResult SavePayment()
        {
            PermitsViewModel oModel = new PermitsViewModel();
            var listofPaymentDetails = (List<PaymentDetails>)TempData["PaymentDetails"];
            var serviceID = System.Web.HttpContext.Current.Request.Form["ServiceID"];
            var fees = System.Web.HttpContext.Current.Request.Form["Fees"];
            var quantity = System.Web.HttpContext.Current.Request.Form["Quantity"];
            var total = System.Web.HttpContext.Current.Request.Form["Total"];
            oModel.PaymentDetails = new PaymentDetails();
            oModel.PaymentDetails.ServiceQuantity = int.Parse(quantity);
            oModel.PaymentDetails.ServiceID = int.Parse(serviceID);
            oModel.PaymentDetails.ServiceFees = decimal.Parse(fees);
            oModel.PaymentDetails.TotalAmount = decimal.Parse(total);
            oModel.PaymentDetailsList = (List<PaymentDetails>)TempData["PaymentDetails"];
            if (oModel.PaymentDetailsList == null)
            {
                oModel.PaymentDetailsList = new List<PaymentDetails>();
            }
            oModel.PaymentDetailsList.Add(oModel.PaymentDetails);
            TempData["PaymentDetails"] = oModel.PaymentDetailsList;
            return PartialView("_ListPayments", oModel);
        }
        #endregion
    }


}