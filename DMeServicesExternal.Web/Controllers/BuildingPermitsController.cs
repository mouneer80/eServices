using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using DisplayPDFDemo.Comman;
using DMeServices.Models.Common.BuildingServices;
using DMeServices.Models.ViewModels.Permits;
using DMeServices.Models.BuildingServices;
using System.IO;
using System.Net.Http;
using System.Data;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DMeServices.Models.ViewModels;

namespace DMeServicesExternal.Web.Controllers
{
    public class BuildingPermitsController : BaseController
    {
        // GET: BuildingPermits
        public ActionResult Index()
        {
            var oModel = new PermitsViewModel();
            oModel.ListBuildingPermits = PermitsCom.PermitsByConsultantCivilId(oModel.oUserInfo.CivilId);
            oModel.ShowAdd = true;
            return View(oModel);
        }
        public ActionResult LandProjects()
        {
            PermitsViewModel oModel = new PermitsViewModel();
            oModel.ListBuildingPermits = PermitsCom.PermitsByLandOwnerCivilId(oModel.oUserInfo.CivilId);
            oModel.ShowAdd = false;
            return View("Index",oModel);
        }
        public ActionResult CompanyList()
        {
            var oModel = new CompaniesListViewModel();
            return View(oModel);
        }
        public static async Task<DataTable> GetPaymentToken()
        {
            // Initialization.  
            DataTable responseObj = new DataTable();

            // HTTP GET.  
            using (var client = new HttpClient())
            {
                // Setting Base address.  
                client.BaseAddress = new Uri("https://www.dhofar.gov.om/ePaymentAPI/");

                // Setting content type.  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Initialization.  
                HttpResponseMessage response = new HttpResponseMessage();

                // HTTP GET  
                response = await client.GetAsync("API/Paymentrequest/OpenPaymentRequest").ConfigureAwait(false);

                // Verification  
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.  
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<DataTable>(result);
                }
            }

            return responseObj;
        }

        public async Task<ActionResult> Pay()
        {
            var payment = new DMeServices.Models.Common.PaymentCom();
            await payment.GetListAsync();

            return View();
        }

        public ActionResult PayWithPost()
        {
            var result = DMeServices.Models.Common.PaymentCom.PayAmount();
            return Redirect(result);
        }

