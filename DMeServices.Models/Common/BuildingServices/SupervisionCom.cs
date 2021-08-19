using AutoMapper;
using DMeServices.DAL;
using DMeServices.Models.BuildingServices;
using DMeServices.Models.ViewModels.Permits;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace DMeServices.Models.Common.BuildingServices
{
    public class SupervisionCom
    {

        #region Method :: AllBuildingSupervision

        public static List<BuildingSupervision> AllSupervisions()
        {
            using (var db = new eServicesEntities())
            {
                var bldSupervisionServices = db.BldSupervisionServices.Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(x => x.BldPermits.RequestDate).ToList();
                var BuildingSupervision = Mapper.Map<List<BldSupervisionServices>, List<BuildingSupervision>>(bldSupervisionServices);
                return BuildingSupervision;
            }

        }

        internal static List<BuildingSupervision> AllSupervisionsByWelayah(int dEPT_ID)
        {
            int welayahID = 0;
            switch (dEPT_ID)
            {
                case 15:
                    welayahID = 7;
                    break;
                case 16:
                    welayahID = 6;
                    break;
                case 17:
                    welayahID = 2;
                    break;
                case 18:
                    welayahID = 3;
                    break;
                case 19:
                    welayahID = 10;
                    break;
                case 20:
                    welayahID = 9;
                    break;
                case 21:
                    welayahID = 4;
                    break;
                case 22:
                    welayahID = 11;
                    break;
                case 23:
                    welayahID = 8;
                    break;
                case 35:
                    welayahID = 1;
                    break;
                case 37:
                    welayahID = 1;
                    break;
            }
            using (var db = new eServicesEntities())
            {
                var bldPermits = db.BldPermits.Where(y => y.WelayahID == welayahID).ToList();
                int[] regions = new int[] { 5, 42, 44, 45, 46, 47, 48, 49, 50, 51, 52, 54, 55, 56, 57, 58, 60, 61, 62, 63, 64, 65, 66, 67, 68, 70, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85 }; 
                if (dEPT_ID == 37 && welayahID == 1)
                    bldPermits = db.BldPermits.Where(y => y.WelayahID == welayahID && regions.Contains((int)y.RegionID)).ToList();
                else if (dEPT_ID == 35 && welayahID == 1)
                    bldPermits = db.BldPermits.Where(y => y.WelayahID == welayahID && regions.Contains((int)y.RegionID) == false).ToList();
                int[] bldPermitsIDs = new int[bldPermits.Count()];
                for (var i = 0; i < bldPermits.Count(); i++)
                {
                    bldPermitsIDs[i] = bldPermits[i].Id;
                }
                var bldSupervisionServices = db.BldSupervisionServices.Where(x => bldPermitsIDs.Contains(x.BldPermitID)).Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(x => x.BldPermits.RequestDate).ToList();
                var BuildingSupervision = Mapper.Map<List<BldSupervisionServices>, List<BuildingSupervision>>(bldSupervisionServices);
                return BuildingSupervision;
            }
        }

        #endregion

        #region Method :: Supervision By ID

        public static BuildingSupervision SupervisionsById(int id)
        {
            using (var db = new eServicesEntities())
            {

                var bldSupervisionServices = db.BldSupervisionServices.Where(x => x.ID == id).Include(y => y.BldPermits.BldPermitsAttachments).SingleOrDefault();
                var BuildingSupervision = Mapper.Map<BldSupervisionServices, BuildingSupervision>(bldSupervisionServices);

                return BuildingSupervision;
            }

        }
        #endregion

        #region Method :: Supervision By Permit ID
        public static BuildingSupervision SupervisionsByPermitId(int id)
        {
            using (var db = new eServicesEntities())
            {

                var bldSupervisionServices = db.BldSupervisionServices.Where(x => x.BldPermits.Id == id).Include(y => y.BldPermits.BldPermitsAttachments).SingleOrDefault();
                var BuildingSupervision = Mapper.Map<BldSupervisionServices, BuildingSupervision>(bldSupervisionServices);

                return BuildingSupervision;
            }

        }
        #endregion

        #region Method :: Supervision By TransNo


        public static BuildingSupervision SupervisionsByTransNo(string transNo)
        {
            using (var db = new eServicesEntities())
            {

                var bldSupervisionServices = db.BldSupervisionServices.Where(x => x.BldPermits.TransactNo == transNo).Include(y => y.BldPermits.BldPermitsAttachments).SingleOrDefault();
                var BuildingSupervision = Mapper.Map<BldSupervisionServices, BuildingSupervision>(bldSupervisionServices);

                return BuildingSupervision;
            }

        }
        #endregion

        #region Method :: Supervision By ConsultantNo
        public static List<BuildingSupervision> SupervisionsByConsultantNo(long consultantNo)
        {
            using (var db = new eServicesEntities())
            {
                var bldSupervisionServices = db.BldSupervisionServices.Where(x => x.BldPermits.ConsultantCrNo == consultantNo).OrderByDescending(x => x.BldPermits.Id).Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(x => x.BldPermits.Id).ToList();
                var BuildingSupervision = Mapper.Map<List<BldSupervisionServices>, List<BuildingSupervision>>(bldSupervisionServices);
                return BuildingSupervision;
            }

        }
        #endregion

        #region Method :: Supervision By LandOwner CivilID
        public static List<BuildingSupervision> SupervisionsByLandOwnerCivilId(int landOwnerCivilId)
        {
            using (var db = new eServicesEntities())
            {
                List<int> bldSupervisionsIDs = SupervisionsIDsByLandOwnerCivilId(landOwnerCivilId);
                List<BldSupervisionServices> bldSupervisionServices = db.BldSupervisionServices.Where(x => bldSupervisionsIDs.Contains(x.BldPermitID) || x.OwnerCivilId == landOwnerCivilId).OrderByDescending(x => x.BldPermitID).Include(y => y.BldPermits).OrderByDescending(y => y.BldPermitID).Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(y => y.BldPermitID).Include(y => y.BldSupervisionServicesTypes).ToList();
                var BuildingSupervision = Mapper.Map<List<BldSupervisionServices>, List<BuildingSupervision>>(bldSupervisionServices);
                return BuildingSupervision;
            }
        }

        #region Method :: Supervisions IDs By Owner CivilID
        public static List<int> SupervisionsIDsByLandOwnerCivilId(long landOwnerCivilId)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldOwners> _bldOwners = db.BldOwners.Where(x => x.CivilID == landOwnerCivilId).ToList();
                List<int> BLDIDs = new List<int>();
                foreach (var o in _bldOwners)
                {
                    BLDIDs.Add(o.BldPermitId.Value);
                }
                return BLDIDs;
            }
        }
        #endregion

        public static List<SupervisionServicesTypes> AllServices(int isConsaltant)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldSupervisionServicesTypes> _BldServicesTypes = db.BldSupervisionServicesTypes.Where(x => x.ServiceStatus == isConsaltant).ToList();
                List<SupervisionServicesTypes> _ServicesTypes = Mapper.Map<List<BldSupervisionServicesTypes>, List<SupervisionServicesTypes>>(_BldServicesTypes);

                return _ServicesTypes;
            }
        }

        public static List<SupervisionServicesTypes> AllServices()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldSupervisionServicesTypes> _BldServicesTypes = db.BldSupervisionServicesTypes.OrderBy(x => x.ID).ToList();
                List<SupervisionServicesTypes> _ServicesTypes = Mapper.Map<List<BldSupervisionServicesTypes>, List<SupervisionServicesTypes>>(_BldServicesTypes);
                return _ServicesTypes;
            }
        }
        public static List<SupervisionServicesTypes> AllPermitsServices()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldSupervisionServicesTypes> _BldServicesTypes = db.BldSupervisionServicesTypes.Where(x => x.ServiceStatus == 4).OrderBy(x => x.ID).ToList();
                List<SupervisionServicesTypes> _ServicesTypes = Mapper.Map<List<BldSupervisionServicesTypes>, List<SupervisionServicesTypes>>(_BldServicesTypes);
                return _ServicesTypes;
            }
        }

        public static void UpdateSupervisionAttachmentsForNonPermitID(string trans_no)
        {
            using (var db = new eServicesEntities())
            {
                int _permitID = GetBldPermitDefaultValue();
                int newBuildingSupervisionID = db.BldSupervisionServices.Where(x => x.TransactNo == trans_no).SingleOrDefault().ID;
                var lstAttachments = db.BldPermitsAttachments.Where(x => x.BldPermitId == _permitID && x.BldSupervisionID == null).ToList();
                foreach (var attach in lstAttachments)
                {
                    attach.BldSupervisionID = newBuildingSupervisionID;
                    db.SaveChanges();
                }
            }
        }

        public static string UpdateOwnerNotesRequests(SupervisionViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldSupervisionServices _BldSupervisions = new BldSupervisionServices();
                try
                {
                    _BldSupervisions = db.BldSupervisionServices.Where(x => x.ID == oModel.BuildingSupervision.ID).SingleOrDefault();
                    if (_BldSupervisions == null)
                    {
                        return null;
                    }
                    _BldSupervisions.OwnerNotes = "1";
                    _BldSupervisions.UpdatedOn = DateTime.Now;
                    db.SaveChanges();
                    return "ok";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static string RefuseOwnerNotesRequests(ViewModels.Internal.Permits.SupervisionViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldSupervisionServices _BldSupervisions = new BldSupervisionServices();
                try
                {
                    _BldSupervisions = db.BldSupervisionServices.Where(x => x.ID == oModel.BuildingSupervision.ID).SingleOrDefault();
                    if (_BldSupervisions == null)
                    {
                        return null;
                    }
                    _BldSupervisions.OwnerNotes = "0";
                    _BldSupervisions.UpdatedBy = oModel.oEmployeeInfo.NAME;
                    _BldSupervisions.UpdatedOn = DateTime.Now;
                    db.SaveChanges();
                    return "ok";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static string AcceptOwnerNotesRequests(ViewModels.Internal.Permits.SupervisionViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldSupervisionServices _BldSupervisions = new BldSupervisionServices();
                try
                {
                    _BldSupervisions = db.BldSupervisionServices.Where(x => x.ID == oModel.BuildingSupervision.ID).SingleOrDefault();
                    if (_BldSupervisions == null)
                        return null;
                    _BldSupervisions.OwnerNotes = "2";
                    _BldSupervisions.UpdatedBy = oModel.oEmployeeInfo.NAME;
                    _BldSupervisions.UpdatedOn = DateTime.Now;
                    db.SaveChanges();
                    return "ok";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static SupervisionServicesTypes ServiceByID(int id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldSupervisionServicesTypes _BldServicesTypes = db.BldSupervisionServicesTypes.Where(x => x.ID == id).SingleOrDefault();
                SupervisionServicesTypes _ServicesTypes = Mapper.Map<BldSupervisionServicesTypes, SupervisionServicesTypes>(_BldServicesTypes);
                return _ServicesTypes;
            }
        }
        #endregion

        #region Method :: Supervision By CivilId
        public static List<BuildingSupervision> SupervisionsByConsultantCivilId(int civilId)
        {
            using (var db = new eServicesEntities())
            {
                List<BldSupervisionServices> bldSupervisionServices = db.BldSupervisionServices.Where(x => x.ConsultantCivilId == civilId && x.BldSupervisionServicesTypes.ServiceStatus == 1).OrderByDescending(x => x.BldPermitID).Include(y => y.BldPermits).OrderByDescending(y => y.BldPermitID).Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(y => y.BldPermitID).Include(y => y.BldSupervisionServicesTypes).ToList();
                var BuildingSupervision = Mapper.Map<List<BldSupervisionServices>, List<BuildingSupervision>>(bldSupervisionServices);
                return BuildingSupervision;
            }
        }
        #endregion
        #region Method :: Supervision By LicenseNo
        public static string SupervisionsByLicenseNo(string licenseNo, string serviceType)
        {
            using (var db = new eServicesEntities())
            {
                try
                {
                    var _permit = db.BldPermits.Where(x => x.LicenseNo == licenseNo).OrderByDescending(x => x.UpdatedOn).FirstOrDefault();
                    if (_permit == null)
                    {
                        return "لا يوجد ترخيص لهذا الرقم";
                    }
                    else
                    {
                        int Stype = Convert.ToInt32(serviceType);
                        var bldSupervisionServices = new List<BldSupervisionServices>();
                        if (Stype == 5)
                            bldSupervisionServices = db.BldSupervisionServices.Where(x => x.BldPermitID == _permit.Id && x.ServiceTypeID == Stype).ToList();
                        if (Stype == 10)
                            bldSupervisionServices = db.BldSupervisionServices.Where(x => x.BldPermitID == _permit.Id && (x.ServiceTypeID == Stype || x.ServiceTypeID == 5)).ToList();
                        if (bldSupervisionServices.Count > 0)
                        {
                            foreach (var _supervision in bldSupervisionServices)
                            {
                                if (_supervision.Status != 67) return "يوجد طلب قيد المعاينة .. لا يمكن اضافة طلب جديد حتى يتم اغلاق الحالي";
                                var bldTransacts = db.BldTransactions.Where(x => x.BldSupervisionId == _supervision.ID).ToList();
                                if (bldTransacts.Count > 0)
                                {
                                    if (Stype == 5)
                                    {
                                        List<int> foundTypes = new List<int>();
                                        foreach (var transact in bldTransacts)
                                            foundTypes.Add((int)transact.TypeId);
                                        if (!foundTypes.Contains(6) && !foundTypes.Contains(9)) return "التصريح الحالي لم يتم اغلاقه بصورة صحيحة يجب اغلاقه قبل البدء بطلب تصريح جديد";
                                    }
                                    if (Stype == 10)
                                    {
                                        List<int> trueTypes = new List<int>();
                                        foreach (var transact in bldTransacts)
                                        {
                                            if (transact.TypeId == 6 || transact.TypeId == 9) return "هذا التصريح تم اغلاقه لا يمكن تقديم طلبات عليه يجب طلب تصريح جديد";
                                            if (transact.TypeId == 10 && transact.Status != 67)
                                                trueTypes.Add((int)transact.Id);
                                        }
                                        if (trueTypes.Count > 0) return "يوجد طلب قيد المعاينة ولا يمكن تقديم طلب اخر الا بعد الموافقة على الطلب الحالي";
                                    }
                                }
                                else
                                {
                                    if (Stype == 5)
                                    {
                                        return "التصريح الحالي لم يتم اغلاقه .. يجب اغلاقه قبل البدء بطلب تصريح جديد";
                                    }
                                }
                            }
                        }
                    }
                    //var bldPermits = db.BldPermits.Where(x => x.Id == _permitID.Id).Include(y => y.BldPermitsAttachments).SingleOrDefault();
                    //var BuildingPermits = Mapper.Map<BldPermits, BuildingPermits>(bldPermits);

                    return _permit.Id.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static BuildingPermits SupervisionsByLicenseNo(string licenseNo, int civilId)
        {
            using (var db = new eServicesEntities())
            {

                BldPermits _permit = db.BldPermits.Where(x => x.LicenseNo == licenseNo && x.ServiceName != "22").OrderByDescending(x => x.RequestDate).FirstOrDefault();
                if (_permit == null)
                {
                    return null;
                }
                BldOwners _owner = db.BldOwners.Where(x => x.BldPermitId == _permit.Id && x.CivilID == civilId).SingleOrDefault();
                if (_owner == null)
                {
                    return null;
                }
                var bldPermits = db.BldPermits.Where(x => x.Id == _permit.Id && _owner.CivilID != null).Include(y => y.BldPermitsAttachments).SingleOrDefault();
                var BuildingPermit = Mapper.Map<BldPermits, BuildingPermits>(bldPermits);
                return BuildingPermit;
            }
        }

        public static BuildingPermits GetBuildingPermit(int _permitID)
        {
            using (var db = new eServicesEntities())
            {
                try
                {
                    var bldPermits = db.BldPermits.Where(x => x.Id == _permitID).Include(y => y.BldPermitsAttachments).SingleOrDefault();
                    var BuildingPermits = Mapper.Map<BldPermits, BuildingPermits>(bldPermits);
                    return BuildingPermits;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static string GetSupervisionLicenseNo(string licenseNo)
        {
            using (var db = new eServicesEntities())
            {
                try
                {
                    var _permitID = db.BldPermits.Where(x => x.LicenseNo == licenseNo).OrderByDescending(x => x.UpdatedOn).FirstOrDefault();

                    var bldSupervisionServices = db.BldSupervisionServices.Where(x => x.BldPermitID == _permitID.Id && x.ServiceTypeID == 5 && x.Status == 67).ToList();
                    string result = "";
                    bool valid = true;
                    if (bldSupervisionServices.Count > 0)
                    {
                        foreach (var _supervision in bldSupervisionServices)
                        {
                            var bldTransacts = db.BldTransactions.Where(x => x.BldSupervisionId == _supervision.ID).ToList();
                            if (bldTransacts.Count > 0)
                            {
                                foreach (var transact in bldTransacts)
                                {
                                    if (transact.TypeId == 6 || transact.TypeId == 9)
                                        valid = false;
                                }
                            }
                            if (valid == true)
                                result = _supervision.LicenseNo;
                        }
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static BuildingSupervision GetSelectedSupervisionRequest(string licenseNo)
        {
            using (var db = new eServicesEntities())
            {
                try
                {
                    var _permitID = db.BldPermits.Where(x => x.LicenseNo == licenseNo).OrderByDescending(x => x.UpdatedOn).FirstOrDefault();

                    var bldSupervisionServices = db.BldSupervisionServices.Where(x => x.BldPermitID == _permitID.Id && x.ServiceTypeID == 5 && x.Status == 67).ToList();
                    BuildingSupervision result = new BuildingSupervision();
                    bool valid = true;
                    if (bldSupervisionServices.Count > 0)
                    {
                        foreach (var _supervision in bldSupervisionServices)
                        {
                            var bldTransacts = db.BldTransactions.Where(x => x.BldSupervisionId == _supervision.ID).ToList();
                            if (bldTransacts.Count > 0)
                            {
                                foreach (var transact in bldTransacts)
                                {
                                    if (transact.TypeId == 6 || transact.TypeId == 9)
                                        valid = false;
                                }
                            }
                            if (valid == true)
                                result = Mapper.Map<BldSupervisionServices, BuildingSupervision>(_supervision);
                        }
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region Method :: Get All New Supervisions By CivilId

        public static List<BuildingSupervision> GetAllNewSupervisionsByConsultantCivilId(long civilId)
        {
            using (var db = new eServicesEntities())
            {
                List<BldSupervisionServices> bldSupervisionServices = db.BldSupervisionServices.Where(x => x.BldPermits.ConsultantCivilId == civilId && x.BldPermits.WorkflowStatus == 8).OrderByDescending(x => x.BldPermits.Id).Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(x => x.BldPermits.Id).ToList();
                var BuildingSupervision = Mapper.Map<List<BldSupervisionServices>, List<BuildingSupervision>>(bldSupervisionServices);
                return BuildingSupervision;
            }

        }
        #endregion

        #region Method :: Get All New Supervisions 
        public static List<BuildingSupervision> GetAllSupervisionsByFlowStatus(int flowId)
        {
            using (var db = new eServicesEntities())
            {
                List<BldSupervisionServices> bldSupervisionServices = db.BldSupervisionServices.Where(x => x.Status == flowId).Include(y => y.BldPermits).Include(y => y.BldPermits.BldPermitsAttachments).Include(y => y.BldSupervisionServicesTypes).OrderByDescending(x => x.RequestDate).ToList();
                var BuildingSupervision = Mapper.Map<List<BldSupervisionServices>, List<BuildingSupervision>>(bldSupervisionServices);
                return BuildingSupervision;
            }

        }

        public static Contractor ContractorByCRNO(int? contractorCR_No)
        {
            using (var db = new eServicesEntities())
            {
                BldSupervisionContractors _bldSupervisionContractor = db.BldSupervisionContractors.Where(x => x.Cr_No == contractorCR_No).SingleOrDefault();
                var contractor = Mapper.Map<BldSupervisionContractors, Contractor>(_bldSupervisionContractor);
                return contractor;
            }
        }
        #endregion

        #region Method :: Supervisions By Engineer Number
        public static List<BuildingSupervision> SupervisionsByEngineerId(int? engNum)
        {
            using (var db = new eServicesEntities())
            {
                List<BldSupervisionServices> bldSupervisionServices = db.BldSupervisionServices.Where(x => x.DmEngineerNo == engNum).OrderByDescending(x => x.ID).Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(x => x.RequestDate).ToList();
                var BuildingSupervision = Mapper.Map<List<BldSupervisionServices>, List<BuildingSupervision>>(bldSupervisionServices);
                return BuildingSupervision;
            }
        }
        #endregion

        #region Method :: Permit By Inspector Number
        public static List<BuildingSupervision> SupervisionsByInspectorID(int? InspectorNum, int Status, bool IsManager)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldSupervisionServices> _bldSupervisionServices;
                if (IsManager)
                {
                    if (Status == 1)
                    {
                        _bldSupervisionServices = db.BldSupervisionServices.Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(x => x.BldPermits.RequestDate).ToList();
                    }
                    else
                    {
                        _bldSupervisionServices = db.BldSupervisionServices.Where(x => x.Status == Status).Include(y => y.BldPermits).Include(y => y.BldPermits.BldPermitsAttachments).Include(y => y.BldSupervisionServicesTypes).OrderByDescending(x => x.RequestDate).ToList();
                    }
                }
                else
                {
                    if (Status == 1)
                    {
                        _bldSupervisionServices = db.BldSupervisionServices.Where(x => x.DmEngineerNo == InspectorNum).Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(x => x.RequestDate).ToList();
                    }
                    else
                    {
                        _bldSupervisionServices = db.BldSupervisionServices.Where(x => x.DmEngineerNo == InspectorNum && x.Status == Status).Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(x => x.RequestDate).ToList();
                    }
                }
                List<BuildingSupervision> _BuildingSupervisionServices = Mapper.Map<List<BldSupervisionServices>, List<BuildingSupervision>>(_bldSupervisionServices);
                return _BuildingSupervisionServices;
            }

        }
        #endregion

        #region Method :: Save Supervision 
        public static string SaveSupervision(SupervisionViewModel oModel)
        {
            using (var db = new eServicesEntities())
            {
                var _BldSupervisionServices = new BldSupervisionServices();
                try
                {
                    _BldSupervisionServices = db.BldSupervisionServices.SingleOrDefault(x => x.ID == oModel.BuildingSupervision.ID || x.TransactNo == oModel.BuildingSupervision.TransactNo);
                    if (_BldSupervisionServices != null)
                        return null;
                    _BldSupervisionServices = Mapper.Map<BuildingSupervision, BldSupervisionServices>(oModel.BuildingSupervision);
                    _BldSupervisionServices.TransactNo = GenTransactNo();
                    if (oModel.BuildingPermits.Id != 0)
                        _BldSupervisionServices.BldPermitID = oModel.BuildingPermits.Id;
                    else
                        _BldSupervisionServices.BldPermitID = 10000;
                    _BldSupervisionServices.ServiceTypeID = oModel.BuildingSupervision.ServiceTypeID;
                    if (oModel.BuildingSupervision.LicenseNo != null)
                        _BldSupervisionServices.LicenseNo = oModel.BuildingSupervision.LicenseNo;
                    else
                        _BldSupervisionServices.LicenseNo = oModel.BuildingPermits.LicenseNo;
                    _BldSupervisionServices.CreatedBy = oModel.oUserInfo.FullName;
                    _BldSupervisionServices.CreatedOn = DateTime.Now;
                    _BldSupervisionServices.RequestDate = DateTime.Now;
                    _BldSupervisionServices.Status = 8;
                    if (oModel.BuildingPermits.KrokiNO != null)
                        _BldSupervisionServices.KrokiNO = oModel.BuildingPermits.KrokiNO;
                    if (oModel.BuildingSupervision.KrokiNO != null)
                        _BldSupervisionServices.KrokiNO = oModel.BuildingSupervision.KrokiNO;
                    if (oModel.oUserInfo.ConsultantCrNo != 0)
                        _BldSupervisionServices.ConsultantCivilId = oModel.oUserInfo.CivilId;
                    if (oModel.Contractor != null && oModel.Contractor.Cr_No != null)
                    {
                        SaveOrUpdateContractor(oModel);
                        _BldSupervisionServices.ContractorCR_No = oModel.Contractor.Cr_No;
                    }
                    if (oModel.ListOfAttachments != null && oModel.ListOfAttachments.Count > 0)
                        SaveAttachments(oModel);
                    db.BldSupervisionServices.Add(_BldSupervisionServices);
                    db.SaveChanges();
                    return _BldSupervisionServices.TransactNo;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private static void SaveAttachments(SupervisionViewModel oModel)
        {
            using (var db = new eServicesEntities())
            {
                int _permitID = oModel.BuildingSupervision.BldPermitID != 0 ? oModel.BuildingSupervision.BldPermitID : GetBldPermitDefaultValue();
                var lstAttachments = Mapper.Map<List<PermitsAttachments>, List<BldPermitsAttachments>>(oModel.ListOfAttachments);
                foreach (var file in lstAttachments)
                {
                    file.BldPermitId = _permitID;
                    db.BldPermitsAttachments.Add(file);
                    db.SaveChanges();
                }
            }
        }

        public static int GetBldPermitDefaultValue()
        {
            return 10000;
        }

        private static void SaveOrUpdateContractor(SupervisionViewModel oModel)
        {
            using (var db = new eServicesEntities())
            {
                var _BldSupervisionContractor = new BldSupervisionContractors();
                try
                {
                    _BldSupervisionContractor = db.BldSupervisionContractors.SingleOrDefault(x => x.Cr_No == oModel.Contractor.Cr_No);
                    if (_BldSupervisionContractor != null)
                    {
                        _BldSupervisionContractor.Foreman_Name = oModel.Contractor.Foreman_Name;
                        _BldSupervisionContractor.Foreman_Civil_ID = oModel.Contractor.Foreman_Civil_ID;
                        _BldSupervisionContractor.Email = oModel.Contractor.Email;
                    }
                    else
                    {
                        _BldSupervisionContractor = Mapper.Map<Contractor, BldSupervisionContractors>(oModel.Contractor);
                        _BldSupervisionContractor.Cr_No = oModel.Contractor.Cr_No;
                        _BldSupervisionContractor.Cr_Name = oModel.Contractor.Cr_Name;
                        _BldSupervisionContractor.Foreman_Name = oModel.Contractor.Foreman_Name;
                        _BldSupervisionContractor.Foreman_Civil_ID = oModel.Contractor.Foreman_Civil_ID;
                        _BldSupervisionContractor.OwnerFullName = oModel.Contractor.OwnerFullName;
                        _BldSupervisionContractor.Owner_Civil_ID = oModel.Contractor.Owner_Civil_ID;
                        _BldSupervisionContractor.PhoneNo = oModel.Contractor.PhoneNo;
                        _BldSupervisionContractor.Email = oModel.Contractor.Email;
                        _BldSupervisionContractor.LegalForm = oModel.Contractor.LegalForm;
                        db.BldSupervisionContractors.Add(_BldSupervisionContractor);
                    }
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static List<Transactions> SupervisionsTransactByTransactId(int id)
        {
            using (var db = new eServicesEntities())
            {
                var bldSupervisionTransact = db.BldTransactions.Where(x => x.Id == id).ToList();
                var BuildingSupervisionTransact = Mapper.Map<List<BldTransactions>, List<Transactions>>(bldSupervisionTransact);
                return BuildingSupervisionTransact;
            }
        }
        public static List<Transactions> SupervisionsTransactBySupervisionId(int SupervisionId)
        {
            using (var db = new eServicesEntities())
            {
                var bldSupervisionTransact = db.BldTransactions.Where(x => x.BldSupervisionId == SupervisionId).ToList();
                var BuildingSupervisionTransact = Mapper.Map<List<BldTransactions>, List<Transactions>>(bldSupervisionTransact);
                return BuildingSupervisionTransact;
            }
        }
        public static List<Transactions> RefundTransactsBySupervisionId(int SupervisionId)
        {
            using (var db = new eServicesEntities())
            {
                var bldSupervisionTransact = db.BldTransactions.Where(x => x.BldSupervisionId == SupervisionId && (x.TypeId == 6 || x.TypeId == 9)).ToList();
                var BuildingSupervisionTransact = Mapper.Map<List<BldTransactions>, List<Transactions>>(bldSupervisionTransact);
                return BuildingSupervisionTransact;
            }
        }

        public static string UpdateInspectorDetails(ViewModels.Internal.Permits.SupervisionViewModel oModel)
        {
            using (var db = new eServicesEntities())
            {
                try
                {
                    var oldSupervisionServices = db.BldSupervisionServices.Where(x => x.ID == oModel.BuildingSupervision.SupervisionRelatedID).SingleOrDefault();
                    var bldSupervisionServices = db.BldSupervisionServices.Where(x => x.ID == oModel.BuildingSupervision.ID).SingleOrDefault();
                    var paymentID = db.BldPayment.Where(x => x.SupervisionID == oModel.BuildingSupervision.SupervisionRelatedID && x.PaymentType == 3 && x.PaymentStatus == 1).OrderByDescending(x => x.PaymentDate).FirstOrDefault().PaymentID;
                    if (bldSupervisionServices == null)
                    {
                        return null;
                    }
                    bldSupervisionServices.LicenseNo = oldSupervisionServices.LicenseNo;
                    bldSupervisionServices.PaymentID = (int)paymentID;
                    bldSupervisionServices.Status = oModel.BuildingSupervision.Status;
                    bldSupervisionServices.ContractorCR_No = oldSupervisionServices.ContractorCR_No;
                    bldSupervisionServices.UpdatedBy = oModel.oEmployeeInfo.NAME;
                    bldSupervisionServices.UpdatedOn = DateTime.Now;
                    bldSupervisionServices.DmInspectorComments = oModel.BuildingSupervision.DmInspectorComments;
                    bldSupervisionServices.DmSupervisionComments = oModel.BuildingSupervision.DmSupervisionComments;
                    bldSupervisionServices.DMSupervisionStatment = oModel.BuildingSupervision.DMSupervisionStatment;
                    db.SaveChanges();
                    return "ok";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            }
        }

        public static List<Transactions> SupervisionsTransactBySupervisionLicenseNo(string licenseNo)
        {
            using (var db = new eServicesEntities())
            {

                var bldSupervisionTransact = db.BldTransactions.Where(x => x.BldSupervisionLicenseNo == licenseNo).ToList();
                var BuildingSupervisionTransact = Mapper.Map<List<BldTransactions>, List<Transactions>>(bldSupervisionTransact);

                return BuildingSupervisionTransact;
            }
        }
        #endregion

        #region Method :: Save Engineer Supervisions 
        public static string SaveInspectorDetails(ViewModels.Internal.Permits.SupervisionViewModel oModel)
        {
            using (var db = new eServicesEntities())
            {
                try
                {
                    var bldSupervisionServices = db.BldSupervisionServices.SingleOrDefault(x => x.ID == oModel.BuildingSupervision.ID);

                    if (bldSupervisionServices == null)
                    {
                        string ErrorMessage = "رقم الخدمة غير صحيح";
                        return ErrorMessage;
                    }
                    if (oModel.BuildingSupervision.Status == 66)
                    {
                        if (oModel.BuildingSupervision.LicenseNo == null || isTwoParts(oModel.BuildingSupervision.LicenseNo))
                            bldSupervisionServices.LicenseNo = GenLicenseNoForSupervision(oModel.BuildingSupervision.ServiceTypeID);
                    }

                    bldSupervisionServices.Status = oModel.BuildingSupervision.Status;
                    bldSupervisionServices.UpdatedBy = oModel.oEmployeeInfo.NAME;
                    bldSupervisionServices.UpdatedOn = DateTime.Now;
                    bldSupervisionServices.DmInspectorComments = oModel.BuildingSupervision.DmInspectorComments;
                    bldSupervisionServices.DmSupervisionComments = oModel.BuildingSupervision.DmSupervisionComments;
                    bldSupervisionServices.DMSupervisionStatment = oModel.BuildingSupervision.DMSupervisionStatment;
                    db.SaveChanges();
                    return "ok";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        private static bool isTwoParts(string licenseNo)
        {
            string[] Result = licenseNo.Split(new[] { '/' }, 3);
            if (Result.Count() != 2)
                return false;
            return true;
        }
        #endregion

        #region Method :: Generate License Number
        private static string GenLicenseNo(SupervisionServicesTypes service)
        {
            string LicenseNo = service.ServiceNameEn[0].ToString() + "/2021/99999";
            using (eServicesEntities db = new eServicesEntities())
            {

                string CurrentYear = DateTime.Now.Year.ToString();
                int LastTNo;

                string lastLicenseNo;

                //BldPermits Bld = db.BldPermits.OrderByDescending(x => x.LicenseNo).FirstOrDefault();
                BldSupervisionServices BldSupervisions = db.BldSupervisionServices.Where(x => x.ServiceTypeID == service.ID).OrderByDescending(x => x.LicenseNo).FirstOrDefault();

                //string[] isPermit = IsPermitLNo(BldSupervisions.LicenseNo);

                if (BldSupervisions == null)
                {
                    lastLicenseNo = service.ServiceNameEn[0].ToString() + "/" + CurrentYear + "/" + "00000";
                }
                else
                {
                    lastLicenseNo = BldSupervisions.LicenseNo;
                }
                //string PermitYear = lastLicenseNo.Substring(0, 4);
                //lastLicenseNo = lastLicenseNo.Substring(4);
                string[] Result = lastLicenseNo.Split(new[] { '/' }, 3);
                string ResultTNo;
                string ResultYear;
                string ResultName;
                if (Result.Count() == 2)
                {
                    ResultTNo = Result[1];
                    ResultYear = Result[0];
                }
                else
                {
                    ResultTNo = Result[2];
                    ResultYear = Result[1];
                    ResultName = Result[0];
                }
                if (ResultYear == CurrentYear)
                {
                    LastTNo = int.Parse(ResultTNo);
                    LastTNo = LastTNo + 1;
                    LicenseNo = "" + service.ServiceNameEn[0].ToString() + "/" + ResultYear + "/" + String.Format("{0:00000}", LastTNo) + "";
                }
                else
                {
                    LastTNo = 1;
                    LicenseNo = "" + service.ServiceNameEn[0].ToString() + "/" + CurrentYear + "/" + String.Format("{0:00000}", LastTNo) + "";
                }
                return LicenseNo;
            }
        }

        private static string GenLicenseNoForSupervision(int serviceTypeID)
        {
            SupervisionServicesTypes servicesType = ServiceByID(serviceTypeID);
            string LicenseNo = servicesType.ServiceNameEn.Split(' ')[0] + "/2021/99999";
            using (eServicesEntities db = new eServicesEntities())
            {
                string CurrentYear = DateTime.Now.Year.ToString();
                int LastTNo;
                string lastLicenseNo = servicesType.ServiceNameEn.Split(' ')[0].ToString()[0] + "/" + CurrentYear + "/" + "00000";
                List<BldSupervisionServices> BldSupervisions = db.BldSupervisionServices.Where(x => x.ServiceTypeID == serviceTypeID && x.LicenseNo != null).OrderByDescending(x => x.LicenseNo).ToList();
                if (BldSupervisions != null && BldSupervisions.Count > 0)
                {
                    List<BldSupervisionServices> validSupervisionLicenseNo = new List<BldSupervisionServices>();
                    for (int i = 0; i < BldSupervisions.Count; i++)
                    {
                        if (BldSupervisions[i].LicenseNo != null && !isTwoParts(BldSupervisions[i].LicenseNo))
                            validSupervisionLicenseNo.Add(BldSupervisions[i]);
                    }
                    lastLicenseNo = validSupervisionLicenseNo.OrderByDescending(x => x.LicenseNo).FirstOrDefault().LicenseNo;
                }
                //if (BldSupervisions == null)
                //{
                //    lastLicenseNo = servicesType.ServiceNameEn.Split(' ')[0].ToString()[0] + "/" + CurrentYear + "/" + "00000";
                //}
                //else
                //{
                //    lastLicenseNo = BldSupervisions.LicenseNo;
                //}
                //string[] isPermit = IsPermitLNo(BldSupervisions.LicenseNo);
                //string PermitYear = lastLicenseNo.Substring(0, 4);
                //lastLicenseNo = lastLicenseNo.Substring(4);
                string[] Result = lastLicenseNo.Split(new[] { '/' }, 3);

                string ResultTNo = Result[2];
                string ResultYear = Result[1];
                string ResultName = Result[0];

                if (ResultYear == CurrentYear)
                {
                    LastTNo = int.Parse(ResultTNo);
                    LastTNo = LastTNo + 1;
                    LicenseNo = "" + ResultName + "/" + ResultYear + "/" + String.Format("{0:00000}", LastTNo) + "";
                }
                else
                {
                    LastTNo = 1;
                    LicenseNo = "" + ResultName + "/" + CurrentYear + "/" + String.Format("{0:00000}", LastTNo) + "";
                }
                return LicenseNo;
            }
        }

        private static string[] IsPermitLNo(string licenseNo)
        {
            string[] Result = licenseNo.Split(new[] { '/' }, 3);
            if (Result.Count() == 2)
                Result = licenseNo.Split(new[] { '/' }, 2);
            return Result;
        }

        public static string GenLicenseNo()
        {
            string LicenseNo = "S/2021/9999";
            using (eServicesEntities db = new eServicesEntities())
            {

                string CurrentYear = DateTime.Now.Year.ToString();
                int LastTNo;

                string lastLicenseNo;

                BldPermits Bld = db.BldPermits.OrderByDescending(x => x.LicenseNo).FirstOrDefault();

                if (Bld == null)
                {
                    lastLicenseNo = "S" + "/" + CurrentYear + "/" + "00000";
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
                    LicenseNo = "" + "S" + "/" + PermitYear + "/" + String.Format("{0:00000}", LastTNo) + "";
                }
                else
                {
                    LastTNo = 1;
                    LicenseNo = "" + "S" + "/" + CurrentYear + "/" + String.Format("{0:00000}", LastTNo) + "";
                }

                return LicenseNo;
            }
        }

        #endregion

        #region Method :: Save Consultat Supervisions 


        public static string SaveConsultantSupervisions(SupervisionViewModel oModel)
        {
            using (var db = new eServicesEntities())
            {
                try
                {
                    var bldSupervisionServices = db.BldSupervisionServices.SingleOrDefault(x => x.ID == oModel.BuildingSupervision.ID);
                    if (bldSupervisionServices == null)
                    {
                        return null;
                    }
                    if (oModel.ListOfAttachments != null)
                    {
                        var lstAttachments = Mapper.Map<List<PermitsAttachments>, List<BldPermitsAttachments>>(oModel.ListOfAttachments);
                        foreach (var file in lstAttachments)
                        {
                            var attachment = db.BldPermitsAttachments.Where(x => x.Id == file.Id).SingleOrDefault();
                            if (attachment != null)
                            {
                                attachment.Description = oModel.Attachments.Description + "BLD-SupervisionService";
                                attachment.UpdatedBy = oModel.oUserInfo.FirstName;
                                attachment.UpdatedOn = DateTime.Now;
                                db.SaveChanges();
                            }
                            else
                            {
                                file.BldPermitId = bldSupervisionServices.BldPermitID;
                                db.BldPermitsAttachments.Add(file);
                                db.SaveChanges();
                            }
                        }
                    }
                    bldSupervisionServices.Status = 10;
                    bldSupervisionServices.UpdatedBy = oModel.oUserInfo.FirstName;
                    bldSupervisionServices.UpdatedOn = DateTime.Now;
                    db.SaveChanges();
                    return "ok";
                }
                catch (Exception e)
                {
                    return null;
                }

            }

        }
        #endregion

        #region Method :: Assign Supervisions 

        public static BuildingSupervision AssignSupervisions(BuildingSupervision Supervisions)
        {
            using (var db = new eServicesEntities())
            {
                var bldSupervisionServices = new BldSupervisionServices();
                var BuildingSupervision = new BuildingSupervision();
                try
                {
                    bldSupervisionServices = db.BldSupervisionServices.Where(x => x.ID == Supervisions.ID).SingleOrDefault();

                    if (bldSupervisionServices == null)
                    {
                        return null;
                    }
                    if (bldSupervisionServices.DmEngineerNo == null)
                    {
                        bldSupervisionServices.DmEngineerNo = Supervisions.DmEngineerNo;
                        bldSupervisionServices.Status = 12;
                    }
                    else
                    {
                        bldSupervisionServices.Status = Supervisions.Status;
                    }
                    bldSupervisionServices.DmInspectorComments = Supervisions.DmInspectorComments;
                    bldSupervisionServices.DmSupervisionComments = Supervisions.DmSupervisionComments;
                    bldSupervisionServices.UpdatedOn = DateTime.Now;
                    db.SaveChanges();
                    BuildingSupervision = Mapper.Map<BldSupervisionServices, BuildingSupervision>(bldSupervisionServices);
                    return BuildingSupervision;
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

        public static string GenTransactNo()
        {
            string transactNo = null;
            using (var db = new eServicesEntities())
            {

                var currentYear = DateTime.Now.Year.ToString();
                int lastTid;

                string lastTransactNo;

                BldSupervisionServices Service = db.BldSupervisionServices.OrderByDescending(x => x.ID).FirstOrDefault();


                if (Service == null)
                {
                    lastTransactNo = "Service-0/" + currentYear + "";
                }
                else
                {
                    lastTransactNo = Service.TransactNo;
                }


                var srvName = lastTransactNo.Substring(0, 8);
                lastTransactNo = lastTransactNo.Substring(8);
                var result = lastTransactNo.Split(new[] { '/' }, 2);
                var resultTid = result[0];
                var resultYear = result[1];
                if (resultYear == currentYear)
                {
                    lastTid = int.Parse(resultTid);
                    lastTid = lastTid + 1;
                    transactNo = "" + srvName + "" + lastTid + "/" + currentYear + "";
                }
                else
                {
                    lastTid = 1;
                    transactNo = "" + srvName + "" + lastTid + "/" + currentYear + "";
                }

                return transactNo;
            }
        }

        public static string UpdatePaymentStatus(int SupervisionID, int status)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldSupervisionServices _BldSupervision = new BldSupervisionServices();
                try
                {

                    _BldSupervision = db.BldSupervisionServices.Where(x => x.ID == SupervisionID).SingleOrDefault();

                    if (_BldSupervision == null)
                    {
                        return null;
                    }

                    _BldSupervision.Status = status;

                    db.SaveChanges();
                    return _BldSupervision.ID.ToString();
                }

                catch (Exception)
                {
                    return null;
                }

            }
        }

        public static string SaveSupervisionTransact(SupervisionViewModel oModel)
        {
            using (var db = new eServicesEntities())
            {
                try
                {
                    var _BldSupervisionTransact = new BldTransactions();
                    if (oModel.BuildingSupervision.ServiceTypeID == 10)
                        _BldSupervisionTransact = new BldTransactions();
                    else if (oModel.BuildingSupervision.ServiceTypeID == 6 || oModel.BuildingSupervision.ServiceTypeID == 9)
                    {
                        _BldSupervisionTransact = db.BldTransactions.Where(x => x.BldSupervisionId == oModel.BuildingSupervision.ID && (x.TypeId == 6 || x.TypeId == 9)).SingleOrDefault();
                        if (_BldSupervisionTransact != null)
                            return "لا يمكن اضافة طلب مرة اخرى";
                    }
                    //if (oModel.oUserInfo.ConsultantCrNo == 0)
                    //{
                    //    return null;
                    //}
                    _BldSupervisionTransact = Mapper.Map<Transactions, BldTransactions>(oModel.SupervisionTransact);

                    _BldSupervisionTransact.BldPermitId = oModel.BuildingPermits.Id;
                    _BldSupervisionTransact.TypeId = oModel.BuildingSupervision.ServiceTypeID;
                    if (oModel.BuildingSupervision.ServiceTypeID == 10)
                        _BldSupervisionTransact.BldSupervisionId = oModel.BuildingSupervision.SupervisionRelatedID;
                    else if (oModel.BuildingSupervision.ServiceTypeID == 6 || oModel.BuildingSupervision.ServiceTypeID == 9)
                        _BldSupervisionTransact.BldSupervisionId = oModel.BuildingSupervision.ID;
                    _BldSupervisionTransact.BldSupervisionLicenseNo = oModel.BuildingSupervision.LicenseNo;
                    _BldSupervisionTransact.BldPermitLicenseNo = oModel.BuildingPermits.LicenseNo;
                    _BldSupervisionTransact.Status = 8;
                    db.BldTransactions.Add(_BldSupervisionTransact);
                    db.SaveChanges();

                    return _BldSupervisionTransact.BldSupervisionId.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static string UpdateSupervisionTransact(ViewModels.Internal.Permits.SupervisionViewModel oModel, int transactID)
        {
            using (var db = new eServicesEntities())
            {
                try
                {
                    var _BldSupervisionTransact = db.BldTransactions.Where(x => x.Id == transactID).SingleOrDefault();
                    //_BldSupervisionTransact = Mapper.Map<Transactions, BldTransactions>(oModel.SupervisionTransact);


                    _BldSupervisionTransact.BldNewSupervisionID = oModel.BuildingSupervision.ID;
                    _BldSupervisionTransact.Status = 67;

                    db.SaveChanges();

                    return _BldSupervisionTransact.BldNewSupervisionID.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

    }
}
