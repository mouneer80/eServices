using DMeServices.Models;
using DMeServices.Models.BuildingServices;
using DMeServices.Models.Common.BuildingServices;
using DMeServices.Models.ViewModels.Internal.Permits;
using DMeServicesInternal.Web.Reports;
using Microsoft.Reporting.WebForms;
using RadPdf.Web.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DMeServicesInternal.Web.Controllers
{
    public class BuildingPermitsController : BaseController
    {
        // GET: BuildingPermits
        public ActionResult Index()
        {
            PermitsViewModel oModel = new PermitsViewModel();
            return View(oModel);
        }

        [HttpPost]
        public ActionResult SaveNotes(string acatext, string id)
        {
            PermitsViewModel model = new PermitsViewModel();
            //Look for form field with the name "test" and see if it is null or has no value 
            var empNo = model.oEmployeeInfo.EMP_NO;
            // create a dummy Bitmap just to get the Graphics object
            Bitmap img = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(img);

            // The font for our text
            Font f = new Font("Arial", 35, FontStyle.Bold);

            // work out how big the text will be when drawn as an image
            SizeF size = g.MeasureString(acatext, f);

            // create a new Bitmap of the required size
            img = new Bitmap((int)Math.Ceiling(size.Width), (int)Math.Ceiling(size.Height));
            g = Graphics.FromImage(img);

            // give it a white background
            g.Clear(Color.Transparent);

            // draw the text in black
            g.DrawString(acatext, f, Brushes.Black, 0, 0);
            var location = Path.Combine(
                        Server.MapPath("~/Images/Notes/"), "note" + id + ".png");
            // save the image
            img.Save(location);

            //var location = Path.Combine(
            //    Server.MapPath("~/Images/Notes/"), fileName);
            //file.SaveAs(location);
            return Json("File uploaded" + acatext);
        }

        #region Method :: Permits
        public ActionResult PermitsList(string Type)
        {
            PermitsViewModel oModel = new PermitsViewModel();
            oEmployeeInfo = (Employee)System.Web.HttpContext.Current.Session["EmployeeInfo"];
            bool IsManaging = false;
            if (oEmployeeInfo.IsEngineerHead || oEmployeeInfo.IsEngineerManager)
                IsManaging = true;
            int Status = 1;
            switch (Type)
            {
                case "NewPermits":
                    Status = 8;
                    break;
                case "ToCompletePermits":
                    Status = 10;
                    break;
                case "PayedPermits":
                    Status = 29;
                    break;
                case "AcceptedPermits":
                    Status = 30;
                    break;
                case "NotCompletePermits":
                    Status = 18;
                    break;
                case "AllPermits":
                    Status = 1;
                    break;
            }
            oModel.ListBuildingPermits = PermitsCom.PermitsByEngineerID(oEmployeeInfo.EMP_NO, Status, IsManaging);
            return PartialView("_PermitsList", oModel);
        }
        #endregion

        private static string dirName(PermitsViewModel oModel)
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

        #region Method :: Permits Details
        public ActionResult PermitDetails(int Id, string errorMessage)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
            }
            PermitsViewModel oModel = new PermitsViewModel();
            oModel.BuildingPermits = PermitsCom.PermitsByID(Id);
            //if(oModel.BuildingPermits.ServiceName == "21")
            //{
            //    oModel.BuildingPermits = PermitsCom.PermitsByIDWithLicenseNo(Id);
            //}
            ViewBag.DDWelayat = DDWelayat();
            ViewBag.DDRegion = DDRegionSaved(oModel.BuildingPermits.WelayahID);
            //ViewBag.DDArea = DDAreaSaved(oModel.BuildingPermits.RegionID);
            ViewBag.DDBuildingTypes = DDBuildingTypes();
            ViewBag.DDLandUseTypes = DDLandUseTypes();
            ViewBag.DDSquareLetters = DDSquareLetters();
            string folderName = dirName(oModel);
            if (oModel.BuildingPermits.ServiceName == "21")
            {
                oModel.ListOfAttachments = PermitsAttachmentsCom.AttachmentsByLicenseNo(oModel.BuildingPermits.LicenseNo, folderName);
            }
            else
            {
                oModel.ListOfAttachments = PermitsAttachmentsCom.AttachmentsByPermitsID(Id, folderName);
            }
            //ViewBag.DDPermitsStatus = DDPermitsStatus();
            oModel.ListOfOwners = PermitsCom.OwnersByPermitID(Id);
            oModel.Payments = PaymentsCom.PaymentsByPermitsID(Id);
            oModel.PaymentDetailsList = PaymentsCom.MapsPaymentDetailsByPermitsID(Id);

            List<SelectListItem> Statuslist = DDPermitsStatus();
            foreach (SelectListItem item in Statuslist.ToList())
            {
                if (item.Value == "8")
                {
                    Statuslist.Remove(item);
                }
            }
            ViewBag.DDPermitsStatus = Statuslist;

            if (oModel.oEmployeeInfo.IsEngineerHead || oModel.oEmployeeInfo.IsEngineerManager)
            {
                ViewBag.DDPermitsStatus = DDPermitsStatus();
                ViewBag.DDEngineersList = DDEngineers();
                if (oModel.oEmployeeInfo.IsEngineerHead)
                {
                    return View("HeadPermitDetails", oModel);
                }
                return View("ManagerPermitDetails", oModel);
            }
            return View("EngineerPermitDetails", oModel);
        }
        #endregion

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
                //byte[] data = stream.ToArray();
                //Attachment.AttachmentStream = data;
                //UpdateStream(Attachment);
            }
            else
            {
                stream = new MemoryStream(Attachment.AttachmentStream);
            }
            if (Attachment != null)
            {
                //FileStream file = new FileStream(Attachment.AttachmentPath, FileMode.Open, FileAccess.Read);

                //Stream stream = new MemoryStream(Attachment.AttachmentStream);
                //Stream stream = new MemoryStream();
                //file.CopyTo(stream);
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
                else if (Attachment.AttachmentName.EndsWith(".txt"))
                {
                    return new FileStreamResult(stream, contentType);
                }
            }
            return new EmptyResult();
        }
        #endregion

        #region Method :: Display Details File
        public ActionResult GetDetailsFile(int ID)
        {
            ViewBag.ID = ID;

            return PartialView("_ViewDetailsFile");
        }

        public ActionResult DisplayDetailsFiles(int Id = -99)
        {
            //IExcelDataReader reader = null;
            PermitsAttachmentDetails Attachment = DMeServices.Models.Common.BuildingServices.PermitsAttachmentsCom.AttachmentDetailsByID(Id);
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
                else if (Attachment.AttachmentName.EndsWith(".txt"))
                {
                    return new FileStreamResult(stream, contentType);
                }
            }
            return new EmptyResult();
        }
        #endregion

        #region Method :: Edit PDF Files 
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
                    ViewBag.PermitNo = DMeServices.Models.Common.BuildingServices.PermitsCom.PermitsByID(Attachment.BldPermitId).LicenseNo;
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

        #region Method :: DD Engineers

        public static List<SelectListItem> DDEngineers()
        {
            List<SelectListItem> LstEngineers = new List<SelectListItem>();
            List<Employee> AllEngineers = DMeServices.Models.Common.EmployeeCom.EmployeeByJobID(1);
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

        #region Method :: Assign Permit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignPermits(PermitsViewModel oModel)
        {
            if (oModel.BuildingPermits.WorkflowStatus == 18 || oModel.BuildingPermits.WorkflowStatus == 19)
            {
                string Result = PermitsCom.SaveEngineerPermits(oModel);
            }
            else
            {
                BuildingPermits permits = PermitsCom.AssignPermits(oModel.BuildingPermits);
                oModel.BuildingPermits = permits;
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Method :: Save Engineer Permits
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEngineerPermits(PermitsViewModel oModel)
        {
            string Result = PermitsCom.SaveEngineerPermits(oModel);
            string ErrorMessage;
            if (Result == "ok")
            {
                var User = DMeServices.Models.Common.UserCom.UserByCivilID((int)oModel.BuildingPermits.ConsultantCivilId);
                oModel.ListOfOwners = PermitsCom.OwnersByPermitID(oModel.BuildingPermits.Id);

                switch (oModel.BuildingPermits.WorkflowStatus)
                {
                    case 18:
                        DMeServices.Models.Common.SmsCom.SendSms("968" + User.MobileNo, " : تم تعليق معاملتكم لحين استيفاء المرفقات رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                        //DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.BuildingPermits.OwnerPhoneNo, " : تم تعليق معاملتكم لحين استيفاء البيانات  رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                        break;
                    case 20:
                        DMeServices.Models.Common.SmsCom.SendSms("968" + User.MobileNo, " : يوجد بعض التعديلات علي الخرائط رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                        //DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.BuildingPermits.OwnerPhoneNo, " : يوجد بعض التعديلات علي الخرائط رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                        break;
                    case 28:
                        DMeServices.Models.Common.SmsCom.SendSms("968" + User.MobileNo, " : تم قبول معاملتكم وفي انتظار الدفع رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                        foreach (var Owner in oModel.ListOfOwners)
                        {

                            DMeServices.Models.Common.SmsCom.SendSms("968" + Owner.Phone, " : تم قبول معاملتكم وفي انتظار الدفع رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                        }
                        break;
                }
                ErrorMessage = "تم حفظ التفاصيل بنجاح";
            }
            else
            {
                ErrorMessage = "حدث خطأ في حفظ البيانات";
            }
            return RedirectToAction("PermitDetails", "BuildingPermits", new { Id = oModel.BuildingPermits.Id, errorMessage = ErrorMessage });
            //return RedirectToAction("Index");
        }
        #endregion

        [HttpPost]
        public ActionResult RefuseAppointment(string bldID)
        {
            PermitsViewModel oModel = new PermitsViewModel();
            oModel.BuildingPermits = PermitsCom.PermitsByID(int.Parse(bldID));
            string Result = PermitsCom.RefuseAppointmentRequests(oModel);
            return Json(new
            {
                msg = "تم رفض طلب المقابلة بنجاح"
            });
        }
        [HttpPost]
        public ActionResult CancelAppointment(string bldID)
        {
            PermitsViewModel oModel = new PermitsViewModel();
            oModel.BuildingPermits = PermitsCom.PermitsByID(int.Parse(bldID));
            string Result = PermitsCom.CancelAppointmentRequests(oModel);
            return Json(new
            {
                msg = "تمت المقابلة بنجاح"
            });
        }
        [HttpPost]
        public ActionResult AcceptAppointment(string bldID, string appointmentDate)
        {
            PermitsViewModel oModel = new PermitsViewModel();
            oModel.BuildingPermits = PermitsCom.PermitsByID(int.Parse(bldID));
            string Result = PermitsCom.AcceptAppointmentRequests(oModel, appointmentDate);
            return Json(new
            {
                msg = "تم تحديد طلب للمقابلة بنجاح"
            });
        }

        #region Method :: List Attachment Details

        public ActionResult SelectAttachmentDetails(int Id = -99)
        {
            PermitsViewModel oModel = new PermitsViewModel();
            oModel.ListOfAttachmentDetails = DMeServices.Models.Common.BuildingServices.PermitsAttachmentsCom.AttachmentsDetailsByAttachmentsID(Id);
            return PartialView("_ListAttachmentsDetails", oModel);
        }
        #endregion

        #region Method :: List Payment Details

        public ActionResult SelectPaymentDetails(int Id = -99)
        {
            PermitsViewModel oModel = new PermitsViewModel();
            oModel.PaymentDetailsList = DMeServices.Models.Common.BuildingServices.PaymentsCom.PaymentDetailsByPaymentID(Id);
            return PartialView("_ListPaymentsDetails", oModel);
        }


        #endregion

        #region Method :: List Attachment Details
        [HttpPost]
        public ActionResult SelectAttachmentDetails(PermitsViewModel oModel, int Id = -99)
        {
            oModel.ListOfAttachmentDetails = DMeServices.Models.Common.BuildingServices.PermitsAttachmentsCom.AttachmentsDetailsByAttachmentsID(Id);
            return PartialView("_ListAttachmentsDetails", oModel);
        }
        #endregion

        #region Method :: DD Permits Status

        public static List<SelectListItem> DDPermitsStatus()
        {
            List<SelectListItem> LstPermitsStatus = new List<SelectListItem>();
            List<LookupType> PermitsStatus = DMeServices.Models.Common.LookupsTypeCom.LookupByDesc("PermitsStatus");
            if (PermitsStatus.Count > 0)
            {
                LstPermitsStatus.Add(new SelectListItem() { Text = "أختر حالة الطلب ", Value = "0" });
                foreach (var item in PermitsStatus)
                {
                    LstPermitsStatus.Add(new SelectListItem() { Text = item.LookupNameAr, Value = item.LookupId.ToString() });
                }
            }
            return LstPermitsStatus;
        }

        #endregion

        #region Method :: Fees and Payments

        public JsonResult GetFees(string id)
        {
            var fee = ServiceFeesCom.TypeByID(int.Parse(id));

            return Json(fee.ServiceFees.ToString(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PermitFees(int Id = -99)
        {
            PermitsViewModel oModel = new PermitsViewModel();
            oModel.BuildingPermits = PermitsCom.PermitsByID(Id);
            TempData["PaymentDetails"] = new List<PaymentDetails>();
            ViewBag.DDServices = DDServiceFees();
            //oModel.BuildingPermits.WorkflowStatus = 28;
            //return View("ErrorPage");
            return View("PermitPaymentDetails", oModel);
            //return Redirect("~/Reports/Report.aspx");
        }

        public ActionResult PermitPrint(int Id = -99)
        {
            PermitsViewModel oModel = new PermitsViewModel();
            oModel.BuildingPermits = PermitsCom.PermitsByID(Id);
            List<Payments> payments = PaymentsCom.PaymentsByPermitsID(Id);
            var paymentDone = payments.Where(x => x.PaymentStatus == 1).ToList();
            string ErrorMessage;
            string serviceName = SupervisionCom.ServiceByID(Int32.Parse(PermitsCom.ServiceNameByID(Id))).ServiceNameAR;
            if (payments.Count > 0 && paymentDone.Count > 0 && payments.Count == paymentDone.Count)
            {
                ErrorMessage = "";
                string folderName = dirName(oModel);
                var result = LoadReport(Id.ToString(), folderName);
                oModel.Attachments = new PermitsAttachments();
                oModel.Attachments.AttachmentPath = result.Content;
                oModel.Attachments.AttachmentTypeId = 30;
                oModel.Attachments.BldPermitId = Id;
                oModel.Attachments.Description = serviceName;
                oModel.Attachments.AttachmentContentType = ".pdf";
                oModel.Attachments.CreatedBy = oModel.oEmployeeInfo.NAME;
                oModel.Attachments.AttachmentName = Path.GetFileName(result.Content);
                PermitsAttachmentsCom.InsertPrintedPermits(oModel.Attachments);
                //to save workflowStatus as printed
                oModel.BuildingPermits.WorkflowStatus = 29;
                PermitsCom.SaveEngineerPermits(oModel);
            }
            else
            {
                ErrorMessage = "لم يتم دفع كل البنود";
                //return View("PermitDetails", oModel); 
            }
            return RedirectToAction("PermitDetails", "BuildingPermits", new { Id = oModel.BuildingPermits.Id, errorMessage = ErrorMessage });
            //return Redirect("~/Reports/Report.aspx?id=" + Id.ToString() + "&civilid=" + oModel.BuildingPermits.OwnerCivilId.ToString());
        }

        private ContentResult LoadReport(string id, string FolderName)
        {
            string serviceName = SupervisionCom.ServiceByID(Int32.Parse(PermitsCom.ServiceNameByID(Int32.Parse(id)))).ServiceNameEn;
            Reports.InternalEngineeringDataSetTableAdapters.BldPermitsTableAdapter ta = new Reports.InternalEngineeringDataSetTableAdapters.BldPermitsTableAdapter();
            InternalEngineeringDataSet.BldPermitsDataTable dt = new InternalEngineeringDataSet.BldPermitsDataTable();
            ta.Fill(dt, Convert.ToInt32(id));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "Permits";
            rds.Value = dt;
            ReportViewer reportViewer1 = new ReportViewer();
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            switch (serviceName)
            {
                case "New Permit":
                    reportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//rpt//PrintPermit.rdlc");
                    break;
                case "Demolition":
                    reportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//rpt//PrintDemolitionRpt.rdlc");
                    break;
                case "Renovation":
                    reportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//rpt//PrintRenovationRpt.rdlc");
                    break;
                case "Fencing":
                    reportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//rpt//PrintFencingRpt.rdlc");
                    break;
                case "Extension Permit":
                    reportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//rpt//PrintExtensionRpt.rdlc");
                    break;
                case "Modify Permit":
                    reportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//rpt//PrintModifyRpt.rdlc");
                    break;
            }
            //eServicesEntities entities = new eServicesEntities();
            //ReportDataSource datasource = new ReportDataSource("Permits", entities.BldPermits);

            reportViewer1.LocalReport.DataSources.Clear();

            ReportParameter parameter = new ReportParameter("ID", id);
            reportViewer1.LocalReport.SetParameters(parameter);
            reportViewer1.LocalReport.DataSources.Add(rds);
            string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>PDF</OutputFormat>" +
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
            //it was : "~/Images/PrintedFiles/"
            //now is : "~/Files/PrintedFiles/Permits/"
            var tempPath = System.Web.HttpContext.Current.Server.MapPath("~/Files/PrintedFiles/Permits/" + FolderName);
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
            //oFile.SaveAs(StrUploadPath);
            var saveAs = string.Format("{0}.pdf", Path.Combine(tempPath, FolderName + "_" + id));

            var idx = 0;
            while (System.IO.File.Exists(saveAs))
            {
                idx++;
                saveAs = string.Format("{0}_{1}.pdf", Path.Combine(tempPath, FolderName + "_" + id), idx);
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
        public ActionResult SavePermitsFees(PermitsViewModel oModel)
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


            string Result = PaymentsCom.SavePaymentDetailsForPermit(oModel);
            if (oModel.Payment.PaymentTotalAmount == 0)
            {
                oModel.BuildingPermits.WorkflowStatus = 29;
            }
            else
            {
                oModel.BuildingPermits.WorkflowStatus = 28;
            }

            PermitsCom.SaveEngineerPermits(oModel);
            ViewBag.PaymentID = Result;

            return RedirectToAction("PermitDetails", "BuildingPermits", new { Id = oModel.BuildingPermits.Id });
        }

        [HttpPost]
        public ActionResult SavePayment()
        {
            PermitsViewModel oModel = new PermitsViewModel();
            var listofPaymentDetails = (List<PaymentDetails>)TempData["PaymentDetails"];
            //if (listofPaymentDetails != null) return PartialView("_ListPayments", oModel);


            var serviceID = System.Web.HttpContext.Current.Request.Form["ServiceID"];
            var fees = System.Web.HttpContext.Current.Request.Form["Fees"];
            var quantity = System.Web.HttpContext.Current.Request.Form["Quantity"];
            var total = System.Web.HttpContext.Current.Request.Form["Total"];
            oModel.PaymentDetails = new PaymentDetails();
            oModel.PaymentDetails.ServiceQuantity = decimal.Parse(quantity);
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

        #region Method :: Delete Fee
        [HttpPost]
        public ActionResult DeleteFee(int Id)
        {
            PermitsViewModel oModel = new PermitsViewModel();
            oModel.PaymentDetailsList = (List<PaymentDetails>)TempData["PaymentDetails"];
            oModel.PaymentDetails = oModel.PaymentDetailsList[Id];
            oModel.PaymentDetailsList.Remove(oModel.PaymentDetails);
            TempData["PaymentDetails"] = oModel.PaymentDetailsList;
            return PartialView("_ListPayments", oModel);

        }
        #endregion
    }
}