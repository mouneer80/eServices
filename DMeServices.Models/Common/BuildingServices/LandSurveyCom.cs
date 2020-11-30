using AutoMapper;
using DMeServices.DAL;
using DMeServices.Models.BuildingServices;
using DMeServices.Models.ViewModels.Permits;
using DMeServices.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace DMeServices.Models.Common.BuildingServices
{
    public class LandSurveyCom
    {

        #region Method :: AllLandSurvey

        public static List<LandSurvey> AllLandSurvey()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldLandSurvey> _BldLandSurvey = db.BldLandSurvey.OrderByDescending(x => x.SurveyID).ToList();
                List<LandSurvey> _LandSurvey = Mapper.Map<List<BldLandSurvey>, List<LandSurvey>>(_BldLandSurvey);
                return _LandSurvey;
            }

        }

        #endregion


        #region Method :: Survey By ID

        public static LandSurvey LandSurveyByID(int Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldLandSurvey _BldLandSurvey = db.BldLandSurvey.Where(x => x.SurveyID == Id).SingleOrDefault();
                LandSurvey _LandSurvey = Mapper.Map<BldLandSurvey, LandSurvey>(_BldLandSurvey);

                return _LandSurvey;
            }

        }
        #endregion

        #region Method ::  Workers By SurveyID

        public static List<WorkersHousingDetails> WorkersBySurveyID(int Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldWorkersHousingDetails> _BldWorkersHousingDetails = db.BldWorkersHousingDetails.Where(x => x.SurveyID == Id).OrderByDescending(x => x.Id).ToList();
                List<WorkersHousingDetails> _WorkersHousingDetails = Mapper.Map<List<BldWorkersHousingDetails>, List<WorkersHousingDetails>>(_BldWorkersHousingDetails);
                

                return _WorkersHousingDetails;
            }

        }
        #endregion


        #region Method :: Survey By Welayah


        public static LandSurvey LandSurveyByWelayahID(int ID)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldLandSurvey _BldLandSurvey = db.BldLandSurvey.Where(x => x.WelyahID == ID).Include(y => y.SurveyID).SingleOrDefault();
                LandSurvey _LandSurvey = Mapper.Map<BldLandSurvey, LandSurvey>(_BldLandSurvey);

                return _LandSurvey;
            }

        }



        #endregion


        #region Method :: Survey By RegionId


        public static List<LandSurvey> LandSurveyByRegionId(int RegionId)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldLandSurvey> _BldLandSurvey = db.BldLandSurvey.Where(x => x.RegionID == RegionId).OrderByDescending(x => x.SurveyID).Include(y => y.BldRegions).OrderByDescending(x => x.RegionID).ToList();
                List<LandSurvey> _LandSurvey = Mapper.Map<List<BldLandSurvey>, List<LandSurvey>>(_BldLandSurvey);
                return _LandSurvey;
            }

        }
        #endregion



        #region Method :: Get All New LandSurvey By LandNO


        public static List<LandSurvey> GetAllNewLandSurveyByConsultantCivilId(string LandNO)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldLandSurvey> _BldLandSurvey = db.BldLandSurvey.Where(x => x.LandNo == LandNO).OrderByDescending(x => x.SurveyID).Include(y => y.SurveyID).OrderByDescending(x => x.SurveyID).ToList();
                List<LandSurvey> _LandSurvey = Mapper.Map<List<BldLandSurvey>, List<LandSurvey>>(_BldLandSurvey);
                return _LandSurvey;
            }

        }
        #endregion

        #region Method :: Survey By TransNo


        public static LandSurvey SurveysByTransNo(string TransNo)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldLandSurvey _BldSurveys = db.BldLandSurvey.Where(x => x.TransactNo == TransNo).Include(y => y.BldWelyat).SingleOrDefault();
                LandSurvey _LandSurveys = Mapper.Map<BldLandSurvey, LandSurvey>(_BldSurveys);

                return _LandSurveys;
            }

        }


        #endregion




        #region Method :: Save LandSurvey 


        public static string SaveLandSurvey(SurveysViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                
                BldLandSurvey _BldLandSurvey = new BldLandSurvey();
                try
                {

                    _BldLandSurvey = db.BldLandSurvey.Where(x => x.SurveyID == oModel.LandSurvey.SurveyID || x.TransactNo == oModel.LandSurvey.TransactNo).SingleOrDefault();

                    if (_BldLandSurvey != null)
                    {
                        return null;
                    }
                    //if (oModel.oUserInfo.ConsultantCrNo == null)
                    //{
                    //    return null;
                    //}



                    _BldLandSurvey = Mapper.Map<LandSurvey, BldLandSurvey>(oModel.LandSurvey);
                    _BldLandSurvey.TransactNo = GenTransactNo();
                    _BldLandSurvey.CreatedBy = oModel.LandSurvey.CreatedBy;
                    _BldLandSurvey.CreatedON = DateTime.Now.Date;
                    
                    //_BldLandSurvey.ConsultantCrNo = (int)oModel.oUserInfo.ConsultantCrNo;
                    //_BldLandSurvey.CreatedBy = oModel.oUserInfo.FirstName;
                    //_BldLandSurvey.CreatedOn = DateTime.Now.Date;
                    //_BldLandSurvey.RequestDate = DateTime.Now.Date;
                    //_BldLandSurvey.WorkflowStatus = 8;
                    db.BldLandSurvey.Add(_BldLandSurvey);
                    
                    db.SaveChanges();
                    if (oModel.ListOfWorkers != null)
                    {
                        List<BldWorkersHousingDetails> LstWorkers = Mapper.Map<List<WorkersHousingDetails>, List<BldWorkersHousingDetails>>(oModel.ListOfWorkers);
                        foreach (var Worker in LstWorkers)
                        {
                            Worker.SurveyID = _BldLandSurvey.SurveyID;
                            db.BldWorkersHousingDetails.Add(Worker);
                            db.SaveChanges();
                        }

                    }




                    return _BldLandSurvey.TransactNo;

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        #endregion

        #region Method :: Edit LandSurvey
        public static string EditSurvey(SurveysViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldLandSurvey _BldSurveys = new BldLandSurvey();
                try
                {
                    
                    _BldSurveys = db.BldLandSurvey.Where(x => x.SurveyID == oModel.LandSurvey.SurveyID).SingleOrDefault();

                    if (_BldSurveys == null)
                    {
                        return null;
                    }



                    //_BldSurveys = Mapper.Map<LandSurvey, BldLandSurvey>(oModel.LandSurvey);

                    //_BldSurveys.WorkflowStatus = oModel.BuildingPermits.WorkflowStatus;
                    //_BldSurveys.UpdatedBy = oModel.oEmployeeInfo.NAME;
                    //_BldSurveys.IndustrialSurveyID = oModel.LandSurvey.IndustrialSurveyID;
                    //_BldSurveys.CreatedBy = oModel.LandSurvey.CreatedBy;
                    //_BldSurveys.CreatedON = oModel.LandSurvey.CreatedON;
                    _BldSurveys.UpdatedBy = oModel.LandSurvey.UpdatedBy;
                    _BldSurveys.BuildingsCount = oModel.LandSurvey.BuildingsCount;
                    _BldSurveys.BuildingTypeID = oModel.LandSurvey.BuildingTypeID;
                    _BldSurveys.FlatsCount = oModel.LandSurvey.FlatsCount;
                    _BldSurveys.LandUseTypeID = oModel.LandSurvey.LandUseTypeID;
                    _BldSurveys.RegionID = oModel.LandSurvey.RegionID;
                    _BldSurveys.Notes = oModel.LandSurvey.Notes;
                    _BldSurveys.RoomsCount = oModel.LandSurvey.RoomsCount;
                    _BldSurveys.WelyahID = oModel.LandSurvey.WelyahID;
                    _BldSurveys.SquareLetterID = oModel.LandSurvey.SquareLetterID;
                    _BldSurveys.UpdatedON = DateTime.Now.Date;
                    //db.BldLandSurvey.Add(_BldSurveys);
                    //db.Entry(_BldSurveys).CurrentValues.SetValues(oModel);
                    db.SaveChanges();
                    if (oModel.ListOfWorkers != null)
                    {
                        List<BldWorkersHousingDetails> LstWorkers = Mapper.Map<List<WorkersHousingDetails>, List<BldWorkersHousingDetails>>(oModel.ListOfWorkers);
                        //List<BldWorkersHousingDetails> LstWorkers = new List<BldWorkersHousingDetails>(oModel.ListOfWorkers.Cast<BldWorkersHousingDetails>());
                        BldWorkersHousingDetails _BldWorker = new BldWorkersHousingDetails();
                        foreach (var Worker in LstWorkers)
                        {
                            _BldWorker = db.BldWorkersHousingDetails.Where(x => x.WorkerID == Worker.WorkerID && x.SurveyID == Worker.SurveyID).SingleOrDefault();

                            if (_BldWorker == null)
                            {
                                Worker.SurveyID = _BldSurveys.SurveyID;
                                db.BldWorkersHousingDetails.Add(Worker);
                                db.SaveChanges();
                            }
                            
                        }

                    }

                    return _BldSurveys.TransactNo;
                    //return "ok";
                }

                catch (Exception ex)
                {
                    return ex.Message;
                }

            }

        }
        #endregion

        #region Method :: Delete LandSurvey 
        #endregion

        #region Method :: AllAreaSurvey

        public static List<AreaSurvey> AllAreaSurvey()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldIndustrialAreasSurvey> _BldAreaSurvey = db.BldIndustrialAreasSurvey.OrderByDescending(x => x.IndustrialSurveyID).ToList();
                List<AreaSurvey> _AreaSurvey = Mapper.Map<List<BldIndustrialAreasSurvey>, List<AreaSurvey>>(_BldAreaSurvey);
                return _AreaSurvey;
            }

        }

        #endregion

        #region Method :: AreaSurvey By ID

        public static AreaSurvey AreaSurveyByID(int Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldIndustrialAreasSurvey _BldAreaSurvey = db.BldIndustrialAreasSurvey.Where(x => x.IndustrialSurveyID == Id).SingleOrDefault();
                AreaSurvey _AreaSurvey = Mapper.Map<BldIndustrialAreasSurvey, AreaSurvey>(_BldAreaSurvey);

                return _AreaSurvey;
            }

        }
        #endregion

        #region Method :: AreaSurvey By Welayah


        public static AreaSurvey AreaSurveyByWelayahID(int ID)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldIndustrialAreasSurvey _BldAreaSurvey = db.BldIndustrialAreasSurvey.Where(x => x.WelayahID == ID).Include(y => y.IndustrialSurveyID).SingleOrDefault();
                AreaSurvey _AreaSurvey = Mapper.Map<BldIndustrialAreasSurvey, AreaSurvey>(_BldAreaSurvey);

                return _AreaSurvey;
            }

        }



        #endregion


        #region Method :: AreaSurvey By RegionId


        public static List<AreaSurvey> AreaSurveyByRegionId(int RegionId)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldIndustrialAreasSurvey> _BldAreaSurvey = db.BldIndustrialAreasSurvey.Where(x => x.RegionID == RegionId).OrderByDescending(x => x.IndustrialSurveyID).Include(y => y.BldRegions).OrderByDescending(x => x.RegionID).ToList();
                List<AreaSurvey> _AreaSurvey = Mapper.Map<List<BldIndustrialAreasSurvey>, List<AreaSurvey>>(_BldAreaSurvey);
                return _AreaSurvey;
            }

        }
        #endregion




        

        #region Method :: Area Survey By TransNo


        public static AreaSurvey AreaSurveysByTransNo(string TransNo)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldIndustrialAreasSurvey _BldAreaSurveys = db.BldIndustrialAreasSurvey.Where(x => x.TransactNo == TransNo).Include(y => y.BldWelyat).SingleOrDefault();
                AreaSurvey _AreaSurveys = Mapper.Map<BldIndustrialAreasSurvey, AreaSurvey>(_BldAreaSurveys);

                return _AreaSurveys;
            }

        }


        #endregion




        #region Method :: Save AreaSurvey 


        public static string SaveAreaSurvey(AreaSurveysViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldIndustrialAreasSurvey _BldAreaSurvey = new BldIndustrialAreasSurvey();
                try
                {

                    _BldAreaSurvey = db.BldIndustrialAreasSurvey.Where(x => x.IndustrialSurveyID == oModel.AreaSurvey.IndustrialSurveyID || x.TransactNo == oModel.AreaSurvey.TransactNo).SingleOrDefault();

                    if (_BldAreaSurvey != null)
                    {
                        return null;
                    }
                    //if (oModel.oUserInfo.ConsultantCrNo == null)
                    //{
                    //    return null;
                    //}



                    _BldAreaSurvey = Mapper.Map<AreaSurvey, BldIndustrialAreasSurvey>(oModel.AreaSurvey);
                    _BldAreaSurvey.TransactNo = GenTransactNo();
                    _BldAreaSurvey.CreatedBy = oModel.AreaSurvey.CreatedBy;
                    _BldAreaSurvey.CreatedON = DateTime.Now.Date;
                    //_BldAreaSurvey.ConsultantCrNo = (int)oModel.oUserInfo.ConsultantCrNo;
                    //_BldAreaSurvey.CreatedBy = oModel.oUserInfo.FirstName;
                    //_BldAreaSurvey.CreatedOn = DateTime.Now.Date;
                    //_BldAreaSurvey.RequestDate = DateTime.Now.Date;
                    //_BldAreaSurvey.WorkflowStatus = 8;
                    db.BldIndustrialAreasSurvey.Add(_BldAreaSurvey);
                    db.SaveChanges();


                    return _BldAreaSurvey.TransactNo;

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        #endregion

        #region Method :: Edit AreaSurvey
        public static string EditAreaSurvey(AreaSurveysViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldIndustrialAreasSurvey _BldAreaSurveys = new BldIndustrialAreasSurvey();
                try
                {

                    _BldAreaSurveys = db.BldIndustrialAreasSurvey.Where(x => x.IndustrialSurveyID == oModel.AreaSurvey.IndustrialSurveyID).SingleOrDefault();

                    if (_BldAreaSurveys == null)
                    {
                        return null;
                    }



                    //_BldAreaSurveys = Mapper.Map<LandSurvey, BldLandSurvey>(oModel.LandSurvey);

                    //_BldAreaSurveys.WorkflowStatus = oModel.BuildingPermits.WorkflowStatus;
                    //_BldAreaSurveys.UpdatedBy = oModel.oEmployeeInfo.NAME;
                    _BldAreaSurveys.BuildingsCount = oModel.AreaSurvey.BuildingsCount;
                    _BldAreaSurveys.BuildingsTypeID = oModel.AreaSurvey.BuildingsTypeID;
                    _BldAreaSurveys.FlatsCount = oModel.AreaSurvey.FlatsCount;
                    _BldAreaSurveys.UseTypeID = oModel.AreaSurvey.UseTypeID;
                    _BldAreaSurveys.RegionID = oModel.AreaSurvey.RegionID;
                    _BldAreaSurveys.WelayahID = oModel.AreaSurvey.WelayahID;
                    _BldAreaSurveys.SquareLetterID = oModel.AreaSurvey.SquareLetterID;
                    _BldAreaSurveys.UpdatedON = DateTime.Now.Date;
                    _BldAreaSurveys.UpdatedBy = oModel.AreaSurvey.UpdatedBy;
                    _BldAreaSurveys.CommercialShopsCount = oModel.AreaSurvey.CommercialShopsCount;
                    _BldAreaSurveys.IndustrialShopsCount = oModel.AreaSurvey.IndustrialShopsCount;
                    _BldAreaSurveys.LocationCoordinates = oModel.AreaSurvey.LocationCoordinates;
                    _BldAreaSurveys.RegionArName = oModel.AreaSurvey.RegionArName;
                    _BldAreaSurveys.UseTypeID = oModel.AreaSurvey.UseTypeID;
                    _BldAreaSurveys.SquareLetterID = oModel.AreaSurvey.SquareLetterID;


                    //db.BldLandSurvey.Add(_BldAreaSurveys);
                    //db.Entry(_BldAreaSurveys).CurrentValues.SetValues(oModel);
                    db.SaveChanges();
                    return _BldAreaSurveys.TransactNo;
                    //return "ok";
                }

                catch (Exception ex)
                {
                    return ex.Message;
                }

            }

        }
        #endregion

        #region Method :: Delete AreaSurvey 
        #endregion

        #region Method :: Generate Transaction Number

        public static string GenTransactNo()
        {
            string TransactNo = null;
            using (eServicesEntities db = new eServicesEntities())
            {

                string CurrentYear = DateTime.Now.Year.ToString();
                int LastTID;

                string lastTransactNo;

                BldLandSurvey Bld = db.BldLandSurvey.OrderByDescending(x => x.SurveyID).FirstOrDefault();


                if (Bld == null)
                {
                    lastTransactNo = "BLD-0/" + CurrentYear + "";
                }
                else
                {
                    lastTransactNo = Bld.TransactNo;
                }


                string SrvName = lastTransactNo.Substring(0, 4);
                lastTransactNo = lastTransactNo.Substring(4);
                string[] Result = lastTransactNo.Split(new[] { '/' }, 2);
                string ResultTID = Result[0];
                string ResultYear = Result[1];
                if (ResultYear == CurrentYear)
                {
                    LastTID = int.Parse(ResultTID);
                    LastTID = LastTID + 1;
                    TransactNo = "" + SrvName + "" + LastTID + "/" + CurrentYear + "";
                }
                else
                {
                    LastTID = 1;
                    TransactNo = "" + SrvName + "" + LastTID + "/" + CurrentYear + "";
                }

                return TransactNo;
            }
        }

        #endregion


        #region Method :: Save LandSurvey 


        public static string SaveWorkersNewList(string TransactNo, SurveysViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldWorkersHousingDetails _BldWorkers = new BldWorkersHousingDetails();
                try
                {
                    BldLandSurvey _BldLandSurvey = new BldLandSurvey();

                    _BldLandSurvey = db.BldLandSurvey.Where(x => x.TransactNo == TransactNo).SingleOrDefault();


                    //if (oModel.oUserInfo.ConsultantCrNo == null)
                    //{
                    //    return null;
                    //}



                    _BldWorkers = Mapper.Map<WorkersHousingDetails, BldWorkersHousingDetails>(oModel.Workers);
                    _BldWorkers.SurveyID = _BldLandSurvey.SurveyID;
                    _BldWorkers.WorkerPhone = oModel.Workers.WorkerPhone;
                    _BldWorkers.WorkerName = oModel.Workers.WorkerName;
                    _BldWorkers.WorkerID = oModel.Workers.WorkerID;

                    //_BldLandSurvey.ConsultantCrNo = (int)oModel.oUserInfo.ConsultantCrNo;
                    //_BldLandSurvey.CreatedBy = oModel.oUserInfo.FirstName;
                    //_BldLandSurvey.CreatedOn = DateTime.Now.Date;
                    //_BldLandSurvey.RequestDate = DateTime.Now.Date;
                    //_BldLandSurvey.WorkflowStatus = 8;
                    db.BldWorkersHousingDetails.Add(_BldWorkers);
                    db.SaveChanges();




                    return _BldWorkers.WorkerName;

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        #endregion

        #region Method :: Surveys By CivilId


        public static List<LandSurvey> SurveysByConsultantCivilId(long CivilId)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldLandSurvey> _BldLandSurveys = db.BldLandSurvey.Where(x => x.ConsultantCivilId == CivilId).OrderByDescending(x => x.SurveyID).Include(y => y.BldWorkersHousingDetails).OrderByDescending(x => x.SurveyID).ToList();
                List<LandSurvey> _LandSurveys = Mapper.Map<List<BldLandSurvey>, List<LandSurvey>>(_BldLandSurveys);
                return _LandSurveys;
            }

        }
        #endregion





    }
}
