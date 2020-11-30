using AutoMapper;
using DMeServices.DAL;
using DMeServices.Models.HealthServices;
using DMeServices.Models.Models.HealthServices;
using DMeServices.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Common.HealthServices
{
  public  class PermitDataCom
    {
        
        #region Method :: MociData By Municipal No

        public static List<MOCI_Permits_BY_MunicipalNo> PermitsBYMunicipalNo(long MunicipalNo, long CivilID)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<MOCI_Permits_BY_MunicipalNo_Result> LstPermitData = db.MOCI_Permits_BY_MunicipalNo(MunicipalNo, CivilID.ToString()).ToList();

                List<MOCI_Permits_BY_MunicipalNo> LstHPermitData = Mapper.Map<List<MOCI_Permits_BY_MunicipalNo_Result>, List<MOCI_Permits_BY_MunicipalNo>>(LstPermitData);

                return LstHPermitData;

            }
        }
        #endregion


        #region Method :: Save Permits 


        public static int SavePermitsDuration(HealthRenewalViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                HsPermitDuration _HsPermits = new HsPermitDuration();
                try
                {

                    _HsPermits = Mapper.Map<PermitDuration, HsPermitDuration>(oModel.PermitDuration);
                    // Will be modified later
                    _HsPermits.PermitStartDate = oModel.PermitDuration.ContractStartDate;
                    _HsPermits.PermitEndDate = oModel.PermitDuration.ContractEndDate;
                    //
                    db.HsPermitDuration.Add(_HsPermits);
                    db.SaveChanges();
                    if (oModel.ListOfAttachments != null)
                    {
                        int i = 1;
                        List<HsContractAttachments> LstAttachments = Mapper.Map<List<ContractAttachments>, List<HsContractAttachments>>(oModel.ListOfAttachments);
                        foreach (var File in LstAttachments)
                        {
                            File.HsPermitDurationId = _HsPermits.Id;
                            File.Description = i.ToString();
                            db.HsContractAttachments.Add(File);
                            db.SaveChanges();
                            i++;
                        }

                    }
                    return 1;

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        #endregion

        #region Method :: All Inspectors

        public static List<Employee> AllInspectors()
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<Employees> _InspectorEmps = db.Employees.Where(x => x.JobId == 3).ToList();
                List<Employee> _Inspectors = Mapper.Map<List<Employees>, List<Employee>>(_InspectorEmps);

                return _Inspectors;
            }

        }
        #endregion

        #region Method :: Inspection Status

        public static List<LookupType> InspectionStatus()
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<LookupTypes> _InspectionStatus = db.LookupTypes.Where(x => x.LookupParentId == 5).ToList();
                List<LookupType> _Status = Mapper.Map<List<LookupTypes>, List<LookupType>>(_InspectionStatus);

                return _Status;
            }

        }
        #endregion

        #region Method :: Save Inspector Feedback 

        public static int SaveInspectorFeedback(HealthRenewalInternalViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                HsPermitsInspection _HsInspectorFeedback = new HsPermitsInspection();
                try
                {

                    _HsInspectorFeedback = Mapper.Map<PermitsInspection, HsPermitsInspection>(oModel.PermitsInspection);
                    db.HsPermitsInspection.Add(_HsInspectorFeedback);
                    db.SaveChanges();

                    long PermitDurationID = (long)_HsInspectorFeedback.PermitDurationId;
                    var PermitDurationData = db.HsPermitDuration.Where(x => x.Id == PermitDurationID).FirstOrDefault();
                    if (PermitDurationData != null)
                    {
                        PermitDurationData.WorkflowStatus=13;
                        db.SaveChanges();
                    }

                    return 1;

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        #endregion


        #region Method :: Assign Inspector 

        public static int AssignInspector(HealthRenewalInternalViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                HsPermitDuration _HsAssignInspector = new HsPermitDuration();
                try
                {

                    _HsAssignInspector = Mapper.Map<PermitDuration, HsPermitDuration>(oModel.PermitDuration);
                    
                    long PermitDurationID = (long)_HsAssignInspector.Id;
                    var PermitDurationData = db.HsPermitDuration.Where(x => x.Id == PermitDurationID).FirstOrDefault();
                    if (PermitDurationData != null)
                    {
                        PermitDurationData.WorkflowStatus = 12;
                        PermitDurationData.InspectorNo = oModel.PermitDuration.InspectorNo;
                        PermitDurationData.InspectionDate = oModel.PermitDuration.InspectionDate;
                        PermitDurationData.HeadSectionNotes = oModel.PermitDuration.HeadSectionNotes;
                        db.SaveChanges();
                        DMeServices.Models.Common.SmsCom.SendSms("968" + oModel.PermitDuration.CivilId, "مرحبا بك ، سيتم إرسال فريق التفتيش بتاريخ :   " + DateTime.Parse(oModel.PermitDuration.InspectionDate.ToString()).ToString("MM/dd/yyyy"));
                    }

                    return 1;

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        #endregion

        #region Method :: Assign Inspector 

        public static int RevenueOperation(HealthRenewalInternalViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                HsPermitDuration _HsAssignInspector = new HsPermitDuration();
                try
                {

                    _HsAssignInspector = Mapper.Map<PermitDuration, HsPermitDuration>(oModel.PermitDuration);

                    long PermitDurationID = (long)_HsAssignInspector.Id;
                    var PermitDurationData = db.HsPermitDuration.Where(x => x.Id == PermitDurationID).FirstOrDefault();
                    if (PermitDurationData != null)
                    {
                        PermitDurationData.WorkflowStatus = 14;
                        PermitDurationData.RevenueReceiptNo = oModel.PermitDuration.RevenueReceiptNo;
                        PermitDurationData.RevenuePermitFees = oModel.PermitDuration.RevenuePermitFees;
                        PermitDurationData.RevenueReceiptDate = DateTime.Now;
                        db.SaveChanges();
                    }

                    return 1;

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }

        #endregion


        #region Method :: Permits HeadSection

        public static List<MOCI_Permits_HeadSection> PermitsHeadSection()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<MOCI_Permits_HeadSection_Result> LstPermitsHeadSection = db.MOCI_Permits_HeadSection().ToList();

                List<MOCI_Permits_HeadSection> LstHPermitsHeadSection = Mapper.Map<List<MOCI_Permits_HeadSection_Result>, List<MOCI_Permits_HeadSection>>(LstPermitsHeadSection);

                return LstHPermitsHeadSection;

            }
        }
        #endregion

        #region Method :: Permits Revenue

        public static List<MOCI_Permits_Revenue> PermitsRevenue()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<MOCI_Permits_Revenue_Result> LstPermitsRevenue = db.MOCI_Permits_Revenue().ToList();

                List<MOCI_Permits_Revenue> LstHPermitsRevenue = Mapper.Map<List<MOCI_Permits_Revenue_Result>, List<MOCI_Permits_Revenue>>(LstPermitsRevenue);

                return LstHPermitsRevenue;

            }
        }
        #endregion

        #region Method :: Permits Finished

        public static List<MOCI_Permits_Renew_Finish> PermitsFinished()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<MOCI_Permits_Renew_Finish_Result> LstPermitsFinished = db.MOCI_Permits_Renew_Finish().ToList();

                List<MOCI_Permits_Renew_Finish> LstHPermitsFinished = Mapper.Map<List<MOCI_Permits_Renew_Finish_Result>, List<MOCI_Permits_Renew_Finish>>(LstPermitsFinished);

                return LstHPermitsFinished;

            }
        }
        #endregion

        #region Method :: Permits Inspector

        public static List<MOCI_Permit_Inspector> PermitsInspector(long InspectorNo)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<MOCI_Permit_Inspector_Result> LstPermitsInspector = db.MOCI_Permit_Inspector((int)InspectorNo).ToList();

                List<MOCI_Permit_Inspector> LstHPermitsInspector = Mapper.Map<List<MOCI_Permit_Inspector_Result>, List<MOCI_Permit_Inspector>>(LstPermitsInspector);

                return LstHPermitsInspector;

            }
        }
        #endregion

        #region Method :: Permit Data HeadSection

        public static List<MOCI_Permit_BY_MunicipalNo_HeadSection> PermitDataHeadSection(long MunicipalNo)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<MOCI_Permit_BY_MunicipalNo_HeadSection_Result> LstPermitDataHeadSection = db.MOCI_Permit_BY_MunicipalNo_HeadSection(MunicipalNo).ToList();

                List<MOCI_Permit_BY_MunicipalNo_HeadSection> LstHPermitDataHeadSection = Mapper.Map<List<MOCI_Permit_BY_MunicipalNo_HeadSection_Result>, List<MOCI_Permit_BY_MunicipalNo_HeadSection>>(LstPermitDataHeadSection);

                return LstHPermitDataHeadSection;

            }
        }
        #endregion

        #region Method :: Permit Data Revenue

        public static List<MOCI_Permits_BY_MunicipalNo_Revenue> PermitDataRevenue(long MunicipalNo)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<MOCI_Permits_BY_MunicipalNo_Revenue_Result> LstPermitDataRevenue = db.MOCI_Permits_BY_MunicipalNo_Revenue(MunicipalNo).ToList();

                List<MOCI_Permits_BY_MunicipalNo_Revenue> LstHPermitDataRevenue = Mapper.Map<List<MOCI_Permits_BY_MunicipalNo_Revenue_Result>, List<MOCI_Permits_BY_MunicipalNo_Revenue>>(LstPermitDataRevenue);

                return LstHPermitDataRevenue;

            }
        }
        #endregion

        #region Method ::  Attachments By ID

        public static PermitDuration AttachmentsByID(int Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                HsPermitDuration _PermitsDurationAttachment = db.HsPermitDuration.Where(x => x.Id == Id).SingleOrDefault();
                PermitDuration _PermitsAttachment = Mapper.Map<HsPermitDuration, PermitDuration>(_PermitsDurationAttachment);
                return _PermitsAttachment;
            }
        }
        #endregion

        #region Method ::  Attachments By ID And Type

        public static ContractAttachments AttachmentsByIDType(int Id, int _Type)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                HsContractAttachments _PermitsDurationAttachment = db.HsContractAttachments.Where(x => x.HsPermitDurationId == Id && x.Description == _Type.ToString()).SingleOrDefault();
                ContractAttachments _PermitsAttachment = Mapper.Map<HsContractAttachments, ContractAttachments>(_PermitsDurationAttachment);
                return _PermitsAttachment;
            }
        }
        #endregion

        #region Method :: CivilID By PermitDuration

        public static User MobileNoByCivilID(int Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                HsPermitDuration _PermitDurationData = db.HsPermitDuration.Where(x => x.Id == Id).SingleOrDefault();
                Users _MobileNo = db.Users.Where(x => x.CivilId == _PermitDurationData.CivilId).SingleOrDefault();
                User MobileNo = Mapper.Map<Users, User>(_MobileNo);

                return MobileNo;
            }

        }
        #endregion
        
        #region Method :: Get Activity Value

        public static ISIC4_LEVEL_5s ActivityValue(int _Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                ISIC4_LEVEL_5 ActivityValue = db.ISIC4_LEVEL_5.Where(x => x.Id == _Id).SingleOrDefault();
                ISIC4_LEVEL_5s _ActivityValue = Mapper.Map<ISIC4_LEVEL_5, ISIC4_LEVEL_5s>(ActivityValue);

                return _ActivityValue;
            }
        }

        #endregion
    }
}