        public ActionResult NewPermits()
        {
            PermitsViewModel oModel = new PermitsViewModel();

            // to save the attachments on the memory
            TempData["Attachments"] = new List<PermitsAttachments>();
            ViewBag.DDAttachmentsType = DDAttachmentTypes();
            ViewBag.DDWelayat = DDWelayat();
            ViewBag.DDBuildingTypes = DDBuildingTypes();
            ViewBag.DDLandUseTypes = DdLandUseTypes();
            ViewBag.DDSquareLetters = DdSquareLetters();
            return View(oModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNewPermits(PermitsViewModel oModel)
        {

                oModel.ListOfAttachments = (List<PermitsAttachments>)TempData["Attachments"];
                TempData["Attachments"] = null;
                if (isListOfAttachment(oModel.ListOfAttachments))
                {
                    oModel.PersonalCard.AttachmentTypeId = 1;
                    oModel.KrokeFile.AttachmentTypeId = 2;
                    oModel.OwnerFile.AttachmentTypeId = 3;
                    oModel.ListOfAttachments.Add(oModel.PersonalCard);
                    oModel.ListOfAttachments.Add(oModel.KrokeFile);
                    oModel.ListOfAttachments.Add(oModel.OwnerFile);
                    //oModel.BuildingPermits.LicenseNo = "ح / 5665";

                    oModel.ListOfAttachments = SaveFiles(oModel);
                    string result = PermitsCom.SavePermits(oModel);

                    ViewBag.TranseID = result;
                    DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.oUserInfo.MobileNo, ":تم تسليم طلبك بنجاح رقم المعاملة" + result);
                }
                return View("SaveNewPermitsSuccessPage");
           
        }

        private bool isListOfAttachment(List<PermitsAttachments> listOfAttachments)
        {
            if (listOfAttachments.Count == 0 || listOfAttachments.Count > 5) return false;

            foreach (var Attachment in listOfAttachments)
            {
                HttpPostedFileBase oFile = Attachment.File;
                FileInfo oFileInfo = new FileInfo(oFile.FileName);

                if (oFile == null || oFile.ContentLength <= 0 || !new string[] { ".jpg", ".jpeg", ".pdf", ".png" }.Contains(oFileInfo.Extension)) return false;
            }
            return true;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveConsultantPermits(PermitsViewModel oModel)
        {
            oModel.ListOfAttachments = (List<PermitsAttachments>)TempData["Attachments"];
            TempData["Attachments"] = null;
            oModel.ListOfAttachments = SaveConsultantFiles(oModel);
            string Result = PermitsCom.SaveConsultatPermits(oModel);

            ViewBag.TranseID = oModel.BuildingPermits.TransactNo;
            return View("SaveConsultatPermitsPage");
        }

        public ActionResult PermitDetails(int id = -99)
        {
            PermitsViewModel oModel = new PermitsViewModel
            {
                BuildingPermits = PermitsCom.PermitsByID(id)
            };
            TempData["Attachments"] = new List<PermitsAttachments>();
            ViewBag.DDAttachmentsType = DDAttachmentTypes();

            ViewBag.DDWelayat = DDWelayat();
            ViewBag.DDRegion = DDRegionSaved(oModel.BuildingPermits.WelayahID);
            //ViewBag.DDArea = DDAreaSaved(oModel.BuildingPermits.RegionID);
            ViewBag.DDBuildingTypes = DDBuildingTypes();
            ViewBag.DDLandUseTypes = DdLandUseTypes();
            ViewBag.DDSquareLetters = DdSquareLetters();
            oModel.ListOfAttachments = PermitsAttachmentsCom.AttachmentsByPermitsID(id);


            return View(oModel);
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
            PermitsAttachments Attachment = PermitsAttachmentsCom.AttachmentsByID(Id);

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

        #region Method :: Save Attachment
        [HttpPost]
        public ActionResult SaveAttachment()
        {
            PermitsViewModel oModel = new PermitsViewModel();
            var listofattachment = (List<PermitsAttachments>)TempData["Attachments"];
            if (listofattachment != null && listofattachment.Count == 5) return PartialView("_ListAttachments", oModel);
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                HttpPostedFileBase File = new HttpPostedFileWrapper(System.Web.HttpContext.Current.Request.Files["MyAttached"]);
                FileInfo oFileInfo = new FileInfo(File.FileName);
                var AttachmentTypeId = System.Web.HttpContext.Current.Request.Form["AttTypeId"];
                var FileDescription = System.Web.HttpContext.Current.Request.Form["FileDescription"];
                oModel.Attachments = new PermitsAttachments();
                oModel.Attachments.File = File;
                oModel.Attachments.InsertDate = DateTime.Now;
                oModel.Attachments.AttachmentContentType = oFileInfo.Extension;
                if (!new string[] { ".jpg", ".jpeg", ".pdf", ".png" }.Contains(oFileInfo.Extension))
                {
                    return PartialView("_ListAttachments", oModel);
                }
                if (string.IsNullOrEmpty(FileDescription))
                {
                    FileDescription = "لايوجد";
                }
                oModel.Attachments.Description = FileDescription;
                oModel.Attachments.AttachmentName = oFileInfo.Name;
                oModel.Attachments.AttachmentTypeId = int.Parse(AttachmentTypeId);
                oModel.ListOfAttachments = (List<PermitsAttachments>)TempData["Attachments"];
                if (oModel.ListOfAttachments == null)
                {
                    oModel.ListOfAttachments = new List<PermitsAttachments>();
                }
                if (oModel.ListOfAttachments.Count < 5)
                    oModel.ListOfAttachments.Add(oModel.Attachments);


                TempData["Attachments"] = oModel.ListOfAttachments;
            }
            else
            {
                ViewBag.message = " لا يمكن اضافة اكثر من 5 ملفات كحد اقصى";
            }
            return PartialView("_ListAttachments", oModel);
        }
        #endregion

        #region Method :: Save Consultant Attachment
        [HttpPost]
        public ActionResult SaveConsultantAttachment()
        {
            PermitsViewModel oModel = new PermitsViewModel();
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                HttpPostedFileBase File = new HttpPostedFileWrapper(System.Web.HttpContext.Current.Request.Files["MyAttached"]);
                FileInfo oFileInfo = new FileInfo(File.FileName);
                var AttachmentTypeId = System.Web.HttpContext.Current.Request.Form["AttTypeId"];
                var PrimID = System.Web.HttpContext.Current.Request.Form["PrimID"];
                var FileDescription = System.Web.HttpContext.Current.Request.Form["FileDescription"];
                oModel.Attachments = new PermitsAttachments();
                oModel.Attachments.File = File;
                oModel.Attachments.InsertDate = DateTime.Now;
                oModel.Attachments.AttachmentContentType = oFileInfo.Extension;
                if (string.IsNullOrEmpty(FileDescription))
                {
                    FileDescription = "لايوجد";
                }
                oModel.Attachments.Description = FileDescription;
                oModel.Attachments.AttachmentName = oFileInfo.Name;
                oModel.Attachments.AttachmentTypeId = int.Parse(AttachmentTypeId);

                oModel.ListOfAttachments = (List<PermitsAttachments>)TempData["Attachments"];

                if (oModel.ListOfAttachments.Count == 0)
                {
                    oModel.ListOfAttachments = PermitsAttachmentsCom.MapsAttachByPermitsID(int.Parse(PrimID));
                }
                oModel.ListOfAttachments.Add(oModel.Attachments);
                oModel.BuildingPermits = PermitsCom.PermitsByID(int.Parse(PrimID));
                TempData["Attachments"] = oModel.ListOfAttachments;
            }


            return PartialView("_ConsultantAttachments", oModel);

        }
        #endregion

        #region Method :: Delete Attachment

        public ActionResult DeleteAttachment(int Id)
        {
            PermitsViewModel oModel = new PermitsViewModel();
            oModel.ListOfAttachments = (List<PermitsAttachments>)TempData["Attachments"];
            oModel.Attachments = oModel.ListOfAttachments[Id];
            oModel.ListOfAttachments.Remove(oModel.Attachments);
            TempData["Attachments"] = oModel.ListOfAttachments;
            return PartialView("_ListAttachments", oModel);

        }
        #endregion

        #region Method :: Delete Consultant Attachment

        public ActionResult DeleteConsultantAttachment(int Id)
        {
            PermitsViewModel oModel = new PermitsViewModel();
            oModel.ListOfAttachments = (List<PermitsAttachments>)TempData["Attachments"];
            oModel.Attachments = oModel.ListOfAttachments[Id];
            oModel.ListOfAttachments.Remove(oModel.Attachments);
            TempData["Attachments"] = oModel.ListOfAttachments;
            return PartialView("_ConsultantAttachments", oModel);

        }
        #endregion

        #region Method :: DD Attachment Types

        public static List<SelectListItem> DDAttachmentTypes()
        {
            List<SelectListItem> LstAttachmentTypes = new List<SelectListItem>();
            List<AttachmentTypes> AllAttachmentTypes = AttachmentTypesCom.AllStructuredTypes();
            if (AllAttachmentTypes.Count > 0)
            {
                LstAttachmentTypes.Add(new SelectListItem() { Text = "أختر نوع الملف ", Value = "0" });
                foreach (var item in AllAttachmentTypes)
                {
                    LstAttachmentTypes.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
                }

            }
            return LstAttachmentTypes;
        }

        #endregion

        #region Method :: Save Files


        public static List<PermitsAttachments> SaveFiles(PermitsViewModel oModel)
        {
            List<PermitsAttachments> ListAttachments = new List<PermitsAttachments>();

            string sFilename = string.Empty;
            string PerPath;
            string StrPath;
            string sPath;

            if (oModel.ListOfAttachments == null)
            {
                return null;
            }

            foreach (var Attachment in oModel.ListOfAttachments)
            {

                HttpPostedFileBase oFile = Attachment.File;
                FileInfo oFileInfo = new FileInfo(oFile.FileName);

                if (oFile != null && oFile.ContentLength > 0)
                {
                    switch (Attachment.AttachmentTypeId)
                    {
                        case 1:
                        case 2:
                        case 3:
                            sFilename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + oFileInfo.Extension;
                            PerPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Files/AttachedFiles/Personal"));
                            sPath = System.IO.Path.Combine(PerPath.ToString());
                            string PerUploadPath = string.Format("{0}\\{1}", sPath, sFilename);

                            //oFile.SaveAs(PerUploadPath);
                            Attachment.AttachmentName = sFilename;
                            Attachment.AttachmentPath = PerUploadPath;
                            sFilename = null;

                            break;
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 14:
                            sFilename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + oFileInfo.Extension;
                            StrPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Files/AttachedFiles/StructuralFiles"));
                            sPath = System.IO.Path.Combine(StrPath.ToString());
                            string StrUploadPath = string.Format("{0}\\{1}", sPath, sFilename);
                            // oFile.SaveAs(StrUploadPath);
                            Attachment.AttachmentPath = StrUploadPath;
                            Attachment.AttachmentName = sFilename;
                            sFilename = null;
                            break;

                    }
                    MemoryStream stream = new MemoryStream();
                    oFile.InputStream.CopyTo(stream);
                    byte[] data = stream.ToArray();
                    //Attachment.AttachmentName = oFile.FileName;

                    Attachment.AttachmentStream = data;
                    Attachment.AttachmentContentType = oFileInfo.Extension;
                    Attachment.InsertDate = DateTime.Now;
                    Attachment.CreatedBy = oModel.oUserInfo.FirstName;
                    Attachment.CreatedOn = DateTime.Now;
                    ListAttachments.Add(Attachment);
                }
            }
            return ListAttachments;

        }

        #endregion

        #region Method :: Save Files


        public static List<PermitsAttachments> SaveConsultantFiles(PermitsViewModel oModel)
        {
            List<PermitsAttachments> listAttachments = new List<PermitsAttachments>();

            if (oModel.ListOfAttachments == null)
            {
                return null;
            }

            foreach (var Attachment in oModel.ListOfAttachments)
            {
                if (Attachment.Id == 0)
                {
                    HttpPostedFileBase oFile = Attachment.File;
                    FileInfo oFileInfo = new FileInfo(oFile.FileName);

                    if (oFile != null && oFile.ContentLength > 0)
                    {
                        var sFilename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + oFileInfo.Extension;
                        var StrPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Files/AttachedFiles/StructuralFiles"));
                        var sPath = System.IO.Path.Combine(StrPath.ToString());
                        string StrUploadPath = string.Format("{0}\\{1}", sPath, sFilename);
                        // oFile.SaveAs(StrUploadPath);
                        Attachment.AttachmentPath = StrUploadPath;
                        Attachment.AttachmentName = sFilename;
                        sFilename = null;
                        MemoryStream stream = new MemoryStream();
                        oFile.InputStream.CopyTo(stream);
                        byte[] data = stream.ToArray();
                        //Attachment.AttachmentName = oFile.FileName;

                        Attachment.AttachmentStream = data;
                        Attachment.AttachmentContentType = oFileInfo.Extension;
                        Attachment.InsertDate = DateTime.Now;
                        Attachment.CreatedBy = oModel.oUserInfo.FirstName;
                        Attachment.CreatedOn = DateTime.Now;
                        listAttachments.Add(Attachment);
                    }
                }
                else
                {
                    listAttachments.Add(Attachment);
                }
            }
            return listAttachments;

        }

        #endregion

        #region Method :: List Engineer Attachment 

        public ActionResult SelectEngineerAttachments(int Id, int PirmID)
        {
            PermitsViewModel oModel = new PermitsViewModel();
            oModel.BuildingPermits = PermitsCom.PermitsByID(PirmID);
            oModel.ListOfAttachmentDetails = PermitsAttachmentsCom.AttachmentsDetailsByAttachmentsID(Id);
            return PartialView("_EngineerAttachments", oModel);
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

        #region Method :: DD Areas

        public JsonResult GetAreas(string id)
        {
            List<SelectListItem> states = new List<SelectListItem>();
            var stateList = this.DDAreas(Convert.ToInt32(id));
            var stateData = stateList.Select(m => new SelectListItem()
            {
                Text = m.RegionArName,
                Value = m.IndustrialSurveyID.ToString(),
            });
            return Json(stateData, JsonRequestBehavior.AllowGet);
        }



        public List<AreaSurvey> DDAreas(int RegionID)
        {
            //List<Regions> LstRegions = new List<Regions>();
            List<AreaSurvey> AllRegions = LandSurveyCom.AreaSurveyByRegionId(RegionID);

            return AllRegions;
        }

        public static List<SelectListItem> DDAreaSaved(int RegionID)
        {
            List<SelectListItem> LstRegions = new List<SelectListItem>();
            var AllRegions = LandSurveyCom.AreaSurveyByRegionId(RegionID).ToList();
            if (AllRegions.Count > 0)
            {
                LstRegions.Add(new SelectListItem() { Text = "أختر المنطقة / القرية", Value = "0" });
                foreach (var item in AllRegions)
                {
                    LstRegions.Add(new SelectListItem() { Text = item.RegionArName, Value = item.IndustrialSurveyID.ToString() });
                }

            }
            return LstRegions;
        }

        #endregion

        #region Method :: DD SquareLetters

        public static List<SelectListItem> DdSquareLetters()
        {
            List<SelectListItem> lstSquareLetters = new List<SelectListItem>();
            List<SquareLetters> allSquareLetters = SquareLettersCom.AllSquareLetters();
            if (allSquareLetters.Count > 0)
            {
                lstSquareLetters.Add(new SelectListItem() { Text = "أختر رقم رمز المربع", Value = "0" });
                foreach (var item in allSquareLetters)
                {
                    lstSquareLetters.Add(new SelectListItem() { Text = item.ArLetter, Value = item.ID.ToString() });
                }

            }
            return lstSquareLetters;
        }

        #endregion

        #region Method :: DD LandUseTypes

        public static List<SelectListItem> DdLandUseTypes()
        {
            List<SelectListItem> LstLandUseTypes = new List<SelectListItem>();
            List<LandUseTypes> AllLandUseTypes = LandUseTypesCom.AllLandUseTypes();
            if (AllLandUseTypes.Count > 0)
            {
                LstLandUseTypes.Add(new SelectListItem() { Text = "أختر نوع", Value = "0" });
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
                LstBuildingTypes.Add(new SelectListItem() { Text = "أختر نوع", Value = "0" });
                foreach (var item in AllBuildingTypes)
                {
                    LstBuildingTypes.Add(new SelectListItem() { Text = item.BuildingType, Value = item.ID.ToString() });
                }

            }
            return LstBuildingTypes;
        }

        #endregion

    }
}