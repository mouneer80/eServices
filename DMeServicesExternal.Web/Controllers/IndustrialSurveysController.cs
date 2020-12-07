using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using DMeServices.Models.BuildingServices;
using DMeServices.Models.Common.BuildingServices;
using DMeServices.Models.ViewModels.Permits;
using DMeServices.Models.BuildingServices;
using System.IO;
//using DMeServicesExternal.Web.Data;

namespace DMeServicesExternal.Web.Controllers
{
    public class IndustrialSurveysController : BaseController
    {
        //private DMeServicesInternalWebContext db = new DMeServicesInternalWebContext();
        //SurveysViewModel oModel = new SurveysViewModel();

        // GET: IndustrialLandSurveys
        public ActionResult Index()
        {
            SurveysViewModel oModel = new SurveysViewModel();
            oModel.ListLandSurveys = LandSurveyCom.SurveysByConsultantCivilId(oModel.oUserInfo.CivilId);
            return View(oModel);
        }

        #region Method :: Create New
        
        //Open New
        public ActionResult NewCollectiveResidence()
        {
            SurveysViewModel oModel = new SurveysViewModel();
            //oModel.LandSurveys = LandSurveyCom.LandSurveyByID();
            //oModel.ListOfAttachments = PermitsAttachmentsCom.AttachmentsByPermitsID(81);


            ViewBag.DDWelayat = DDWelayat();
            ViewBag.DDBuildingTypes = DDBuildingTypes();
            ViewBag.DDLandUseTypes = DDLandUseTypes();
            ViewBag.DDSquareLetters = DDSquareLetters();
            //ViewBag.DDRegions = DDRegions();
            //ViewBag.DDPermitsStatus = DDPermitsStatus();
            return View(oModel);
        }
        //Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDetails(SurveysViewModel oModel)
        {
            oModel.ListOfWorkers = (List<WorkersHousingDetails>)TempData["Workers"];
            TempData["Workers"] = null;

            //oModel.ListOfWorkers.Add(oModel.Workers); 
            
            //oModel.ListOfWorkers = SaveWorkersDetails(oModel);
            string Result = LandSurveyCom.SaveLandSurvey(oModel);

            ViewBag.TranseID = Result;

            

            return RedirectToAction("Index");
        }
        
        #endregion


        #region Method :: Details
        
        public ActionResult CollectiveResidenceDetails(int Id = -99)
        {
            SurveysViewModel oModel = new SurveysViewModel();
            oModel.LandSurvey = LandSurveyCom.LandSurveyByID(Id);
            oModel.ListOfWorkers = LandSurveyCom.WorkersBySurveyID(Id);
            TempData["Workers"] = new List<WorkersHousingDetails>();
            ViewBag.DDWelayat = DDWelayat();
            ViewBag.DDRegion = DDRegionSaved(oModel.LandSurvey.WelyahID);
            ViewBag.DDArea = DDAreaSaved(oModel.LandSurvey.RegionID);
            ViewBag.DDBuildingTypes = DDBuildingTypes();
            ViewBag.DDLandUseTypes = DDLandUseTypes();
            ViewBag.DDSquareLetters = DDSquareLetters();
            return View("CollectiveResidenceDetails", oModel);
        }
        
        #endregion

        #region Method :: Edited Collective Residence Details

        //Land Survey
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDetails(SurveysViewModel oModel)
        {
            oModel.ListOfWorkers = (List<WorkersHousingDetails>)TempData["Workers"];
            TempData["Workers"] = null;

            string Result = LandSurveyCom.EditSurvey(oModel);

            ViewBag.TranseID = Result;

            return RedirectToAction("Index");
        }
        //end of Land

        #endregion

        #region Method :: DD Welayat

        public static List<SelectListItem> DDWelayat()
        {
            List<SelectListItem> LstWelayat = new List<SelectListItem>();
            List<Welyat> AllWelayat = WelayatCom.AllWelayat();
            if (AllWelayat.Count > 0)
            {
                LstWelayat.Add(new SelectListItem() { Text = "أختر المنطقة / القرية ", Value = "0" });
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

        #region Method :: Save Worker Details


        public static List<WorkersHousingDetails> SaveWorkersDetails(SurveysViewModel oModel)
        {
            List<WorkersHousingDetails> ListWorkers = new List<WorkersHousingDetails>();

            if (oModel.ListOfWorkers == null)
            {
                return null;
            }

            foreach (var Worker in oModel.ListOfWorkers)
            {

                Worker.SurveyID = oModel.LandSurvey.SurveyID;
                Worker.WorkerID = oModel.Workers.WorkerID;
                Worker.WorkerName = oModel.Workers.WorkerName;
                Worker.WorkerPhone = oModel.Workers.WorkerPhone;

                ListWorkers.Add(Worker);
            }
            return ListWorkers;

        }

        #endregion

        #region Method :: Save Worker
        [HttpPost]
        public ActionResult SaveWorker()
        {
            SurveysViewModel oModel = new SurveysViewModel();
            var WorkerID = System.Web.HttpContext.Current.Request.Form["WorkerID"];
            var WorkerName = System.Web.HttpContext.Current.Request.Form["WorkerName"];
            var WorkerPhone = System.Web.HttpContext.Current.Request.Form["WorkerPhone"];
            var SurveyID = System.Web.HttpContext.Current.Request.Form["SurveyID"];
            oModel.Workers = new WorkersHousingDetails();
            oModel.Workers.WorkerID = int.Parse(WorkerID);
            oModel.Workers.WorkerName = WorkerName;
            oModel.Workers.WorkerPhone = int.Parse(WorkerPhone);
            //oModel.Workers.SurveyID = int.Parse(SurveyID);
            oModel.ListOfWorkers = (List<WorkersHousingDetails>)TempData["Workers"];
            if (oModel.ListOfWorkers == null)
            {
                oModel.ListOfWorkers = new List<WorkersHousingDetails>();
            }
            if (oModel.ListOfWorkers.Count == 0)
            {
                if (SurveyID != null)
                { 
                    oModel.ListOfWorkers = LandSurveyCom.WorkersBySurveyID(int.Parse(SurveyID));
                }
            }
            oModel.ListOfWorkers.Add(oModel.Workers);
            TempData["Workers"] = oModel.ListOfWorkers;
            return PartialView("_ListWorkers", oModel);
        }
        #endregion

        #region Method :: Delete Worker

        public ActionResult DeleteWorker(int Id)
        {
            SurveysViewModel oModel = new SurveysViewModel();
            oModel.ListOfWorkers = (List<WorkersHousingDetails>)TempData["Workers"];
            oModel.Workers = oModel.ListOfWorkers[Id];
            oModel.ListOfWorkers.Remove(oModel.Workers);
            TempData["Workers"] = oModel.ListOfWorkers;
            return PartialView("_ListWorkers", oModel);

        }
        #endregion

    }
}
