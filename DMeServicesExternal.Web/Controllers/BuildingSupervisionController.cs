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
using DMeServices.Models;
using DMeServices.Models.Common;
using Resources;

namespace DMeServicesExternal.Web.Controllers
{
    public class BuildingSupervisionController : BaseController
    {
        // GET: BuildingSupervision
        public ActionResult Index()
        {
            //var oModel = new SupervisionViewModel();
            //if (showadd == true)
            //    oModel.ListBuildingSupervision = SupervisionCom.SupervisionsByConsultantCivilId(oModel.oUserInfo.CivilId);
            //else
            //    oModel.ListBuildingSupervision = SupervisionCom.SupervisionsByLandOwnerCivilId(oModel.oUserInfo.CivilId);
            //oModel.ShowAdd = showadd;
            return View();
        }

        public ActionResult SaveRev()
        {
            string tFees = "f2cbdd5e-fb65-4c5a-a940-886b9355c00a";
            string tInsurance = "e906985c-acc4-4fd6-b1c0-ed326a19a99a";
            PaymentsCom.TestSaveRevenue(tInsurance);
            return View("Index");
        }
        public ActionResult OwnerIndex()
        {
            SupervisionViewModel oModel = new SupervisionViewModel();
            oModel.ListBuildingSupervision = SupervisionCom.SupervisionsByLandOwnerCivilId(oModel.oUserInfo.CivilId);
            oModel.ShowAdd = false;
            return View("Index", oModel);
        }
        public ActionResult ConsultantIndex()
        {
            SupervisionViewModel oModel = new SupervisionViewModel();
            oModel.ListBuildingSupervision = SupervisionCom.SupervisionsByConsultantCivilId(oModel.oUserInfo.CivilId);
            oModel.ShowAdd = true;
            return View("Index", oModel);
        }
        public ActionResult NewSupervision(bool showadd)
        {
            SupervisionViewModel oModel = new SupervisionViewModel();
            oModel.ShowAdd = showadd;
            if (Session["UserInfo"] != null)
            {
                User user = (User)Session["UserInfo"];
                user = UserCom.UserByCivilID(user.CivilId);
                if (user != null && showadd == true)
                {
                    oModel.oUserInfo.CompanyName = user.CompanyName;
                    oModel.oUserInfo.ConsultantCrNo = user.ConsultantCrNo;
                    oModel.oUserInfo.PhoneNo = user.PhoneNo;
                    oModel.oUserInfo.Email = user.Email;
                    oModel.oUserInfo.UserType = user.UserType;
                    oModel.oUserInfo.MobileNo = user.MobileNo;
                }
            }
            // to save the attachments on the memory
            TempData["Attachments"] = new List<PermitsAttachments>();
            ViewBag.DDAttachmentsType = DDAttachmentTypes();
            ViewBag.DDWelayat = DDWelayat();
            ViewBag.DDServiceType = DDServiceType(showadd);
            ViewBag.DDBuildingTypes = DDBuildingTypes();
            ViewBag.DDLandUseTypes = DdLandUseTypes();
            ViewBag.DDSquareLetters = DdSquareLetters();
            return View(oModel);
        }

        #region Method :: Services Type
        private dynamic DDServiceType(bool showadd)
        {
            var userType = showadd ? 1 : 2;

            List<SelectListItem> LstServices = new List<SelectListItem>();
            List<SupervisionServicesTypes> AllServices = SupervisionCom.AllServices(userType);
            if (AllServices.Count > 0)
            {
                LstServices.Add(new SelectListItem() { Text = "أختر نوع الخدمة ", Value = "0" });
                foreach (var item in AllServices)
                {
                    LstServices.Add(new SelectListItem() { Text = item.ServiceNameAR, Value = item.ID.ToString() });
                }
            }
            return LstServices;
        }
        private dynamic DDServiceType()
        {
            List<SelectListItem> LstServices = new List<SelectListItem>();
            List<SupervisionServicesTypes> AllServices = SupervisionCom.AllServices();
            if (AllServices.Count > 0)
            {
                LstServices.Add(new SelectListItem() { Text = "أختر نوع الخدمة ", Value = "0" });
                foreach (var item in AllServices)
                {
                    LstServices.Add(new SelectListItem() { Text = item.ServiceNameAR, Value = item.ID.ToString() });
                }
            }
            return LstServices;
        }
        #endregion

