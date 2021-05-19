using AutoMapper;
using DMeServices.DAL;
using DMeServices.Models.BuildingServices;
using DMeServices.Models.ViewModels.Permits;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;


namespace DMeServices.Models.Common.BuildingServices
{
    public class PermitsCom
    {

        #region Method :: AllBuildingPermits

        public static List<BuildingPermits> AllPermits()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldPermits> _BldPermits = db.BldPermits.Include(y => y.BldPermitsAttachments).OrderByDescending(x => x.RequestDate).ToList();
                List<BuildingPermits> _BuildingPermits = Mapper.Map<List<BldPermits>, List<BuildingPermits>>(_BldPermits);
                return _BuildingPermits;
            }

        }

        #endregion

        #region Method :: Permit By ID

        public static BuildingPermits PermitsByID(int Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldPermits _BldPermits = db.BldPermits.Where(x => x.Id == Id).Include(y => y.BldPermitsAttachments).SingleOrDefault();
                BuildingPermits _BuildingPermits = Mapper.Map<BldPermits, BuildingPermits>(_BldPermits);

                return _BuildingPermits;
            }

        }
        #endregion

        #region Method :: Permit By TransNo
        public static BuildingPermits PermitsByTransNo(string TransNo)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldPermits _BldPermits = db.BldPermits.Where(x => x.TransactNo == TransNo).Include(y => y.BldPermitsAttachments).SingleOrDefault();
                BuildingPermits _BuildingPermits = Mapper.Map<BldPermits, BuildingPermits>(_BldPermits);

                return _BuildingPermits;
            }

        }
        #endregion

        #region Method :: Permit By ConsultantNo


        public static List<BuildingPermits> PermitsByConsultantNo(long ConsultantNo)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldPermits> _BldPermits = db.BldPermits.Where(x => x.ConsultantCrNo == ConsultantNo).OrderByDescending(x => x.Id).Include(y => y.BldPermitsAttachments).OrderByDescending(x => x.Id).ToList();
                List<BuildingPermits> _BuildingPermits = Mapper.Map<List<BldPermits>, List<BuildingPermits>>(_BldPermits);
                return _BuildingPermits;
            }

        }
        #endregion

        #region Method :: Permit By LandOwner CivilID
        public static List<BuildingPermits> PermitsByLandOwnerCivilId(long landOwnerCivilId)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldPermits> bldPermits = db.BldPermits.Where(x => x.OwnerCivilId == landOwnerCivilId).OrderByDescending(x => x.RequestDate).ToList();
                List<BuildingPermits> buildingPermits = Mapper.Map<List<BldPermits>, List<BuildingPermits>>(bldPermits);
                return buildingPermits;
            }
        }
        #endregion

        #region Method :: Permit By CivilId
        public static List<BuildingPermits> PermitsByConsultantCivilId(long CivilId)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldPermits> _BldPermits = db.BldPermits.Where(x => x.ConsultantCivilId == CivilId).Include(y => y.BldPermitsAttachments).OrderByDescending(x => x.RequestDate).ToList();
                List<BuildingPermits> _BuildingPermits = Mapper.Map<List<BldPermits>, List<BuildingPermits>>(_BldPermits);
                return _BuildingPermits;
            }
        }
        #endregion

        #region Method :: Get All New Permits By CivilId


        public static List<BuildingPermits> GetAllNewPermitsByConsultantCivilId(long CivilId)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldPermits> _BldPermits = db.BldPermits.Where(x => x.ConsultantCivilId == CivilId && x.WorkflowStatus == 8).OrderByDescending(x => x.Id).Include(y => y.BldPermitsAttachments).OrderByDescending(x => x.Id).ToList();
                List<BuildingPermits> _BuildingPermits = Mapper.Map<List<BldPermits>, List<BuildingPermits>>(_BldPermits);
                return _BuildingPermits;
            }

        }

        public static string UpdateStatus(int permitID, int status)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldPermits _BldPermits = new BldPermits();
                try
                {

                    _BldPermits = db.BldPermits.Where(x => x.Id == permitID).SingleOrDefault();

                    if (_BldPermits == null)
                    {
                        return null;
                    }

                    _BldPermits.WorkflowStatus = status;

                    db.SaveChanges();
                    return "paid";
                }

                catch (Exception)
                {
                    return null;
                }

            }
        }
        #endregion

        #region Method :: Get All New Permits 


        public static List<BuildingPermits> GetAllPermitsByflowStatus(int FlowID)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldPermits> _BldPermits = db.BldPermits.Where(x => x.WorkflowStatus == FlowID).Include(y => y.BldPermitsAttachments).OrderByDescending(x => x.RequestDate).ToList();
                List<BuildingPermits> _BuildingPermits = Mapper.Map<List<BldPermits>, List<BuildingPermits>>(_BldPermits);
                return _BuildingPermits;
            }

        }
        #endregion

        #region Method :: Permit By Engineer Number


        public static List<BuildingPermits> PermitsByEngineerID(int? EngNum, int Status)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldPermits> _BldPermits;
                if (Status == 1)
                {
                    _BldPermits = db.BldPermits.Where(x => x.DmEngineerNo == EngNum).Include(y => y.BldPermitsAttachments).OrderByDescending(x => x.RequestDate).ToList();
                }
                else
                {
                    _BldPermits = db.BldPermits.Where(x => x.DmEngineerNo == EngNum && x.WorkflowStatus == Status).Include(y => y.BldPermitsAttachments).OrderByDescending(x => x.RequestDate).ToList();
                }
                List<BuildingPermits> _BuildingPermits = Mapper.Map<List<BldPermits>, List<BuildingPermits>>(_BldPermits);
                return _BuildingPermits;
            }

        }
        #endregion

        #region Method :: Save Permits 
        public static string SavePermits(PermitsViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldPermits _BldPermits = new BldPermits();
                try
                {
                    _BldPermits = db.BldPermits.Where(x => x.Id == oModel.BuildingPermits.Id || x.TransactNo == oModel.BuildingPermits.TransactNo).SingleOrDefault();

                    if (_BldPermits != null)
                    {
                        return null;
                    }
                    if (oModel.oUserInfo.ConsultantCrNo == null)
                    {
                        return null;
                    }
                    _BldPermits = Mapper.Map<BuildingPermits, BldPermits>(oModel.BuildingPermits);
                    string[] _bldP = { "12", "16", "21" };
                    if (_bldP.Contains(oModel.BuildingPermits.ServiceName))
                    {
                        _BldPermits.TransactNo = GenTransactNo();
                    }
                    else
                    {
                        _BldPermits.TransactNo = GenTransactSmallBLDNo();
                    }
                    //_BldPermits.ConsultantCrNo = (long)oModel.oUserInfo.ConsultantCrNo;
                    //_BldPermits.ConsultantCivilId = (long)oModel.oUserInfo.CivilId;
                    if (oModel.BuildingPermits.WelayahID != null)
                    {
                        _BldPermits.WelayahID = oModel.BuildingPermits.WelayahID;
                    }
                    else
                    {
                        _BldPermits.WelayahID = 12;
                    }
                    if (oModel.BuildingPermits.RegionID != null)
                    {
                        _BldPermits.RegionID = oModel.BuildingPermits.RegionID;
                    }
                    else
                    {
                        _BldPermits.RegionID = 1;
                    }
                    if (oModel.BuildingPermits.SquareLetterID != null)
                    {
                        _BldPermits.SquareLetterID = oModel.BuildingPermits.SquareLetterID;
                    }
                    else
                    {
                        _BldPermits.SquareLetterID = 27;
                    }
                    if (oModel.BuildingPermits.UseTypeID != null)
                    {
                        _BldPermits.UseTypeID = oModel.BuildingPermits.UseTypeID;
                    }
                    else
                    {
                        _BldPermits.UseTypeID = 106;
                    }
                    if (oModel.BuildingPermits.BuildingTypeID != null)
                    {
                        _BldPermits.BuildingTypeID = oModel.BuildingPermits.BuildingTypeID;
                    }
                    else
                    {
                        _BldPermits.BuildingTypeID = 5;
                    }
                    _BldPermits.CreatedBy = oModel.oUserInfo.FullName;
                    _BldPermits.CreatedOn = DateTime.Now;
                    _BldPermits.RequestDate = DateTime.Now;
                    _BldPermits.WorkflowStatus = 8;
                    db.BldPermits.Add(_BldPermits);
                    db.SaveChanges();

                    if (oModel.ListOfAttachments != null)
                    {
                        List<BldPermitsAttachments> LstAttachments = Mapper.Map<List<PermitsAttachments>, List<BldPermitsAttachments>>(oModel.ListOfAttachments);
                        foreach (var File in LstAttachments)
                        {
                            File.BldPermitId = _BldPermits.Id;
                            db.BldPermitsAttachments.Add(File);
                            db.SaveChanges();
                        }
                    }
                    if (oModel.ListOfOwners != null)
                    {
                        List<BldOwners> LstOwners = Mapper.Map<List<Owner>, List<BldOwners>>(oModel.ListOfOwners);
                        foreach (var owner in LstOwners)
                        {
                            owner.BldPermitId = _BldPermits.Id;
                            db.BldOwners.Add(owner);
                            db.SaveChanges();
                        }
                    }
                    return _BldPermits.TransactNo;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region Method :: Handle Appointments Requests
        public static string UpdateConsultatAppointmentRequests(PermitsViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldPermits _BldPermits = new BldPermits();
                try
                {
                    _BldPermits = db.BldPermits.Where(x => x.Id == oModel.BuildingPermits.Id).SingleOrDefault();
                    if (_BldPermits == null)
                    {
                        return null;
                    }
                    _BldPermits.AppointmentStatus = "1";
                    _BldPermits.UpdatedOn = DateTime.Now;
                    db.SaveChanges();
                    return "ok";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static string AcceptAppointmentRequests(ViewModels.Internal.Permits.PermitsViewModel oModel, string oDate)
        {
            DateTime date;

            date = DateTime.ParseExact(oDate, "dd/MM/yyyy HH:mm", new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
            using (eServicesEntities db = new eServicesEntities())
            {
                BldPermits _BldPermits = new BldPermits();
                try
                {
                    _BldPermits = db.BldPermits.Where(x => x.Id == oModel.BuildingPermits.Id).SingleOrDefault();
                    if (_BldPermits == null)
                    {
                        return null;
                    }
                    _BldPermits.AppointmentStatus = "2";
                    _BldPermits.AppointmentDate = date;
                    _BldPermits.UpdatedBy = oModel.oEmployeeInfo.NAME;
                    _BldPermits.UpdatedOn = DateTime.Now;
                    db.SaveChanges();
                    return "ok";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static string RefuseAppointmentRequests(ViewModels.Internal.Permits.PermitsViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldPermits _BldPermits = new BldPermits();
                try
                {
                    _BldPermits = db.BldPermits.Where(x => x.Id == oModel.BuildingPermits.Id).SingleOrDefault();
                    if (_BldPermits == null)
                    {
                        return null;
                    }
                    _BldPermits.AppointmentStatus = "0";
                    _BldPermits.UpdatedBy = oModel.oEmployeeInfo.NAME;
                    _BldPermits.UpdatedOn = DateTime.Now;
                    db.SaveChanges();
                    return "ok";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static string CancelAppointmentRequests(ViewModels.Internal.Permits.PermitsViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldPermits _BldPermits = new BldPermits();
                try
                {
                    _BldPermits = db.BldPermits.Where(x => x.Id == oModel.BuildingPermits.Id).SingleOrDefault();
                    if (_BldPermits == null)
                    {
                        return null;
                    }
                    _BldPermits.AppointmentStatus = "3";
                    _BldPermits.UpdatedBy = oModel.oEmployeeInfo.NAME;
                    _BldPermits.UpdatedOn = DateTime.Now;
                    db.SaveChanges();
                    return "ok";
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region Method :: Save Engineer Permits 
        public static string SaveEngineerPermits(ViewModels.Internal.Permits.PermitsViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldPermits _BldPermits = new BldPermits();
                try
                {
                    _BldPermits = db.BldPermits.Where(x => x.Id == oModel.BuildingPermits.Id).SingleOrDefault();
                    if (_BldPermits == null)
                    {
                        return null;
                    }
                    //_BldPermits = Mapper.Map<BuildingPermits, BldPermits>(oModel.BuildingPermits);
                    if (oModel.BuildingPermits.WorkflowStatus == null)
                    {
                        _BldPermits.WorkflowStatus = 60;
                    }
                    if (oModel.BuildingPermits.WorkflowStatus == 19)
                    {
                        string[] _bldP = { "12", "16", "21" };
                        if (_bldP.Contains(oModel.BuildingPermits.ServiceName))
                        {
                            _BldPermits.LicenseNo = GenLicenseNo();
                        }
                        else
                        {
                            if (_BldPermits.LicenseNo == null)
                            {
                                _BldPermits.LicenseNo = _BldPermits.MLicenseNo = GenLicenseNoSmallBLDNo();
                            }
                            else
                            {
                                _BldPermits.MLicenseNo = GenLicenseNoSmallBLDNo();
                            }
                        }
                        if (_BldPermits.DMLicenseComments == null)
                        {
                            _BldPermits.DMLicenseComments = _BldPermits.MLicenseStatement = oModel.BuildingPermits.DMLicenseComments;
                        }
                        else
                        {
                            _BldPermits.MLicenseStatement = oModel.BuildingPermits.DMLicenseComments;
                        }
                    }
                    _BldPermits.WorkflowStatus = oModel.BuildingPermits.WorkflowStatus;
                    _BldPermits.UpdatedBy = oModel.oEmployeeInfo.NAME;
                    _BldPermits.UpdatedOn = DateTime.Now;
                    _BldPermits.DmEngineerComments = oModel.BuildingPermits.DmEngineerComments;
                    _BldPermits.DMManagerNotes = oModel.BuildingPermits.DMManagerNotes;
                    _BldPermits.DMEngineerNotes = oModel.BuildingPermits.DMEngineerNotes;
                    db.SaveChanges();
                    return "ok";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }



        public static List<Owner> OwnersByPermitID(int id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldOwners> _BldOwners = db.BldOwners.Where(x => x.BldPermitId == id).OrderByDescending(x => x.Id).ToList();
                List<Owner> _Owners = Mapper.Map<List<BldOwners>, List<Owner>>(_BldOwners);
                return _Owners;
            }
        }




        #endregion

        #region Method :: Save Consultat Permits 
        public static string SaveConsultatPermits(PermitsViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldPermits _BldPermits = new BldPermits();
                try
                {
                    _BldPermits = db.BldPermits.Where(x => x.Id == oModel.BuildingPermits.Id).SingleOrDefault();
                    if (_BldPermits == null)
                    {
                        return null;
                    }
                    if (oModel.ListOfAttachments != null)
                    {
                        List<BldPermitsAttachments> LstAttachments = Mapper.Map<List<PermitsAttachments>, List<BldPermitsAttachments>>(oModel.ListOfAttachments);
                        foreach (var File in LstAttachments)
                        {
                            var Attachment = db.BldPermitsAttachments.Where(x => x.Id == File.Id).SingleOrDefault();
                            if (Attachment != null)
                            {
                                Attachment.UpdatedBy = oModel.oUserInfo.FirstName;
                                Attachment.UpdatedOn = DateTime.Now;
                                db.SaveChanges();
                            }
                            else
                            {
                                File.BldPermitId = _BldPermits.Id;
                                db.BldPermitsAttachments.Add(File);
                                db.SaveChanges();
                            }
                        }
                    }

                    //  _BldPermits = Mapper.Map<BuildingPermits, BldPermits>(oModel.BuildingPermits);
                    _BldPermits.WorkflowStatus = 10;
                    _BldPermits.UpdatedBy = oModel.oUserInfo.FirstName;
                    _BldPermits.UpdatedOn = DateTime.Now;
                    db.SaveChanges();
                    return "ok";
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        #endregion

        #region Method :: Assign Permit 
        public static BuildingPermits AssignPermits(BuildingPermits Permits)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldPermits _BldPermits = new BldPermits();
                BuildingPermits _BuildingPermits = new BuildingPermits();
                try
                {
                    _BldPermits = db.BldPermits.Where(x => x.Id == Permits.Id).SingleOrDefault();
                    if (_BldPermits == null)
                    {
                        return null;
                    }
                    _BldPermits.DmEngineerNo = Permits.DmEngineerNo;
                    _BldPermits.WorkflowStatus = 60;
                    db.SaveChanges();
                    _BuildingPermits = Mapper.Map<BldPermits, BuildingPermits>(_BldPermits);
                    return _BuildingPermits;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region Method :: Delete Permits 
        #endregion

        #region Method :: Generate Transaction Number
        private static string GenTransactSmallBLDNo()
        {
            string TransactNo = null;
            using (eServicesEntities db = new eServicesEntities())
            {

                string CurrentYear = DateTime.Now.Year.ToString();
                int LastTID;

                string lastTransactNo;
                string[] _bldP = { "13", "14", "15", "17", "18", "19", "20", "22" };
                BldPermits Bld = db.BldPermits.Where(x => _bldP.Contains(x.ServiceName)).OrderByDescending(x => x.Id).FirstOrDefault();


                if (Bld == null)
                {
                    lastTransactNo = "E_BLD-0/" + CurrentYear + "";
                }
                else
                {
                    lastTransactNo = Bld.TransactNo;
                }

                string SrvName = lastTransactNo.Substring(0, 6);
                lastTransactNo = lastTransactNo.Substring(6);
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
        public static string GenTransactNo()
        {
            string TransactNo = null;
            using (eServicesEntities db = new eServicesEntities())
            {

                string CurrentYear = DateTime.Now.Year.ToString();
                int LastTID;

                string lastTransactNo;
                string[] _bldP = { "12", "16", "21" };


                BldPermits Bld = db.BldPermits.Where(x => _bldP.Contains(x.ServiceName)).OrderByDescending(x => x.Id).FirstOrDefault();


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

        #region Method :: Generate License Number
        private static string GenLicenseNoSmallBLDNo()
        {
            string LicenseNo = "2021/M999";
            using (eServicesEntities db = new eServicesEntities())
            {
                string CurrentYear = DateTime.Now.Year.ToString();
                int LastTNo;
                string lastLicenseNo;
                string[] _bldP = { "13", "14", "15", "17", "18", "19", "20" };
                BldPermits Bld = db.BldPermits.Where(x => _bldP.Contains(x.ServiceName)).OrderByDescending(x => x.LicenseNo).FirstOrDefault();

                if (Bld.LicenseNo == null)
                {
                    lastLicenseNo = CurrentYear + "/" + "M000";
                }
                else
                {
                    lastLicenseNo = Bld.LicenseNo;
                }

                string PermitYear = lastLicenseNo.Substring(0, 4);
                //lastLicenseNo = lastLicenseNo.Substring(4);
                string[] Result = lastLicenseNo.Split(new[] { '/' }, 2);
                string ResultTNo = Result[1];
                string ResultYear = Result[0];
                if (ResultYear == CurrentYear)
                {
                    LastTNo = int.Parse(ResultTNo.Substring(1, 3));
                    LastTNo = LastTNo + 1;
                    LicenseNo = "" + PermitYear + "/M" + String.Format("{0:000}", LastTNo) + "";
                }
                else
                {
                    LastTNo = 1;
                    LicenseNo = "" + CurrentYear + "/M" + String.Format("{0:000}", LastTNo) + "";
                }

                return LicenseNo;
            }
        }
        public static string GenLicenseNo()
        {
            string LicenseNo = "2021/9999";
            using (eServicesEntities db = new eServicesEntities())
            {

                string CurrentYear = DateTime.Now.Year.ToString();
                int LastTNo;

                string lastLicenseNo;
                string[] _bldP = { "12", "16", "21" };
                BldPermits Bld = db.BldPermits.Where(x => _bldP.Contains(x.ServiceName)).OrderByDescending(x => x.LicenseNo).FirstOrDefault();

                if (Bld.LicenseNo == null)
                {
                    lastLicenseNo = CurrentYear + "/" + "00000";
                }
                else
                {
                    lastLicenseNo = Bld.LicenseNo;
                }


                string PermitYear = lastLicenseNo.Substring(0, 4);
                //lastLicenseNo = lastLicenseNo.Substring(4);
                string[] Result = lastLicenseNo.Split(new[] { '/' }, 2);
                string ResultTNo = Result[1];
                string ResultYear = Result[0];
                if (ResultYear == CurrentYear)
                {
                    LastTNo = int.Parse(ResultTNo);
                    LastTNo = LastTNo + 1;
                    LicenseNo = "" + PermitYear + "/" + String.Format("{0:00000}", LastTNo) + "";
                }
                else
                {
                    LastTNo = 1;
                    LicenseNo = "" + CurrentYear + "/" + String.Format("{0:00000}", LastTNo) + "";
                }

                return LicenseNo;
            }
        }



        #endregion

        #region Method :: Owners By Permit ID
        public static List<Owner> GetOwnersNames(int permitId)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldOwners> _BldOwners = db.BldOwners.Where(x => x.BldPermitId == permitId).OrderByDescending(x => x.Id).ToList();
                List<Owner> _PermitOwners = Mapper.Map<List<BldOwners>, List<Owner>>(_BldOwners);
                return _PermitOwners;
            }
        }

        public static string ServiceNameByID(int id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldPermits _BldPermits = db.BldPermits.Where(x => x.Id == id).SingleOrDefault();
                string _srvName = _BldPermits.ServiceName;

                return _srvName;
            }
        }
        #endregion



    }
}
