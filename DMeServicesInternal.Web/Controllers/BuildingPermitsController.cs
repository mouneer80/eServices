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
    public class BuildingPermitsController : BaseController
    {
        // GET: BuildingPermits
        public ActionResult Index()
        {
            PermitsViewModel oModel = new PermitsViewModel();

            return View(oModel);
        }




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




        public ActionResult PermitDetails(int Id = -99)
        {
            PermitsViewModel oModel = new PermitsViewModel();
            oModel.BuildingPermits = PermitsCom.PermitsByID(Id);
            ViewBag.DDWelayat = DDWelayat();
            ViewBag.DDRegion = DDRegionSaved(oModel.BuildingPermits.WelayahID);
            //ViewBag.DDArea = DDAreaSaved(oModel.BuildingPermits.RegionID);
            ViewBag.DDBuildingTypes = DDBuildingTypes();
            ViewBag.DDLandUseTypes = DDLandUseTypes();
            ViewBag.DDSquareLetters = DDSquareLetters();
            oModel.ListOfAttachments = PermitsAttachmentsCom.AttachmentsByPermitsID(Id);

            if (oModel.oEmployeeInfo.IsEngineerHead)
            {
                ViewBag.DDEngineersList = DDEngineers();
                return View("HeadPermitDetails", oModel);
            }

            ViewBag.DDPermitsStatus = DDPermitsStatus();
            return View("EngineerPermitDetails", oModel);
        }

        public ActionResult LandSurvey()
        {
            PermitsViewModel oModel = new PermitsViewModel();
            oModel.BuildingPermits = PermitsCom.PermitsByID(81);
            oModel.ListOfAttachments = PermitsAttachmentsCom.AttachmentsByPermitsID(81);


            ViewBag.DDWelayat = DDWelayat();
            ViewBag.DDBuildingTypes = DDBuildingTypes();
            ViewBag.DDLandUseTypes = DDLandUseTypes();
            ViewBag.DDSquareLetters = DDSquareLetters();
            //ViewBag.DDRegions = DDRegions();
            ViewBag.DDPermitsStatus = DDPermitsStatus();
            return View("LandSurvey");
        }

        public ActionResult AssignPermits(PermitsViewModel oModel)
        {
            BuildingPermits permits = PermitsCom.AssignPermits(oModel.BuildingPermits);
            oModel.BuildingPermits = permits;
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
                //    if (filepath.EndsWith(".xls"))
                //    {
                //        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                //    }
                //    else if (filepath.EndsWith(".xlsx"))
                //    {
                //        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                //    }
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
                //    if (filepath.EndsWith(".xls"))
                //    {
                //        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                //    }
                //    else if (filepath.EndsWith(".xlsx"))
                //    {
                //        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                //    }
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

       
        public ActionResult PermitFees(int Id = -99)
        {
            PermitsViewModel oModel = new PermitsViewModel();
            oModel.BuildingPermits = PermitsCom.PermitsByID(Id);
            return Redirect("~/Reports/Report.aspx");
        }


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


            return RedirectToAction("Index");
        }

        #endregion


        #region Method ::Edit PDF Files 
        public ActionResult EditPDFFile(int Id, int PirmID)
        {

                    //IExcelDataReader reader = null;
                    PermitsAttachments Attachment = DMeServices.Models.Common.BuildingServices.PermitsAttachmentsCom.AttachmentsByID(Id);

            string contentType = MimeMapping.GetMimeMapping(Attachment.AttachmentPath);



            if (Attachment != null)
            {

                Stream stream = new MemoryStream(Attachment.AttachmentStream);
                stream.Position = 0;
                //    if (filepath.EndsWith(".xls"))
                //    {
                //        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                //    }
                //    else if (filepath.EndsWith(".xlsx"))
                //    {
                //        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                //    }
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


        #endregion


        #region Method :: List Attachment Details

        public ActionResult SelectAttachmentDetails(int Id = -99)
        {
            PermitsViewModel oModel = new PermitsViewModel();
            oModel.ListOfAttachmentDetails = DMeServices.Models.Common.BuildingServices.PermitsAttachmentsCom.AttachmentsDetailsByAttachmentsID(Id);
            return PartialView("_ListAttachmentsDetails", oModel);
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
    }


}