        #region Method :: Save Supervisions
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNewSupervision(SupervisionViewModel oModel)
        {
            oModel.ListOfAttachments = (List<PermitsAttachments>)TempData["Attachments"];
            TempData["Attachments"] = null;
            if (oModel.ShowAdd == true)
            {
                AddConsultantAttachmentsToListOfAttachments(oModel);
                oModel.BuildingSupervision.ConsultantCivilId = oModel.oUserInfo.CivilId;
            }
            else if (oModel.ShowAdd == false)
            {
                if (oModel.BuildingSupervision.ServiceTypeID == 23)
                    AddOwnerAttachmentsToListOfAttachments(oModel);
                oModel.BuildingSupervision.OwnerCivilId = oModel.oUserInfo.CivilId;
                oModel.oUserInfo.ConsultantCrNo = 0;
            }
            if (oModel.OtherAttachments != null && oModel.OtherAttachments.OptionalFiles != null && oModel.OtherAttachments.OptionalFiles[0] != null)
            {
                oModel.OtherAttachments.AttachmentTypeId = 49;
                oModel.ListOfAttachments.Add(oModel.OtherAttachments);
            }
            if (oModel.ListOfAttachments != null && oModel.ListOfAttachments.Count > 0)
            {
                oModel.ListOfAttachments = SaveFiles(oModel);
            }
            string result = SupervisionCom.SaveSupervision(oModel);
            if (oModel.BuildingSupervision.ServiceTypeID == 23)
                SupervisionCom.UpdateSupervisionAttachmentsForNonPermitID(result);
            if (oModel.BuildingSupervision.ServiceTypeID == 10)
            {
                SaveChangeConsultantTransaction(oModel);
            }
            ViewBag.TranseID = result;
            ViewBag.showadd = oModel.ShowAdd;
            SendResultSMS(oModel, result);
            return View("SaveSupervisionSuccessPage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveConsultantSupervision(SupervisionViewModel oModel)
        {
            oModel.ListOfAttachments = (List<PermitsAttachments>)TempData["Attachments"];
            TempData["Attachments"] = null;
            if (oModel.OtherAttachments != null && oModel.OtherAttachments.Files != null)
            {
                oModel.OtherAttachments.AttachmentTypeId = 49;
                oModel.ListOfAttachments.Add(oModel.OtherAttachments);
            }
            if (oModel.ListOfAttachments.Count > 0)
            {
                oModel.ListOfAttachments = SaveConsultantFiles(oModel);
            }
            string Result = SupervisionCom.SaveConsultantSupervisions(oModel);

            ViewBag.TranseID = oModel.BuildingSupervision.TransactNo;
            ViewBag.showadd = oModel.ShowAdd;
            return View("SaveSupervisionSuccessPage");
        }
        #endregion

        #region Method :: Saving Process
        private static void SendResultSMS(SupervisionViewModel oModel, string result)
        {
            List<int> _phoneNos = new List<int>();
            if (oModel.oUserInfo.MobileNo != 0)
                _phoneNos.Add(oModel.oUserInfo.MobileNo);
            if (oModel.ShowAdd == false)
            {
                _phoneNos = GetOwnersPhoneNos(oModel);
            }
            if (_phoneNos != null && _phoneNos.Count > 0)
            {
                for (var i = 0; i < _phoneNos.Count; i++)
                    DMeServices.Models.Common.SmsCom.SendSms("968" + _phoneNos[i], ": تم تسليم طلبك بنجاح رقم المعاملة  " + result);
            }
        }
        private static List<int> GetOwnersPhoneNos(SupervisionViewModel oModel)
        {
            List<int> _ownersPhoneNos = new List<int>();
            oModel.ListOfOwners = PermitsCom.OwnersByPermitID(oModel.BuildingPermits.Id);
            foreach (var Owner in oModel.ListOfOwners)
            {
                if (Owner.Phone != null)
                    _ownersPhoneNos.Add((int)Owner.Phone);
            }
            return _ownersPhoneNos;
        }
        private static string SaveChangeConsultantTransaction(SupervisionViewModel oModel)
        {
            try
            {
                oModel.SupervisionTransact = new Transactions();
                string saveTransactResult = SupervisionCom.SaveSupervisionTransact(oModel);
                return saveTransactResult;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private static void AddOwnerAttachmentsToListOfAttachments(SupervisionViewModel oModel)
        {
            int _defaultPermitID = SupervisionCom.GetBldPermitDefaultValue();
            if (oModel.PersonalCard != null && oModel.PersonalCard.File != null)
            {
                oModel.PersonalCard.BldPermitId = _defaultPermitID;
                oModel.PersonalCard.AttachmentTypeId = 1;
                oModel.ListOfAttachments.Add(oModel.PersonalCard);
            }
            if (oModel.KrokeFile != null && oModel.KrokeFile.File != null)
            {
                oModel.PersonalCard.BldPermitId = _defaultPermitID;
                oModel.KrokeFile.AttachmentTypeId = 2;
                oModel.ListOfAttachments.Add(oModel.KrokeFile);
            }
            if (oModel.OwnerFile != null && oModel.OwnerFile.File != null)
            {
                oModel.PersonalCard.BldPermitId = _defaultPermitID;
                oModel.OwnerFile.AttachmentTypeId = 3;
                oModel.ListOfAttachments.Add(oModel.OwnerFile);
            }
        }
        private static void AddConsultantAttachmentsToListOfAttachments(SupervisionViewModel oModel)
        {
            if (oModel.ContractorCRFile != null && oModel.ContractorCRFile.File != null)
            {
                oModel.ContractorCRFile.AttachmentTypeId = 41;
                oModel.ListOfAttachments.Add(oModel.ContractorCRFile);
            }
            if (oModel.ContractorOwnerPersonalCard != null && oModel.ContractorOwnerPersonalCard.File != null)
            {
                oModel.ContractorOwnerPersonalCard.AttachmentTypeId = 42;
                oModel.ListOfAttachments.Add(oModel.ContractorOwnerPersonalCard);
            }
            if (oModel.ForemanPersonalCard != null && oModel.ForemanPersonalCard.File != null)
            {
                oModel.ForemanPersonalCard.AttachmentTypeId = 43;
                oModel.ListOfAttachments.Add(oModel.ForemanPersonalCard);
            }
            if (oModel.SupervisionLetter != null && oModel.SupervisionLetter.File != null)
            {
                oModel.SupervisionLetter.AttachmentTypeId = 44;
                oModel.ListOfAttachments.Add(oModel.SupervisionLetter);
            }
            if (oModel.SupervisionAgreement != null && oModel.SupervisionAgreement.File != null)
            {
                oModel.SupervisionAgreement.AttachmentTypeId = 45;
                oModel.ListOfAttachments.Add(oModel.SupervisionAgreement);
            }
            if (oModel.ConstructionPermitApplication != null && oModel.ConstructionPermitApplication.File != null)
            {
                oModel.ConstructionPermitApplication.AttachmentTypeId = 46;
                oModel.ListOfAttachments.Add(oModel.ConstructionPermitApplication);
            }
            if (oModel.PlotMarksForm != null && oModel.PlotMarksForm.File != null)
            {
                oModel.PlotMarksForm.AttachmentTypeId = 47;
                oModel.ListOfAttachments.Add(oModel.PlotMarksForm);
            }
            if (oModel.ProjectBoardForm != null && oModel.ProjectBoardForm.File != null)
            {
                oModel.ProjectBoardForm.AttachmentTypeId = 48;
                oModel.ListOfAttachments.Add(oModel.ProjectBoardForm);
            }
            if (oModel.ChangeConsultantStatment != null && oModel.ChangeConsultantStatment.File != null)
            {
                oModel.ChangeConsultantStatment.AttachmentTypeId = 50;
                oModel.ListOfAttachments.Add(oModel.ChangeConsultantStatment);
            }
        }
        #endregion

        #region Method :: Appointments
        [HttpPost]
        public ActionResult MakeAppointment(string bldID)
        {
            SupervisionViewModel oModel = new SupervisionViewModel();
            oModel.BuildingSupervision = SupervisionCom.SupervisionsById(int.Parse(bldID));
            string Result = SupervisionCom.UpdateOwnerNotesRequests(oModel);
            return Json(new
            {
                msg = "تم تقديم الطلب بنجاح"
            });
        }
        private bool isListOfAttachment(List<PermitsAttachments> listOfAttachments)
        {
            if (listOfAttachments.Count == 0 || listOfAttachments.Count > 20) return false;
            foreach (var Attachment in listOfAttachments)
            {
                HttpPostedFileBase oFile = Attachment.File;
                FileInfo oFileInfo = new FileInfo(oFile.FileName);

                if (oFile == null || oFile.ContentLength <= 0 || !new string[] { ".jpg", ".jpeg", ".pdf", ".png" }.Contains(oFileInfo.Extension.ToLower())) return false;
            }
            return true;
        }
        #endregion

        #region Method :: Get Permit License Data for Supervision Start
        [HttpPost]
        public ActionResult GetLicenseData(string licenseNo, string userType, string serviceType)
        {
            SupervisionViewModel oModel = new SupervisionViewModel();
            if (userType == "True")
            {
                string _result = GetBuildingPermitResult(licenseNo, serviceType, oModel);
                if (_result != "done")
                {
                    return Json(new { msg = _result });
                }
            }
            else
            {
                oModel.BuildingPermits = SupervisionCom.SupervisionsByLicenseNo(licenseNo, oModel.oUserInfo.CivilId);
                if (oModel.BuildingPermits == null)
                {
                    return Json(new
                    {
                        msg = "عفوا .. لا يمكنك اضافة طلبات على هذا الترخيص .. حيث ان الرقم المدني المستخدم غير مطابق او رقم الترخيص غير صحيح او غير مسجل .. برجاء مراجعة قسم تراخيص البناء"
                    });
                }
            }
            TempData["Attachments"] = new List<PermitsAttachments>();
            ViewBag.DDAttachmentsType = DDAttachmentTypes();
            if (oModel.BuildingSupervision.LicenseNo != null)
                ViewBag.LicenseNo = oModel.BuildingSupervision.LicenseNo;
            ViewBag.DDWelayat = DDWelayat();
            ViewBag.DDRegion = DDRegionSaved(oModel.BuildingPermits.WelayahID);
            ViewBag.DDServiceType = DDServiceType();
            //ViewBag.DDArea = DDAreaSaved(oModel.BuildingPermits.RegionID);
            ViewBag.DDBuildingTypes = DDBuildingTypes();
            ViewBag.DDLandUseTypes = DdLandUseTypes();
            ViewBag.DDSquareLetters = DdSquareLetters();
            oModel.ListOfOwners = PermitsCom.OwnersByPermitID(oModel.BuildingPermits.Id);
            string folderName = dirName(oModel);
            oModel.ListOfAttachments = PermitsAttachmentsCom.AttachmentsByPermitsID(oModel.BuildingPermits.Id, folderName);
            return PartialView("_Details", oModel);
        }

        private string GetBuildingPermitResult(string licenseNo, string serviceType, SupervisionViewModel oModel)
        {
            oModel.BuildingPermits = null;
            string resultmessage = SupervisionCom.SupervisionsByLicenseNo(licenseNo, serviceType).ToString();
            int permitID;
            if (int.TryParse(resultmessage, out permitID))
            {
                FillBuildingSupervision(licenseNo, serviceType, oModel, permitID);
                return "done";
            }
            return resultmessage;
        }

        private static void FillBuildingSupervision(string licenseNo, string serviceType, SupervisionViewModel oModel, int permitID)
        {
            oModel.BuildingPermits = SupervisionCom.GetBuildingPermit(permitID);
            if (serviceType == "10")
            {
                var supervisionRelated = SupervisionCom.GetSelectedSupervisionRequest(licenseNo);
                oModel.BuildingSupervision.LicenseNo = supervisionRelated.LicenseNo;
                oModel.BuildingSupervision.ContractorCR_No = supervisionRelated.ContractorCR_No;
                oModel.BuildingSupervision.SupervisionRelatedID = supervisionRelated.ID;
                oModel.BuildingSupervision.PaymentID = PaymentCom.GetPaymentIDBySupervisionID(supervisionRelated.ID);
            }
        }
        #endregion

        #region Method :: Supervision Details
        public ActionResult SupervisionDetails(bool showadd, int id = -99)
        {
            //System.Threading.Thread.Sleep(10000);
            SupervisionViewModel oModel = new SupervisionViewModel();
            if (oModel.oUserInfo == null && Session["UserInfo"] != null)
                oModel.oUserInfo = (DMeServices.Models.User)Session["UserInfo"];
            oModel.BuildingSupervision = SupervisionCom.SupervisionsById(id);
            if (oModel.BuildingSupervision.ContractorCR_No != null)
            {
                oModel.Contractor = SupervisionCom.ContractorByCRNO(oModel.BuildingSupervision.ContractorCR_No);
            }
            oModel.ShowAdd = showadd;
            oModel.BuildingPermits = PermitsCom.PermitsByID(oModel.BuildingSupervision.BldPermitID);
            TempData["Attachments"] = new List<PermitsAttachments>();
            ViewBag.DDAttachmentsType = DDAttachmentTypes();

            ViewBag.DDWelayat = DDWelayat();
            ViewBag.DDRegion = DDRegionSaved(oModel.BuildingPermits.WelayahID);
            ViewBag.DDServiceType = DDServiceType();
            //ViewBag.DDArea = DDAreaSaved(oModel.BuildingPermits.RegionID);
            ViewBag.DDBuildingTypes = DDBuildingTypes();
            ViewBag.DDLandUseTypes = DdLandUseTypes();
            ViewBag.DDSquareLetters = DdSquareLetters();
            oModel.ListOfOwners = PermitsCom.OwnersByPermitID(oModel.BuildingSupervision.BldPermitID);
            string folderName = dirName(oModel);
            oModel.ListOfAttachments = PermitsAttachmentsCom.AttachmentsByPermitsID(oModel.BuildingSupervision.BldPermitID, folderName);
            if (oModel.BuildingSupervision.ServiceTypeID == 23)
            {
                ViewBag.DDRegion = DDRegionSaved(oModel.BuildingSupervision.WelayahID);
                oModel.ListOfAttachments = PermitsAttachmentsCom.AttachmentsBySupervisionID(oModel.BuildingSupervision.ID, folderName);
            }
            oModel.Payments = PaymentsCom.PaymentsBySupervisionID(oModel.BuildingSupervision.ID);
            oModel.PaymentDetailsList = PaymentsCom.MapsPaymentDetailsBySupervisionID(oModel.BuildingSupervision.ID);
            if (oModel.BuildingSupervision.ServiceTypeID == 6 || oModel.BuildingSupervision.ServiceTypeID == 9 || oModel.BuildingSupervision.ServiceTypeID == 10)
                oModel.TransactsList = SupervisionCom.SupervisionsTransactBySupervisionLicenseNo(oModel.BuildingSupervision.LicenseNo);
            else
                oModel.TransactsList = SupervisionCom.SupervisionsTransactBySupervisionId(oModel.BuildingSupervision.ID);
            return View(oModel);
        }
        #endregion

        #region Method :: Working With Attachments, Directory and Files 
        public ActionResult PdfPartial(int id)
        {
            ViewBag.Id = id;
            return PartialView("_ViewFileToDownload");
        }

        public async Task<ActionResult> ShowModalDocument(int id)
        {
            string filePath = "~/Files/" + id.ToString() + ".pdf";

            var contentDisposition = new System.Net.Mime.ContentDisposition
            {
                FileName = "test.pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
            await Task.FromResult(0);
            return File(filePath, System.Net.Mime.MediaTypeNames.Application.Pdf);
        }

        public ActionResult GetFile(int ID)
        {
            ViewBag.ID = ID;
            return PartialView("_ViewFile");
        }

        public ActionResult DisplayFiles(int Id = -99)
        {
            PermitsAttachments Attachment = PermitsAttachmentsCom.AttachmentsByID(Id);
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
                oModel.Attachments.AttachmentContentType = oFileInfo.Extension.ToLower();
                if (!new string[] { ".jpg", ".jpeg", ".pdf", ".png" }.Contains(oFileInfo.Extension.ToString()))
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
                oModel.Attachments.AttachmentContentType = oFileInfo.Extension.ToLower();
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
        public static List<PermitsAttachments> SaveFiles(SupervisionViewModel oModel)
        {
            List<PermitsAttachments> ListAttachments = new List<PermitsAttachments>();
            foreach (var Attachment in oModel.ListOfAttachments)
            {
                HttpPostedFileBase oFile = Attachment.File;
                if (Attachment.File == null && Attachment.OptionalFile != null)
                {
                    oFile = Attachment.OptionalFile;
                }
                FileInfo oFileInfo;
                string attachmentFilePath = "";
                string attachmentFileType = "";
                if (Attachment.File != null)
                {
                    oFileInfo = new FileInfo(oFile.FileName);
                    attachmentFileType = oFileInfo.Extension.ToLower();
                }
                string folderName = dirName(oModel);
                if (oModel.BuildingSupervision.ServiceTypeID == 23)
                    folderName = oModel.BuildingSupervision.KrokiNO;
                string userFirstName = oModel.oUserInfo.FirstName;
                if (Attachment.OptionalFiles != null && Attachment.OptionalFiles[0] != null)
                {
                    foreach (var file in Attachment.OptionalFiles)
                    {
                        oFileInfo = new FileInfo(file.FileName);
                        attachmentFileType = oFileInfo.Extension.ToLower();
                        attachmentFilePath = SaveAttachmentFileSystem(file, folderName, "SupervisionFiles");
                        AddToListOfAttachments(ListAttachments, Attachment, userFirstName, file.FileName.ToLower(), attachmentFilePath, attachmentFileType);
                    }
                }
                if (Attachment.Files != null && Attachment.Files[0] != null)
                {
                    foreach (var file in Attachment.Files)
                    {
                        oFileInfo = new FileInfo(file.FileName);
                        attachmentFileType = oFileInfo.Extension.ToLower();
                        attachmentFilePath = SaveAttachmentFileSystem(file, folderName, "SupervisionFiles");
                        AddToListOfAttachments(ListAttachments, Attachment, userFirstName, file.FileName.ToLower(), attachmentFilePath, attachmentFileType);
                    }
                }
                if (oFile != null && oFile.ContentLength > 0)
                {
                    switch (Attachment.AttachmentTypeId)
                    {
                        case 1:
                        case 2:
                        case 3:
                            attachmentFilePath = SaveAttachmentFileSystem(oFile, folderName, "Personal");
                            break;
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 14:
                            attachmentFilePath = SaveAttachmentFileSystem(oFile, folderName, "StructuralFiles");
                            break;
                        case 41:
                        case 42:
                        case 43:
                        case 44:
                        case 45:
                        case 46:
                        case 47:
                        case 48:
                        case 49:
                        case 50:
                            attachmentFilePath = SaveAttachmentFileSystem(oFile, folderName, "SupervisionFiles");
                            break;
                    }
                    AddToListOfAttachments(ListAttachments, Attachment, userFirstName, oFile.FileName.ToLower(), attachmentFilePath, attachmentFileType);
                }
            }
            return ListAttachments;
        }

        private static List<PermitsAttachments> AddToListOfAttachments(List<PermitsAttachments> ListAttachments, PermitsAttachments Attachment, string userFirstName, string attachmentName, string attachmentPath, string attachmentType)
        {
            Attachment.AttachmentPath = attachmentPath;
            Attachment.AttachmentName = attachmentName;
            Attachment.AttachmentContentType = attachmentType;
            Attachment.InsertDate = DateTime.Now;
            Attachment.CreatedBy = userFirstName;
            Attachment.CreatedOn = DateTime.Now;
            ListAttachments.Add(Attachment);
            return ListAttachments;
        }

        public static string SaveAttachmentFileSystem(HttpPostedFileBase oFile, string folderName, string directoryName)
        {
            FileInfo oFileInfo = new FileInfo(oFile.FileName);
            string sFilename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + oFileInfo.Extension.ToLower();
            string sPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Files/AttachedFiles/" + directoryName + "/" + folderName));
            string uploadPath = string.Format("{0}\\{1}", sPath, sFilename);
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
            oFile.SaveAs(uploadPath);
            return uploadPath;
        }

        public static List<PermitsAttachments> SaveConsultantFiles(SupervisionViewModel oModel)
        {
            List<PermitsAttachments> listAttachments = new List<PermitsAttachments>();
            if (oModel.ListOfAttachments == null) return null;
            foreach (var Attachment in oModel.ListOfAttachments)
            {
                if (Attachment.Id == 0)
                {
                    HttpPostedFileBase oFile = Attachment.File;
                    if (Attachment.Files != null)
                    {
                        for (int i = 0; i < Attachment.Files.Length; i++)
                        {
                            oFile = Attachment.Files[i];
                            SaveSelectedFile(oModel, listAttachments, Attachment, oFile);
                        }
                    }
                    else SaveSelectedFile(oModel, listAttachments, Attachment, oFile);
                }
                else listAttachments.Add(Attachment);
            }
            return listAttachments;
        }
        private static void SaveSelectedFile(SupervisionViewModel oModel, List<PermitsAttachments> listAttachments, PermitsAttachments Attachment, HttpPostedFileBase oFile)
        {
            FileInfo oFileInfo = new FileInfo(oFile.FileName);
            string folderName = dirName(oModel);
            if (oFile != null && oFile.ContentLength > 0)
            {
                string sFilename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + oFileInfo.Extension.ToLower();
                string SupPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Files/AttachedFiles/SupervisionFiles/" + folderName));
                string sPath = System.IO.Path.Combine(SupPath.ToString());
                string SupUploadPath = string.Format("{0}\\{1}", sPath, sFilename);
                if (!Directory.Exists(SupPath))
                {
                    Directory.CreateDirectory(SupPath);
                }
                oFile.SaveAs(SupUploadPath);
                Attachment.AttachmentPath = SupUploadPath;
                Attachment.AttachmentName = sFilename;
                sFilename = null;
                Attachment.AttachmentName = oFile.FileName.ToLower();
                Attachment.AttachmentContentType = oFileInfo.Extension.ToLower();
                Attachment.InsertDate = DateTime.Now;
                Attachment.CreatedBy = oModel.oUserInfo.FirstName;
                Attachment.CreatedOn = DateTime.Now;
                listAttachments.Add(Attachment);
            }
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

        #region Method :: List Payment Details

        public ActionResult SelectPaymentDetails(int Id = -99)
        {
            SupervisionViewModel oModel = new SupervisionViewModel();
            oModel.PaymentDetailsList = DMeServices.Models.Common.BuildingServices.PaymentsCom.PaymentDetailsByPaymentID(Id);
            return PartialView("_ListPayments", oModel);
        }


        #endregion

        #region Method :: Payment

        public static async Task<DataTable> GetPaymentToken()
        {
            // Initialization.  
            DataTable responseObj = new DataTable();

            // HTTP GET.  
            using (var client = new HttpClient())
            {
                // Setting Base address from dhofar.gov.om  
                //client.BaseAddress = new Uri(DMeServices.Models.Resources.ApiUrls.PaymentAPIDhofar.ToString());

                // Setting Base address from main.edm.gov.om  
                client.BaseAddress = new Uri(ApiUrls.PaymentAPI.ToString());

                // Setting content type.  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Initialization.  
                HttpResponseMessage response = new HttpResponseMessage();

                // HTTP GET  
                //response = await client.GetAsync(DMeServices.Models.Resources.ApiUrls.PaymentAPIOpenRequestDhofar.ToString()).ConfigureAwait(false);
                response = await client.GetAsync(ApiUrls.PaymentAPIOpenRequest.ToString()).ConfigureAwait(false);

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
            await payment.GetListAsync(ApiUrls.PaymentAPI.ToString(), ApiUrls.PaymentAPIOpenRequest.ToString());

            return View();
        }

        public ActionResult PayWithPost(int id)
        {
            string requestUrl = Request.Url.AbsoluteUri.Remove(Request.Url.AbsoluteUri.Length - Request.Url.Segments[Request.Url.Segments.Length - 2].Length - Request.Url.Segments[Request.Url.Segments.Length - 1].Length);
            var _payment = DMeServices.Models.Common.BuildingServices.PaymentsCom.PaymentByID(id);
            string totalAmountToPay = _payment.PaymentTotalAmount.ToString();
            string amountType = _payment.PaymentType.ToString();
            var result = DMeServices.Models.Common.PaymentCom.PayAmount(totalAmountToPay, amountType, requestUrl, ApiUrls.PaymentAPIOpenRequest.ToString());

            var paymentID = DMeServices.Models.Common.BuildingServices.PaymentsCom.UpdatPaymentToken(id, result.token.ToString());

            if (paymentID != null)
            {
                return Redirect(result.url.ToString());
            }
            else
            {
                return HttpNotFound();
            }

        }

        public ActionResult PayingSucceeded(string token)
        {
            token = Request.QueryString["Token"].ToString();
            string T = token;
            DMeServices.Models.BankResponse _bankResponse = DMeServices.Models.Common.PaymentCom.GetBankResponse(T, ApiUrls.PaymentAPIGetRequestStatus.ToString());
            var permitID = DMeServices.Models.Common.BuildingServices.PaymentsCom.UpdatPaymentStatus(T, _bankResponse);
            int SupervisionID = PaymentsCom.PaymentIDByToken(T);
            int status = 29;

            var result = SupervisionCom.UpdatePaymentStatus(SupervisionID, status);
            if (result != null)
            {
                //PermitsViewModel pModel = new PermitsViewModel();
                //pModel.BuildingPermits = PermitsCom.PermitsByID(Convert.ToInt32(permitID));
                return RedirectToAction("SupervisionDetails", new { showadd = true, @id = Convert.ToInt32(result) });
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult PayingFailed()
        {


            return Redirect("www.google.com");


        }

        #endregion

        #region Method :: Change Contractor / Close Transaction

        public ActionResult SupervisionCloseTransaction(bool showadd, int Id = -99)
        {
            SupervisionViewModel oModel = new SupervisionViewModel();
            oModel.BuildingSupervision = SupervisionCom.SupervisionsById(Id);
            oModel.BuildingPermits = PermitsCom.PermitsByID(oModel.BuildingSupervision.BldPermitID);
            TempData["PaymentDetails"] = new List<PaymentDetails>();
            List<SelectListItem> serviceTypelist = DDServiceType(showadd);
            ViewBag.DDServiceType = serviceTypelist;
            if (showadd == true)
            {
                List<SelectListItem> _serviceTypelist = new List<SelectListItem>();
                foreach (SelectListItem item in serviceTypelist)
                {
                    if (item.Value == "0" || item.Value == "9" || item.Value == "6")
                    {
                        _serviceTypelist.Add(item);
                    }
                }
                ViewBag.DDServiceType = _serviceTypelist;
            }
            return View("SupervisionCloseTransactDetails", oModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSupervisionCloseTransaction(SupervisionViewModel oModel)
        {
            oModel.SupervisionTransact = new Transactions();

            string Result = SupervisionCom.SaveSupervisionTransact(oModel);
            //oModel.BuildingSupervision.Status = 8;
            oModel.BuildingSupervision.ID = 0;
            oModel.BuildingSupervision.TransactNo = null;
            //oModel.BuildingSupervision.PaymentID = int.Parse(Result);
            //SaveNewSupervision(oModel);
            //SupervisionCom.UpdatePaymentStatus(oModel.BuildingSupervision.ID, (int)oModel.BuildingSupervision.Status);
            //ViewBag.PaymentID = Result;
            //return RedirectToAction("SupervisionDetails", "BuildingSupervision", new { showadd = oModel.ShowAdd, Id = oModel.BuildingSupervision.ID }); return RedirectToAction("SupervisionDetails", "BuildingSupervision", new { showadd = oModel.ShowAdd, Id = oModel.BuildingSupervision.ID });
            return SaveNewSupervision(oModel);
        }

        #endregion
    }
}