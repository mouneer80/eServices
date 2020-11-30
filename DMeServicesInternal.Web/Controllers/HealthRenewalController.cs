using DMeServices.Models;
using DMeServices.Models.Common.HealthServices;
using DMeServices.Models.HealthServices;
using DMeServices.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMeServicesInternal.Web.Controllers
{
    public class HealthRenewalController : BaseController
    {
        // GET: HealthRenewal
        public ActionResult Index()
        {
            HealthRenewalInternalViewModel oModel = new HealthRenewalInternalViewModel();
            oModel.LstPermitsHeadSection = PermitDataCom.PermitsHeadSection();

            return View(oModel);
        }

        public ActionResult AssignRenew(int ID)
        {
            HealthRenewalInternalViewModel oModel = new HealthRenewalInternalViewModel();
            oModel.LstPermitData = PermitDataCom.PermitDataHeadSection(ID);
            oModel.LstPermitAdvertisement = PermitAdvertisementCom.PermitAdvertisement(ID);
            ViewBag.DDInspectors = DDInspectors();
            return View(oModel);
        }

        public ActionResult InspectorRenew()
        {
            HealthRenewalInternalViewModel oModel = new HealthRenewalInternalViewModel();
            oModel.LstPermitsInspector = PermitDataCom.PermitsInspector((int)oModel.oEmployeeInfo.EMP_NO);

            return View(oModel);


        }

        public ActionResult InspectorReport(int ID)
        {
            HealthRenewalInternalViewModel oModel = new HealthRenewalInternalViewModel();
            oModel.LstPermitData = PermitDataCom.PermitDataHeadSection(ID);
            oModel.LstPermitAdvertisement = PermitAdvertisementCom.PermitAdvertisement(ID);
            ViewBag.DDInspectorStatus = DDInspectorStatus();
            return View(oModel);
        }

        #region Method :: DD Inspectors

        public static List<SelectListItem> DDInspectors()
        {
            List<SelectListItem> LstInspectors = new List<SelectListItem>();
            List<Employee> AllInspectors = PermitDataCom.AllInspectors();
            if (AllInspectors.Count > 0)
            {
                foreach (var item in AllInspectors)
                {
                    LstInspectors.Add(new SelectListItem() { Text = item.NAME, Value = item.EMP_NO.ToString() });
                }

            }
            return LstInspectors;
        }

        #endregion

        #region Method :: DD Inspector Status

        public static List<SelectListItem> DDInspectorStatus()
        {
            List<SelectListItem> LstInspectorStatus = new List<SelectListItem>();
            List<LookupType> AllStatus = PermitDataCom.InspectionStatus();
            if (AllStatus.Count > 0)
            {
                foreach (var item in AllStatus)
                {
                    LstInspectorStatus.Add(new SelectListItem() { Text = item.LookupNameAr, Value = item.LookupId.ToString() });
                }

            }
            return LstInspectorStatus;
        }

        #endregion

        [HttpPost]
        public ActionResult SaveInspectorFeedback(HealthRenewalInternalViewModel oModel)
        {
            oModel.PermitsInspection.InspectionDate = DateTime.Now;
            int vResult = PermitDataCom.SaveInspectorFeedback(oModel);

            return View("SaveNewPermitsSuccessPage");
        }

        public ActionResult AssignInspector(HealthRenewalInternalViewModel oModel)
        {
            int vResult = PermitDataCom.AssignInspector(oModel);

            return View("SaveNewPermitsSuccessPage");
        }

        public ActionResult RevenueData()
        {
            HealthRenewalInternalViewModel oModel = new HealthRenewalInternalViewModel();
            oModel.LstPermitsRevenue = PermitDataCom.PermitsRevenue();

            return View(oModel);
        }

        public ActionResult RevenueReport(int ID)
        {
            HealthRenewalInternalViewModel oModel = new HealthRenewalInternalViewModel();
            oModel.LstRevenuePermitData = PermitDataCom.PermitDataRevenue(ID);
            return View(oModel);
        }

        public ActionResult RevenueOperation(HealthRenewalInternalViewModel oModel)
        {
            int vResult = PermitDataCom.RevenueOperation(oModel);

            return View("SaveNewPermitsSuccessPage");
        }

        public ActionResult PermitFinish()
        {
            HealthRenewalInternalViewModel oModel = new HealthRenewalInternalViewModel();
            oModel.LstPermitsFinish = PermitDataCom.PermitsFinished();

            return View(oModel);
        }

        public ActionResult PermitFinishReport(int ID)
        {
            HealthRenewalInternalViewModel oModel = new HealthRenewalInternalViewModel();
            oModel.LstRevenuePermitData = PermitDataCom.PermitDataRevenue(ID);
            oModel.LstPermitAdvertisement = PermitAdvertisementCom.PermitAdvertisement(ID);
            return View(oModel);
        }

        public ActionResult GetFile(int Id, int Desc)
        {
            ViewBag.ID = Id;
            ViewBag.Type = Desc;

            return PartialView("_ViewHealthFiles");
        }

        //public ActionResult DisplayFiles(int Id)
        //{
        //    PermitDuration Attachment = DMeServices.Models.Common.HealthServices.PermitDataCom.AttachmentsByID(Id);

        //    if (Attachment != null)
        //    {
        //        Stream stream = new MemoryStream(Attachment.AttachmentStream);
        //        stream.Position = 0;

        //        if (Attachment.AttachmentName.EndsWith(".pdf"))
        //        {
        //            return new FileStreamResult(stream, "application/pdf");
        //        }
        //        else if (Attachment.AttachmentName.EndsWith(".jpeg"))
        //        {
        //            return new FileStreamResult(stream, "image/jpeg");
        //        }
        //        else if (Attachment.AttachmentName.EndsWith(".gif"))
        //        {
        //            return new FileStreamResult(stream, "image/gif");
        //        }
        //        else if (Attachment.AttachmentName.EndsWith(".png"))
        //        {
        //            return new FileStreamResult(stream, "image/png");
        //        }
        //        else if (Attachment.AttachmentName.EndsWith(".bmp"))
        //        {
        //            return new FileStreamResult(stream, "image/bmp");
        //        }
        //        else if (Attachment.AttachmentName.EndsWith(".webp"))
        //        {
        //            return new FileStreamResult(stream, "image/webp");
        //        }
        //    }
        //    return new EmptyResult();
        //}

        public ActionResult DisplayFiles(int Id, int Desc)
        {
            ContractAttachments Attachment = DMeServices.Models.Common.HealthServices.PermitDataCom.AttachmentsByIDType(Id, Desc);

            if (Attachment != null)
            {
                Stream stream = new MemoryStream(Attachment.AttachmentStream);
                stream.Position = 0;

                if (Attachment.AttachmentName.EndsWith(".pdf"))
                {
                    return new FileStreamResult(stream, "application/pdf");
                }
                else if (Attachment.AttachmentName.EndsWith(".jpeg"))
                {
                    return new FileStreamResult(stream, "image/jpeg");
                }
                else if (Attachment.AttachmentName.EndsWith(".jpg"))
                {
                    return new FileStreamResult(stream, "image/jpg");
                }
                else if (Attachment.AttachmentName.EndsWith(".gif"))
                {
                    return new FileStreamResult(stream, "image/gif");
                }
                else if (Attachment.AttachmentName.EndsWith(".png"))
                {
                    return new FileStreamResult(stream, "image/png");
                }
                else if (Attachment.AttachmentName.EndsWith(".bmp"))
                {
                    return new FileStreamResult(stream, "image/bmp");
                }
                else if (Attachment.AttachmentName.EndsWith(".webp"))
                {
                    return new FileStreamResult(stream, "image/webp");
                }
            }
            return new EmptyResult();
        }

        public ActionResult Test()
        {
            return View();
        }

        private decimal GetActivityValue(int ID)
        {
            HealthRenewalInternalViewModel oModel = new HealthRenewalInternalViewModel();
            oModel.ActivityValue = PermitDataCom.ActivityValue(ID);

            return (decimal)oModel.ActivityValue.Value;
        }

        [HttpPost]
        public JsonResult ActivityValue(int ID)
        {
            decimal Result = GetActivityValue(ID);

            return Json(Result, JsonRequestBehavior.AllowGet);
        }
    }
}