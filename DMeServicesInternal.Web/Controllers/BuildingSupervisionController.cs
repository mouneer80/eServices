using DMeServices.Models;
using DMeServices.Models.BuildingServices;
using DMeServices.Models.Common.BuildingServices;
using DMeServices.Models.ViewModels.Internal.Permits;
using DMeServicesInternal.Web.Reports;
using Microsoft.Reporting.WebForms;
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
            bool IsManaging = false;
            if (oEmployeeInfo.IsSupervisionHead || oEmployeeInfo.IsEngineerManager)
                IsManaging = true;
            int Status = 1;
            switch (Type)
            {
                case "NewSupervisions":
                    Status = 8;
                    break;
                case "ForSignatureSupervisions":
                    Status = 66;
                    break;
                case "AcceptedSupervisions":
                    Status = 67;
                    break;
                case "NotCompleteSupervisions":
                    Status = 62;
                    break;
                case "AllSupervisions":
                    Status = 1;
                    break;
            }
            oModel.ListBuildingSupervision = SupervisionCom.SupervisionsByInspectorID(oEmployeeInfo.EMP_NO, Status, IsManaging);
            //switch (Type)
            //{
            //    case "NewSupervisions":
            //        oModel.ListBuildingSupervision = SupervisionCom.GetAllSupervisionsByFlowStatus(8);
            //        break;

            //    case "CanceledSupervisions":
            //        oModel.ListBuildingSupervision = SupervisionCom.GetAllSupervisionsByFlowStatus(64);
            //        break;

            //    case "AcceptedSupervisions":
            //        oModel.ListBuildingSupervision = SupervisionCom.GetAllSupervisionsByFlowStatus(67);
            //        break;

            //    case "NotCompleteSupervisions":
            //        oModel.ListBuildingSupervision = SupervisionCom.GetAllSupervisionsByFlowStatus(62);
            //        break;

            //    case "AllSupervisions":
            //        oModel.ListBuildingSupervision = SupervisionCom.AllSupervisions();
            //        break;
            //}
            return PartialView("_SupervisionsList", oModel);
        }


        public ActionResult SupervisionDetails(int Id = -99)
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }
            SupervisionViewModel oModel = new SupervisionViewModel();
            oModel.BuildingSupervision = SupervisionCom.SupervisionsById(Id);
            if (oModel.BuildingSupervision.ContractorCR_No != null)
            {
                oModel.Contractor = SupervisionCom.ContractorByCRNO(oModel.BuildingSupervision.ContractorCR_No);
            }
            TempData["ErrorMessage"] = null;
            ViewBag.DDWelayat = DDWelayat();
            ViewBag.DDRegion = DDRegionSaved(oModel.BuildingSupervision.BldPermits.WelayahID);
            //ViewBag.DDArea = DDAreaSaved(oModel.BuildingPermits.RegionID);
            ViewBag.DDBuildingTypes = DDBuildingTypes();
            ViewBag.DDLandUseTypes = DDLandUseTypes();
            ViewBag.DDSquareLetters = DDSquareLetters();
            oModel.ListOfAttachments = PermitsAttachmentsCom.AttachmentsByPermitsID(oModel.BuildingSupervision.BldPermitID, oModel.BuildingSupervision.BldPermits.KrokiNO);
            if (oModel.BuildingSupervision.ServiceTypeID == 23)
            {
                ViewBag.DDRegion = DDRegionSaved(oModel.BuildingSupervision.WelayahID);
                oModel.ListOfAttachments = PermitsAttachmentsCom.AttachmentsBySupervisionID(oModel.BuildingSupervision.ID, oModel.BuildingSupervision.KrokiNO);
            }
            //ViewBag.DDSupervisionsStatus = DDSupervisionsStatus();
            List<SelectListItem> Statuslist = DDSupervisionsStatus();
            //foreach (SelectListItem item in Statuslist.ToList())
            //{
            //    if (item.Value == "51")
            //    {
            //        Statuslist.Remove(item);
            //    }
            //}
            ViewBag.DDSupervisionsStatus = Statuslist;
            oModel.ListOfOwners = PermitsCom.OwnersByPermitID(oModel.BuildingSupervision.BldPermitID);
            oModel.Payments = PaymentsCom.PaymentsBySupervisionID(oModel.BuildingSupervision.ID);
            oModel.PaymentDetailsList = PaymentsCom.MapsPaymentDetailsBySupervisionID(oModel.BuildingSupervision.ID);

            if (oModel.oEmployeeInfo.IsSupervisionHead || oModel.oEmployeeInfo.IsEngineerManager)
            {
                ViewBag.DDEngineersList = DDEngineers();
                if (oModel.oEmployeeInfo.IsSupervisionHead)
                {
                    return View("HeadSupervisionDetails", oModel);
                }
                return View("ManagerSupervisionDetails", oModel);
            }


            return View("InspectorDetails", oModel);
        }
        #region Method :: Assign Supervision
        public ActionResult AssignSupervision(SupervisionViewModel oModel)
        {
            int? dmEngNo = SupervisionCom.SupervisionsById(oModel.BuildingSupervision.ID).DmEngineerNo;

            if (oModel.BuildingSupervision.DmEngineerNo != dmEngNo)
            {
                BuildingSupervision Supervisions = SupervisionCom.AssignSupervisions(oModel.BuildingSupervision);
                oModel.BuildingSupervision = Supervisions;
                return RedirectToAction("Index");
            }
            SaveInspectorDetails(oModel);

            return RedirectToAction("Index");
        }
        #endregion

        #region Method :: Save Supervision
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveInspectorDetails(SupervisionViewModel oModel)
        {
            string Result = SupervisionCom.SaveInspectorDetails(oModel);

            if (Result == "ok")
            {
                var consaltantMobileNo = 0;
                if (oModel.BuildingSupervision.ConsultantCivilId != null)
                {
                    consaltantMobileNo = DMeServices.Models.Common.UserCom.UserByCivilID((int)oModel.BuildingSupervision.ConsultantCivilId).MobileNo;
                }
                oModel.ListOfOwners = PermitsCom.OwnersByPermitID(oModel.BuildingSupervision.BldPermitID);
                switch (oModel.BuildingSupervision.Status)
                {
                    case 62:
                        if (consaltantMobileNo != 0)
                            DMeServices.Models.Common.SmsCom.SendSms("968" + consaltantMobileNo, " : تم تعليق معاملتكم لحين استيفاء البيانات رقم المعاملة " + oModel.BuildingSupervision.TransactNo);
                        //DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.BuildingPermits.OwnerPhoneNo, " : تم تعليق معاملتكم لحين استيفاء البيانات  رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                        break;
                    case 64:
                        if (consaltantMobileNo != 0)
                            DMeServices.Models.Common.SmsCom.SendSms("968" + consaltantMobileNo, " : يوجد بعض الملاحظات علي طلبكم رقم المعاملة " + oModel.BuildingSupervision.TransactNo);
                        //DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.BuildingPermits.OwnerPhoneNo, " : يوجد بعض التعديلات علي الخرائط رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                        break;
                    case 65:
                        if (consaltantMobileNo != 0)
                        {
                            DMeServices.Models.Common.SmsCom.SendSms("968" + consaltantMobileNo, " : تم قبول معاملتكم وفي انتظار الدفع رقم المعاملة " + oModel.BuildingSupervision.TransactNo);
                        }
                        foreach (var owner in oModel.ListOfOwners)
                        {

                            DMeServices.Models.Common.SmsCom.SendSms("968" + owner.Phone, " : تم قبول معاملتكم وفي انتظار الدفع رقم المعاملة " + oModel.BuildingSupervision.TransactNo);
                        }
                        break;
                }
                TempData["ErrorMessage"] = "تم حفظ المعاملة .. برجاء المتابعة او العودة للصفحة السابقة";
            }
            return RedirectToAction("SupervisionDetails", "BuildingSupervision", new { Id = oModel.BuildingSupervision.ID });
        }
        #endregion

        [HttpPost]
        public ActionResult RefuseAppointment(string bldID)
        {
            SupervisionViewModel oModel = new SupervisionViewModel();
            oModel.BuildingSupervision = SupervisionCom.SupervisionsById(int.Parse(bldID));
            string Result = SupervisionCom.RefuseOwnerNotesRequests(oModel);
            return Json(new
            {
                msg = "تم رفض طلب المقابلة بنجاح"
            });
        }
        [HttpPost]
        public ActionResult AcceptAppointment(string bldID)
        {
            SupervisionViewModel oModel = new SupervisionViewModel();
            oModel.BuildingSupervision = SupervisionCom.SupervisionsById(int.Parse(bldID));
            string Result = SupervisionCom.AcceptOwnerNotesRequests(oModel);
            return Json(new
            {
                msg = "تمت المقابلة بنجاح"
            });
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
            Stream stream = new MemoryStream();
            if (Attachment.AttachmentStream == null || Attachment.AttachmentStream.Length <= 0)
            {
                System.IO.FileStream oFile = new FileStream(Attachment.AttachmentPath, FileMode.Open, FileAccess.Read);
                oFile.CopyTo(stream);
            }
            else
            {
                stream = new MemoryStream(Attachment.AttachmentStream);
            }

            if (Attachment != null)
            {

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
                LstEngineers.Add(new SelectListItem() { Text = "أختر مفتش ", Value = "0" });
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

        private static string dirName(SupervisionViewModel oModel)
        {
            string directoryName;
            if (string.IsNullOrEmpty(oModel.BuildingPermits.KrokiNO))
            {
                directoryName = oModel.BuildingPermits.ConsultantCivilId.ToString() + "_" + oModel.BuildingPermits.ConsultantCrNo.ToString();
            }
            else
            {
                directoryName = oModel.BuildingPermits.KrokiNO;
            }
            return directoryName;
        }

        #region Method :: List Attachment Details
        public ActionResult SelectAttachment(int Id = -99)
        {
            SupervisionViewModel oModel = new SupervisionViewModel();
            string folderName = dirName(oModel);
            oModel.ListOfAttachments = DMeServices.Models.Common.BuildingServices.PermitsAttachmentsCom.AttachmentsByPermitsID(Id, folderName);
            return PartialView("_ListAttachments", oModel);
        }
        #endregion

        #region Method :: DD Supervisions Status
        public static List<SelectListItem> DDSupervisionsStatus()
        {
            List<SelectListItem> LstSupervisionsStatus = new List<SelectListItem>();
            List<LookupType> SupervisionsStatus = DMeServices.Models.Common.LookupsTypeCom.LookupByDesc("SupervisionStatus");
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
            SupervisionViewModel oModel = new SupervisionViewModel();
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
        #region Method ::Edit PDF Files 
        public ActionResult EditPDFFile(int Id, int PirmID)
        {
            //IExcelDataReader reader = null;
            PermitsAttachments Attachment = DMeServices.Models.Common.BuildingServices.PermitsAttachmentsCom.AttachmentsByID(Id);

            string contentType = MimeMapping.GetMimeMapping(Attachment.AttachmentPath);
            if (Attachment.AttachmentStream == null || Attachment.AttachmentStream.Length <= 0)
            {
                System.IO.FileStream oFile = new FileStream(Attachment.AttachmentPath, FileMode.Open, FileAccess.Read);

                MemoryStream streamToUpdate = new MemoryStream();
                oFile.CopyTo(streamToUpdate);
                byte[] data = streamToUpdate.ToArray();
                Attachment.AttachmentStream = data;
                UpdateStream(Attachment);
            }

            if (Attachment != null)
            {
                Stream stream = new MemoryStream(Attachment.AttachmentStream);
                stream.Position = 0;
                if (Attachment.AttachmentName.EndsWith(".pdf"))
                {
                    // Create RAD PDF control
                    PdfWebControl pdfWebControl1 = new PdfWebControl();
                    // Setup pdfWebControl1 with any properties which must be called before CreateDocument (optional)
                    // e.g. pdfWebControl1.RenderDpi = 144;
                    // Create document from PDF data
                    pdfWebControl1.CreateDocument(Attachment.Id.ToString(), stream);
                    // Put control in ViewBag
                    ViewBag.PdfWebControl1 = pdfWebControl1;
                    ViewBag.DocID = Attachment.Id;
                    //ViewBag.PermitNo = DMeServices.Models.Common.BuildingServices.PermitsCom.PermitsByID(Attachment.BldPermitId).LicenseNo;
                    return PartialView("EditPDFFile");
                    //   return new FileStreamResult(stream, contentType);
                }
                else if (Attachment.AttachmentName.EndsWith(".jpg"))
                {
                    return new FileStreamResult(stream, contentType);
                }
                else if (Attachment.AttachmentName.EndsWith(".png"))
                {
                    return new FileStreamResult(stream, contentType);
                }
                else if (Attachment.AttachmentName.EndsWith(".txt"))
                {
                    return new FileStreamResult(stream, contentType);
                }
            }
            return new EmptyResult();
        }

        public static void UpdateStream(PermitsAttachments attachment)
        {
            PermitsAttachmentsCom.UpdateAttachmentStream((int)attachment.Id, attachment.AttachmentStream);
        }
        #endregion

        #region Method :: Print Supervision Reports
        public ActionResult BuildingSupervisionPrint(int Id = -99)
        {
            SupervisionViewModel oModel = new SupervisionViewModel();
            oModel.BuildingSupervision = SupervisionCom.SupervisionsById(Id);

            if (oModel.BuildingSupervision.ServiceTypeID == 5 && !IsPaymentDone(Id))
                TempData["ErrorMessage"] = "لم يتم دفع كل البنود";
            else
            {
                string saveResult;
                int printSupervisionID = UpdateSupervisionTransact(oModel);
                oModel.BuildingSupervision.Status = 66;
                if (oModel.BuildingSupervision.ServiceTypeID == 10)
                {
                    saveResult = SupervisionCom.UpdateInspectorDetails(oModel);
                    printSupervisionID = Id;
                }
                else
                    saveResult = SupervisionCom.SaveInspectorDetails(oModel);

                if (saveResult == "ok")
                {
                    var serviceType = DMeServices.Models.Common.BuildingServices.SupervisionCom.ServiceByID((int)oModel.BuildingSupervision.ServiceTypeID);
                    var printResult = LoadReport(printSupervisionID.ToString(), oModel.BuildingSupervision.OwnerCivilId.ToString(), serviceType);
                    switch (printResult.Content)
                    {
                        case "M1":
                            TempData["ErrorMessage"] = "لا يمكن طباعة التصريح .. بيانات غير صحيحة لطباعة تصريح الخدمة";
                            break;
                        case "M2":
                            TempData["ErrorMessage"] = "لا يمكن طباعة التصريح .. بيانات غير صحيحة لتصريح الشروع بالعمل";
                            break;
                        case "M3":
                            TempData["ErrorMessage"] = "لا يمكن طباعة التصريح .. بيانات غير صحيحة لاسترداد التأمين";
                            break;
                        default:
                            InsertPrintedPermitsInAttachments(oModel, serviceType.ServiceNameAR, printResult.Content);
                            break;
                    }
                    TempData["ErrorMessage"] = "تم حفظ المعاملة .. برجاء المتابعة او العودة للصفحة السابقة";
                }
                else
                {
                    TempData["ErrorMessage"] = saveResult;
                }
            }
            return RedirectToAction("SupervisionDetails", "BuildingSupervision", new { Id = oModel.BuildingSupervision.ID });
            //return Redirect("~/Reports/Report.aspx?id=" + Id.ToString() + "&civilid=" + oModel.BuildingPermits.OwnerCivilId.ToString());
        }

        private static void InsertPrintedPermitsInAttachments(SupervisionViewModel oModel, string serviceNameAR, string path)
        {
            int ServiceTypeID = oModel.BuildingSupervision.ServiceTypeID;
            oModel.Attachments = new PermitsAttachments();
            if (oModel.BuildingSupervision.ServiceTypeID == 23)
            {
                ServiceTypeID = 4;
                oModel.Attachments.BldSupervisionID = oModel.BuildingSupervision.ID;
            }
            oModel.Attachments.AttachmentPath = path;
            oModel.Attachments.AttachmentTypeId = ServiceTypeID + 30;
            oModel.Attachments.BldPermitId = oModel.BuildingSupervision.BldPermitID;
            oModel.Attachments.Description = serviceNameAR;
            oModel.Attachments.AttachmentContentType = ".pdf";
            oModel.Attachments.CreatedBy = oModel.oEmployeeInfo.NAME;
            oModel.Attachments.AttachmentName = Path.GetFileName(path);

            PermitsAttachmentsCom.InsertPrintedPermits(oModel.Attachments);
        }

        private static int UpdateSupervisionTransact(SupervisionViewModel oModel)
        {
            int printSupervisionID = oModel.BuildingSupervision.ID;
            if (oModel.BuildingSupervision.LicenseNo != null)
            {
                var transactions = DMeServices.Models.Common.BuildingServices.SupervisionCom.SupervisionsTransactBySupervisionLicenseNo(oModel.BuildingSupervision.LicenseNo);
                if (transactions != null && transactions.Count > 0)
                {
                    foreach (var transaction in transactions)
                    {
                        if (transaction.TypeId == oModel.BuildingSupervision.ServiceTypeID)
                        {
                            printSupervisionID = (int)transaction.BldSupervisionId;
                            SupervisionCom.UpdateSupervisionTransact(oModel, transaction.Id);
                        }
                    }
                }
            }

            return printSupervisionID;
        }

        private bool IsPaymentDone(int id)
        {
            List<Payments> payments = PaymentsCom.PaymentsBySupervisionID(id);
            var paymentDone = payments.Where(x => x.PaymentStatus == 1).ToList();
            if (payments.Count > 0 && paymentDone.Count > 0 && payments.Count == paymentDone.Count)
                return true;
            return false;
        }

        private ContentResult LoadReport(string id, string civilid, SupervisionServicesTypes service)
        {
            ReportDataSource rds = new ReportDataSource();
            switch (service.ID)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 7:
                case 8:
                    Reports.InternalEngineeringDataSetTableAdapters.UnpaidServicesTableAdapter uta = new Reports.InternalEngineeringDataSetTableAdapters.UnpaidServicesTableAdapter();
                    InternalEngineeringDataSet.UnpaidServicesDataTable udt = new InternalEngineeringDataSet.UnpaidServicesDataTable();
                    uta.Fill(udt, Convert.ToInt32(id));
                    if (udt != null)
                    {
                        if (udt.Rows.Count <= 0)
                            return Content("M1");
                    }
                    rds.Name = "UnpaidServices";
                    rds.Value = udt;
                    break;
                case 5:
                case 6:
                case 9:
                    Reports.InternalEngineeringDataSetTableAdapters.BldSupervisionServicesTableAdapter pta = new Reports.InternalEngineeringDataSetTableAdapters.BldSupervisionServicesTableAdapter();
                    InternalEngineeringDataSet.BldSupervisionServicesDataTable pdt = new InternalEngineeringDataSet.BldSupervisionServicesDataTable();
                    pta.Fill(pdt, Convert.ToInt32(id));
                    if (pdt != null)
                    {
                        if (pdt.Rows.Count <= 0)
                            return Content("M2");
                    }
                    rds.Name = "Supervisions";
                    rds.Value = pdt;
                    break;
                case 10:
                    Reports.InternalEngineeringDataSetTableAdapters.AlreadyPaidServicesTableAdapter ata = new Reports.InternalEngineeringDataSetTableAdapters.AlreadyPaidServicesTableAdapter();
                    InternalEngineeringDataSet.AlreadyPaidServicesDataTable adt = new InternalEngineeringDataSet.AlreadyPaidServicesDataTable();
                    ata.Fill(adt, Convert.ToInt32(id));
                    if (adt != null)
                    {
                        if (adt.Rows.Count <= 0)
                        {
                            return Content("M3");
                        }
                    }
                    rds.Name = "AlreadyPaidServices";
                    rds.Value = adt;
                    break;
                case 23:
                    Reports.InternalEngineeringDataSetTableAdapters.SpecialSignsTableAdapter sta = new Reports.InternalEngineeringDataSetTableAdapters.SpecialSignsTableAdapter();
                    InternalEngineeringDataSet.SpecialSignsDataTable sdt = new InternalEngineeringDataSet.SpecialSignsDataTable();
                    sta.Fill(sdt, Convert.ToInt32(id));
                    if (sdt != null)
                    {
                        if (sdt.Rows.Count <= 0)
                            return Content("M1");
                    }
                    rds.Name = "SpecialSigns";
                    rds.Value = sdt;
                    break;
            }
            ReportViewer reportViewer1 = new ReportViewer();
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            switch (service.ID)
            {
                case 1:
                case 8:
                    reportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//rpt//PrintElectricityRpt.rdlc");
                    break;
                case 2:
                    reportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//rpt//PrintWaterRpt.rdlc");
                    break;
                case 3:
                    reportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//rpt//PrintTechnicalRpt.rdlc");
                    break;
                case 4:
                    reportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//rpt//PrintSignsRpt.rdlc");
                    break;
                case 5:
                    reportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//rpt//PrintStartBuildingRpt.rdlc");
                    break;
                case 6:
                case 9:
                    reportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//rpt//PrintInsuranceRpt.rdlc");
                    break;
                case 7:
                    reportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//rpt//PrintBuildingCertificateRpt.rdlc");
                    break;
                case 10:
                    reportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//rpt//PrintChangeConsultantRpt.rdlc");
                    break;
                case 23:
                    reportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//rpt//PrintSpecialSignsRpt.rdlc");
                    break;
            }
            reportViewer1.LocalReport.DataSources.Clear();
            ReportParameter parameter = new ReportParameter("ID", id);
            reportViewer1.LocalReport.SetParameters(parameter);
            reportViewer1.LocalReport.DataSources.Add(rds);
            string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>PDF</OutputFormat>" +
                    "  <PageWidth>8.27in</PageWidth>" +
                    "  <PageHeight>11.69in</PageHeight>" +
                    "  <MarginTop>0in</MarginTop>" +
                    "  <MarginLeft>0in</MarginLeft>" +
                    "  <MarginRight>0in</MarginRight>" +
                    "  <MarginBottom>0in</MarginBottom>" +
                    "  <EmbedFonts>None</EmbedFonts>" +
                    "</DeviceInfo>";
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            byte[] bytes = reportViewer1.LocalReport.Render(
                "PDF",
                deviceInfo,
                out mimeType,
                out encoding,
                out extension,
                out streamIds,
                out warnings);
            //it was : "~/SupervisionReports/PrintedFiles/"
            //now is : "~/Files/PrintedFiles/Supervision/"
            var tempPath = System.Web.HttpContext.Current.Server.MapPath("~/Files/PrintedFiles/Supervision/" + service.ServiceNameEn + "/" + civilid);
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
            //oFile.SaveAs(StrUploadPath);
            var saveAs = string.Format("{0}.pdf", Path.Combine(tempPath, civilid + "_" + id));

            var idx = 0;
            while (System.IO.File.Exists(saveAs))
            {
                idx++;
                saveAs = string.Format("{0}_{1}.pdf", Path.Combine(tempPath, civilid + "_" + id), idx);
            }

            using (var stream = new FileStream(saveAs, FileMode.Create, FileAccess.Write))
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
            }
            return Content(saveAs);
            //Response.Buffer = true;
            //Response.Clear();
            //Response.ContentType = mimeType;
            //Response.AddHeader(
            //    "content-disposition",
            //    "attachment; filename= filename" + "." + extension);
            //Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
            //Response.Flush(); // send it to the client to download  
            //Response.End();

            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/"+"Reports//rpt//PrintPermit.rdlc");
            //reportViewer1.LocalReport.Refresh();
            //reportViewer1.ZoomMode = ZoomMode.PageWidth;
        }
        #endregion

        #region Method :: Delete Fee
        [HttpPost]
        public ActionResult DeleteFee(int Id)
        {
            SupervisionViewModel oModel = new SupervisionViewModel();
            oModel.PaymentDetailsList = (List<PaymentDetails>)TempData["PaymentDetails"];
            oModel.PaymentDetails = oModel.PaymentDetailsList[Id];
            oModel.PaymentDetailsList.Remove(oModel.PaymentDetails);
            TempData["PaymentDetails"] = oModel.PaymentDetailsList;
            return PartialView("_ListPayments", oModel);
        }
        #endregion
    }


}