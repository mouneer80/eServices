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

        #region :: Method Permits
        public ActionResult PermitsList(string Type)
        {
            PermitsViewModel oModel = new PermitsViewModel();
            switch (Type)
            {
                case "NewPermits":
                    oModel.ListBuildingPermits = PermitsCom.GetAllPermitsByflowStatus(8);
                    break;

                case "CanceledPermits":
                    oModel.ListBuildingPermits = PermitsCom.GetAllPermitsByflowStatus(18);
                    break;

                case "AcceptedPermits":
                    oModel.ListBuildingPermits = PermitsCom.GetAllPermitsByflowStatus(19);
                    break;

                case "NotCompletePermits":
                    oModel.ListBuildingPermits = PermitsCom.GetAllPermitsByflowStatus(10);
                    break;

                case "AllPermits":
                    oModel.ListBuildingPermits = PermitsCom.AllPermits();
                    break;
            }
            return PartialView("_PermitsList", oModel);
        }
        #endregion

        #region Method :: Peremits Details
        public ActionResult PermitDetails(int Id)
        {
            PermitsViewModel oModel = new PermitsViewModel();
            oModel.BuildingPermits = PermitsCom.PermitsByID(Id);
            ViewBag.DDWelayat = DDWelayat();
            ViewBag.DDRegion = DDRegionSaved(oModel.BuildingPermits.WelayahID);
            //ViewBag.DDArea = DDAreaSaved(oModel.BuildingPermits.RegionID);
            ViewBag.DDBuildingTypes = DDBuildingTypes();
            ViewBag.DDLandUseTypes = DDLandUseTypes();
            ViewBag.DDSquareLetters = DDSquareLetters();
            oModel.ListOfAttachments = PermitsAttachmentsCom.AttachmentsByPermitsID(Id, (long)oModel.BuildingPermits.OwnerCivilId);
            ViewBag.DDPermitsStatus = DDPermitsStatus();
            oModel.Payments = PaymentsCom.PaymentsByPermitsID(Id);
            oModel.PaymentDetailsList = PaymentsCom.MapsPaymentDetailsByPermitsID(Id);
            if (oModel.oEmployeeInfo.IsEngineerHead)
            {
                ViewBag.DDEngineersList = DDEngineers();
                return View("HeadPermitDetails", oModel);
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
                //FileStream file = new FileStream(Attachment.AttachmentPath, FileMode.Open, FileAccess.Read);

                Stream stream = new MemoryStream(Attachment.AttachmentStream);
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
            if (oModel.BuildingPermits.WorkflowStatus != 12 && oModel.BuildingPermits.WorkflowStatus != 0)
            {
                string Result = PermitsCom.SaveEngineerPermits(oModel);

                if (Result == "ok")
                {
                    var User = DMeServices.Models.Common.UserCom.UserByCivilID((long)oModel.BuildingPermits.ConsultantCivilId);

                    switch (oModel.BuildingPermits.WorkflowStatus)
                    {
                        case 18:
                            DMeServices.Models.Common.SmsCom.SendSms("968" + User.MobileNo, " : تم   الغاء المعاملة رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                            DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.BuildingPermits.OwnerPhoneNo, " : تم   الغاء المعاملة رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                            break;

                        case 19:
                            DMeServices.Models.Common.SmsCom.SendSms("968" + User.MobileNo, " : تم قبول المعاملة رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                            DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.BuildingPermits.OwnerPhoneNo, " : تم قبول المعاملة  رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                            break;
                        case 20:
                            DMeServices.Models.Common.SmsCom.SendSms("968" + User.MobileNo, " : يوجد بعض التعديلات علي الخرائط رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                            DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.BuildingPermits.OwnerPhoneNo, " : يوجد بعض التعديلات علي الخرائط رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                            break;
                    }

                }
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

            if (Result == "ok")
            {
                var User = DMeServices.Models.Common.UserCom.UserByCivilID((long)oModel.BuildingPermits.ConsultantCivilId);

                switch (oModel.BuildingPermits.WorkflowStatus)
                {
                    case 18:
                        DMeServices.Models.Common.SmsCom.SendSms("968" + User.MobileNo, " : تم تعليق معاملتكم لحين استيفاء البيانات رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                        DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.BuildingPermits.OwnerPhoneNo, " : تم تعليق معاملتكم لحين استيفاء البيانات  رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                        break;

                    case 19:
                        DMeServices.Models.Common.SmsCom.SendSms("968" + User.MobileNo, " : تم قبول معاملتكم وفي انتظار الدفع رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                        DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.BuildingPermits.OwnerPhoneNo, " : تم قبول معاملتكم وفي انتظار الدفع رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                        break;
                    case 20:
                        DMeServices.Models.Common.SmsCom.SendSms("968" + User.MobileNo, " : يوجد بعض التعديلات علي الخرائط رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                        DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.BuildingPermits.OwnerPhoneNo, " : يوجد بعض التعديلات علي الخرائط رقم المعاملة " + oModel.BuildingPermits.TransactNo);
                        break;
                }
            }
            return RedirectToAction("Index");
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
                    ViewBag.PirmID = PirmID;
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
            return PartialView("_ListPayments", oModel);
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
            var result = LoadReport(Id.ToString(), oModel.BuildingPermits.OwnerCivilId.ToString());
            oModel.Attachments = new PermitsAttachments();
            oModel.Attachments.AttachmentPath = result.Content;
            oModel.Attachments.AttachmentTypeId = 30;
            oModel.Attachments.BldPermitId = Id;
            oModel.Attachments.Description = "رخصة بناء";
            oModel.Attachments.AttachmentContentType = ".pdf";
            oModel.Attachments.CreatedBy = oModel.oEmployeeInfo.NAME;
            oModel.Attachments.AttachmentName = Path.GetFileName(result.Content);

            PermitsAttachmentsCom.InsertPrintedPermits(oModel.Attachments);
            return RedirectToAction("PermitDetails", "BuildingPermits", new { Id = oModel.BuildingPermits.Id });
            //return Redirect("~/Reports/Report.aspx?id=" + Id.ToString() + "&civilid=" + oModel.BuildingPermits.OwnerCivilId.ToString());
        }

        private ContentResult LoadReport(string id, string civilid)
        {
            Reports.InternalEngineeringDataSetTableAdapters.BldPermitsTableAdapter ta = new Reports.InternalEngineeringDataSetTableAdapters.BldPermitsTableAdapter();
            InternalEngineeringDataSet.BldPermitsDataTable dt = new InternalEngineeringDataSet.BldPermitsDataTable();
            ta.Fill(dt, Convert.ToInt64(id));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "Permits";
            rds.Value = dt;
            ReportViewer reportViewer1 = new ReportViewer();
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            reportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//rpt//PrintPermit.rdlc");
            //eServicesEntities entities = new eServicesEntities();
            //ReportDataSource datasource = new ReportDataSource("Permits", entities.BldPermits);

            reportViewer1.LocalReport.DataSources.Clear();

            ReportParameter parameter = new ReportParameter("ID", id);
            reportViewer1.LocalReport.SetParameters(parameter);
            reportViewer1.LocalReport.DataSources.Add(rds);
            string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>PDF</OutputFormat>" +
                    "  <PageWidth>11.69in</PageWidth>" +
                    "  <PageHeight>8.27in</PageHeight>" +
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
            var tempPath = System.Web.HttpContext.Current.Server.MapPath("~/Images/PrintedFiles/" + civilid);
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


            string Result = PaymentsCom.SavePaymentDetails(oModel);
            oModel.BuildingPermits.WorkflowStatus = 28;
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
    }
    #endregion

}