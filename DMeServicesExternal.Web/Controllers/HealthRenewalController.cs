using DMeServices.Models.Common.HealthServices;
using DMeServices.Models.HealthServices;
using DMeServices.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMeServicesExternal.Web.Controllers
{
    public class HealthRenewalController : BaseController
    {
        // GET: HealthRenewal
        public ActionResult Index()
        {
            HealthRenewalViewModel oModel = new HealthRenewalViewModel();
            oModel.LstMociData = MociDataCom.MociDataByCivilID(oModel.oUserInfo.CivilId);
            ViewBag.Name = oModel.oUserInfo.FirstName + " " + oModel.oUserInfo.SecondName + " " + oModel.oUserInfo.ThirdName + " " + oModel.oUserInfo.LastName;
            ViewBag.Count_New = MociDataCom.MociDataByCivilID(oModel.oUserInfo.CivilId).Count();
            ViewBag.Count_Completed = 0;
            if (MociDataCom.MociDataByCivilID(oModel.oUserInfo.CivilId).Count() == 0)
            {
                ViewBag.Count_Completed = 1;
            }
            return View(oModel);
        }

        public ActionResult UserRenew(int ID)
        {
            HealthRenewalViewModel oModel1 = new HealthRenewalViewModel();
            oModel1.LstPermitData = PermitDataCom.PermitsBYMunicipalNo(ID, oModel1.oUserInfo.CivilId);
            oModel1.LstPermitAdvertisement = PermitAdvertisementCom.PermitAdvertisement(ID);

            return View(oModel1);
        }

        public ActionResult PermitsAdvertisement()
        {
            HealthRenewalViewModel oModel2 = new HealthRenewalViewModel();
            oModel2.LstPermitAdvertisement = PermitAdvertisementCom.PermitAdvertisement(10056);

            return View(oModel2);
        }

        [HttpPost]
        public ActionResult SaveNewPermitsDuration(HealthRenewalViewModel oModel)
        {
            oModel.ListOfAttachments = new List<ContractAttachments>();

            oModel.ListOfAttachments.Add(oModel.PersonalCard);
            oModel.ListOfAttachments.Add(oModel.ContractFile);
            oModel.ListOfAttachments.Add(oModel.OwnerContract);

            oModel.ListOfAttachments = SaveFiles(oModel);

            //string sFilename = string.Empty;
            
            //HttpPostedFileBase postedFile = oModel.PermitDuration.File;
            //FileInfo oFileInfo = new FileInfo(postedFile.FileName);
            //if (postedFile != null)
            //{
            //    sFilename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + oFileInfo.Extension;
                
            //    oModel.PermitDuration.AttachmentName = sFilename;
            //}

            //MemoryStream stream = new MemoryStream();
            //postedFile.InputStream.CopyTo(stream);
            //byte[] data = stream.ToArray();
            //oModel.PermitDuration.AttachmentContentType = oFileInfo.Extension;
            //oModel.PermitDuration.AttachmentStream = data;

            oModel.PermitDuration.WorkflowStatus = 11;
            oModel.PermitDuration.InsertDate = DateTime.Now;
            oModel.PermitDuration.RequestDate = DateTime.Now;

            oModel.PermitDuration.CivilId = int.Parse(oModel.oUserInfo.CivilId.ToString());
            int vResult = PermitDataCom.SavePermitsDuration(oModel);

            DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.oUserInfo.MobileNo, "تم تسليم طلبك بنجاح  . سيتم مراجعة البيانات واستكمال الإجراءات  ");

            return View("SaveNewPermitsSuccessPage");
        }

        #region Method :: Save Files


        public static List<ContractAttachments> SaveFiles(HealthRenewalViewModel oModel)
        {
            List<ContractAttachments> ListAttachments = new List<ContractAttachments>();

            string sFilename = string.Empty;

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
                    MemoryStream stream = new MemoryStream();
                    oFile.InputStream.CopyTo(stream);
                    byte[] data = stream.ToArray();
                    Attachment.AttachmentName = oFile.FileName;
                    Attachment.InsertDate = DateTime.Now;
                    Attachment.AttachmentStream = data;
                    Attachment.AttachmentContentType = oFileInfo.Extension;
                    ListAttachments.Add(Attachment);
                }
            }
            return ListAttachments;

        }

        #endregion

        public ActionResult GetUserData()
        {
            return View();
        }
        
        
    }
}