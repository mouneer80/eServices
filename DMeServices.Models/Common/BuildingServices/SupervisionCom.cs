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
                var bldSupervisionServices = db.BldSupervisionServices.Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(x => x.BldPermits.Id).ToList();
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
        public static List<BuildingSupervision> SupervisionsByLandOwnerCivilId(long landOwnerCivilId)
        {
            using (var db = new eServicesEntities())
            {
                List<BldSupervisionServices> bldSupervisionServices = db.BldSupervisionServices.Where(x => x.BldPermits.OwnerCivilId == landOwnerCivilId).ToList();
                var BuildingSupervision = Mapper.Map<List<BldSupervisionServices>, List<BuildingSupervision>>(bldSupervisionServices);
                return BuildingSupervision;
            }
        }

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
        #endregion

        #region Method :: Supervision By CivilId
        public static List<BuildingSupervision> SupervisionsByConsultantCivilId(long civilId)
        {
            using (var db = new eServicesEntities())
            {
                List<BldSupervisionServices> bldSupervisionServices = db.BldSupervisionServices.Where(x => x.BldPermits.ConsultantCivilId == civilId).OrderByDescending(x => x.BldPermits.Id).Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(x => x.BldPermits.Id).ToList();
                var BuildingSupervision = Mapper.Map<List<BldSupervisionServices>, List<BuildingSupervision>>(bldSupervisionServices);
                return BuildingSupervision;
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
                List<BldSupervisionServices> bldSupervisionServices = db.BldSupervisionServices.Where(x => x.BldPermits.WorkflowStatus == flowId).OrderByDescending(x => x.BldPermits.Id).Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(x => x.BldPermits.Id).ToList();
                var BuildingSupervision = Mapper.Map<List<BldSupervisionServices>, List<BuildingSupervision>>(bldSupervisionServices);
                return BuildingSupervision;
            }

        }
        #endregion

        #region Method :: Supervisions By Engineer Number


        public static List<BuildingSupervision> SupervisionsByEngineerId(int? engNum)
        {
            using (var db = new eServicesEntities())
            {


                List<BldSupervisionServices> bldSupervisionServices = db.BldSupervisionServices.Where(x => x.BldPermits.DmEngineerNo == engNum).OrderByDescending(x => x.BldPermits.Id).Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(x => x.BldPermits.Id).ToList();
                var BuildingSupervision = Mapper.Map<List<BldSupervisionServices>, List<BuildingSupervision>>(bldSupervisionServices);
                return BuildingSupervision;
            }

        }
        #endregion

        #region Method :: Save Supervision 


        public static string SaveSupervision(SupervisionViewModel oModel)
        {
            using (var db = new eServicesEntities())
            {
                var bldSupervisionServices = new BldSupervisionServices();
                try
                {

                    bldSupervisionServices = db.BldSupervisionServices.SingleOrDefault(x => x.ID == oModel.BuildingSupervision.ID || x.TransactNo == oModel.BuildingSupervision.TransactNo);

                    if (bldSupervisionServices != null)
                    {
                        return null;
                    }
                    if (oModel.oUserInfo.ConsultantCrNo == null)
                    {
                        return null;
                    }



                    bldSupervisionServices = Mapper.Map<BuildingSupervision, BldSupervisionServices>(oModel.BuildingSupervision);
                    bldSupervisionServices.TransactNo = GenTransactNo();
                    bldSupervisionServices.BldPermits.ConsultantCrNo = (long)oModel.oUserInfo.ConsultantCrNo;
                    bldSupervisionServices.BldPermits.ConsultantCivilId = (long)oModel.oUserInfo.CivilId;
                    bldSupervisionServices.CreatedBy = oModel.oUserInfo.FullName;
                    bldSupervisionServices.CreatedOn = DateTime.Now.Date;
                    bldSupervisionServices.RequestDate = DateTime.Now.Date;
                    bldSupervisionServices.Status = 8;
                    db.BldSupervisionServices.Add(bldSupervisionServices);
                    db.SaveChanges();
                    if (oModel.ListOfAttachments != null)
                    {
                        var lstAttachments = Mapper.Map<List<PermitsAttachments>, List<BldPermitsAttachments>>(oModel.ListOfAttachments);
                        foreach (var file in lstAttachments)
                        {
                            file.BldPermitId = bldSupervisionServices.BldPermits.Id;
                            file.Description = bldSupervisionServices.TransactNo;
                            db.BldPermitsAttachments.Add(file);
                            db.SaveChanges();
                        }

                    }

                    return bldSupervisionServices.TransactNo;

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }




        #endregion

        #region Method :: Save Engineer Supervisions 


        public static string SaveEngineerSupervisions(ViewModels.Internal.Permits.SupervisionViewModel oModel)
        {
            using (var db = new eServicesEntities())
            {
                try
                {

                    var bldSupervisionServices = db.BldSupervisionServices.SingleOrDefault(x => x.DmEngineerNo == oModel.BuildingSupervision.DmEngineerNo);

                    if (bldSupervisionServices == null)
                    {
                        return null;
                    }

                    //  _BldSupervisionServices = Mapper.Map<BuildingSupervision, BldSupervisionServices>(oModel.BuildingSupervision);
                    bldSupervisionServices.Status = oModel.BuildingSupervision.Status;
                    bldSupervisionServices.UpdatedBy = oModel.oEmployeeInfo.NAME;
                    bldSupervisionServices.UpdatedOn = DateTime.Now.Date;
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

        #region Method :: Save Consultat Supervisions 


        public static string SaveConsultantSupervisions(SupervisionViewModel oModel)
        {
            using (var db = new eServicesEntities())
            {
                try
                {

                    var bldSupervisionServices = db.BldSupervisionServices.SingleOrDefault(x => x.ConsultantCivilId == oModel.BuildingSupervision.ConsultantCivilId);

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
                                attachment.UpdatedOn = DateTime.Now.Date;
                                db.SaveChanges();
                            }
                            else
                            {
                                file.BldPermitId = bldSupervisionServices.ID;
                                db.BldPermitsAttachments.Add(file);
                                db.SaveChanges();
                            }


                        }

                    }

                    //  _BldSupervisionServices = Mapper.Map<BuildingSupervision, BldSupervisionServices>(oModel.BuildingSupervision);
                    bldSupervisionServices.Status = 10;
                    bldSupervisionServices.UpdatedBy = oModel.oUserInfo.FirstName;
                    bldSupervisionServices.UpdatedOn = DateTime.Now.Date;
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

                    bldSupervisionServices.DmEngineerNo = Supervisions.DmEngineerNo;
                    bldSupervisionServices.Status = 12;
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
                    lastTransactNo = Service.ID.ToString();
                }


                var srvName = lastTransactNo.Substring(0, 4);
                lastTransactNo = lastTransactNo.Substring(4);
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

        #endregion

    }
}
