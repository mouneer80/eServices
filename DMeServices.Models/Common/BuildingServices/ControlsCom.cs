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
    public class ControlsCom
    {

        #region Method :: AllBuildingControls

        public static List<BuildingControls> AllControls()
        {
            using (var db = new eServicesEntities())
            {
                var bldControlServices = db.BldControlServices.Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(x => x.BldPermits.Id).ToList();
                var buildingControls = Mapper.Map<List<BldControlServices>, List<BuildingControls>>(bldControlServices);
                return buildingControls;
            }

        }

        #endregion

        #region Method :: Control By ID

        public static BuildingControls ControlsById(int id)
        {
            using (var db = new eServicesEntities())
            {

                var bldControlServices = db.BldControlServices.Where(x => x.ID == id).Include(y => y.BldPermits.BldPermitsAttachments).SingleOrDefault();
                var buildingControls = Mapper.Map<BldControlServices, BuildingControls>(bldControlServices);

                return buildingControls;
            }

        }
        #endregion

        #region Method :: Control By Permit ID

        public static BuildingControls ControlsByPermitId(int id)
        {
            using (var db = new eServicesEntities())
            {

                var bldControlServices = db.BldControlServices.Where(x => x.BldPermits.Id == id).Include(y => y.BldPermits.BldPermitsAttachments).SingleOrDefault();
                var buildingControls = Mapper.Map<BldControlServices, BuildingControls>(bldControlServices);

                return buildingControls;
            }

        }
        #endregion

        #region Method :: Control By TransNo


        public static BuildingControls ControlsByTransNo(string transNo)
        {
            using (var db = new eServicesEntities())
            {

                var bldControlServices = db.BldControlServices.Where(x => x.BldPermits.TransactNo == transNo).Include(y => y.BldPermits.BldPermitsAttachments).SingleOrDefault();
                var buildingControls = Mapper.Map<BldControlServices, BuildingControls>(bldControlServices);

                return buildingControls;
            }

        }



        #endregion

        #region Method :: Control By ConsultantNo


        public static List<BuildingControls> ControlsByConsultantNo(long consultantNo)
        {
            using (var db = new eServicesEntities())
            {
                var bldControlServices = db.BldControlServices.Where(x => x.BldPermits.ConsultantCrNo == consultantNo).OrderByDescending(x => x.BldPermits.Id).Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(x => x.BldPermits.Id).ToList();
                var buildingControls = Mapper.Map<List<BldControlServices>, List<BuildingControls>>(bldControlServices);
                return buildingControls;
            }

        }
        #endregion

        #region Method :: Control By LandOwner CivilID
        public static List<BuildingControls> ControlsByLandOwnerCivilId(long landOwnerCivilId)
        {
            using (var db = new eServicesEntities())
            {
                List<BldControlServices> bldControlServices = db.BldControlServices.Where(x => x.BldPermits.OwnerCivilId == landOwnerCivilId).ToList();
                var buildingControls = Mapper.Map<List<BldControlServices>, List<BuildingControls>>(bldControlServices);
                return buildingControls;
            }
        }

        public static List<ControlServicesTypes> AllServices(int isConsaltant)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldControlServicesTypes> _BldServicesTypes = db.BldControlServicesTypes.Where(x => x.ServiceStatus == isConsaltant).ToList();
                List<ControlServicesTypes> _ServicesTypes = Mapper.Map<List<BldControlServicesTypes>, List<ControlServicesTypes>>(_BldServicesTypes);

                return _ServicesTypes;
            }
        }

        public static List<ControlServicesTypes> AllServices()
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldControlServicesTypes> _BldServicesTypes = db.BldControlServicesTypes.OrderBy(x => x.ID).ToList();
                List<ControlServicesTypes> _ServicesTypes = Mapper.Map<List<BldControlServicesTypes>, List<ControlServicesTypes>>(_BldServicesTypes);

                return _ServicesTypes;
            }
        }
        #endregion

        #region Method :: Control By CivilId
        public static List<BuildingControls> ControlsByConsultantCivilId(long civilId)
        {
            using (var db = new eServicesEntities())
            {
                List<BldControlServices> bldControlServices = db.BldControlServices.Where(x => x.BldPermits.ConsultantCivilId == civilId).OrderByDescending(x => x.BldPermits.Id).Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(x => x.BldPermits.Id).ToList();
                var buildingControls = Mapper.Map<List<BldControlServices>, List<BuildingControls>>(bldControlServices);
                return buildingControls;
            }
        }
        #endregion

        #region Method :: Get All New Controls By CivilId


        public static List<BuildingControls> GetAllNewControlsByConsultantCivilId(long civilId)
        {
            using (var db = new eServicesEntities())
            {
                List<BldControlServices> bldControlServices = db.BldControlServices.Where(x => x.BldPermits.ConsultantCivilId == civilId && x.BldPermits.WorkflowStatus == 8).OrderByDescending(x => x.BldPermits.Id).Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(x => x.BldPermits.Id).ToList();
                var buildingControls = Mapper.Map<List<BldControlServices>, List<BuildingControls>>(bldControlServices);
                return buildingControls;
            }

        }
        #endregion

        #region Method :: Get All New Controls 


        public static List<BuildingControls> GetAllControlsByFlowStatus(int flowId)
        {
            using (var db = new eServicesEntities())
            {
                List<BldControlServices> bldControlServices = db.BldControlServices.Where(x => x.BldPermits.WorkflowStatus == flowId).OrderByDescending(x => x.BldPermits.Id).Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(x => x.BldPermits.Id).ToList();
                var buildingControls = Mapper.Map<List<BldControlServices>, List<BuildingControls>>(bldControlServices);
                return buildingControls;
            }

        }
        #endregion

        #region Method :: Controls By Engineer Number


        public static List<BuildingControls> ControlsByEngineerId(int? engNum)
        {
            using (var db = new eServicesEntities())
            {


                List<BldControlServices> bldControlServices = db.BldControlServices.Where(x => x.BldPermits.DmEngineerNo == engNum).OrderByDescending(x => x.BldPermits.Id).Include(y => y.BldPermits.BldPermitsAttachments).OrderByDescending(x => x.BldPermits.Id).ToList();
                var buildingControls = Mapper.Map<List<BldControlServices>, List<BuildingControls>>(bldControlServices);
                return buildingControls;
            }

        }
        #endregion

        #region Method :: Save Controls 


        public static string SaveControls(ControlsViewModel oModel)
        {
            using (var db = new eServicesEntities())
            {
                var bldControlServices = new BldControlServices();
                try
                {

                    bldControlServices = db.BldControlServices.SingleOrDefault(x => x.ID == oModel.BuildingControls.ID || x.TransactNo == oModel.BuildingControls.TransactNo);

                    if (bldControlServices != null)
                    {
                        return null;
                    }
                    if (oModel.oUserInfo.ConsultantCrNo == null)
                    {
                        return null;
                    }



                    bldControlServices = Mapper.Map<BuildingControls, BldControlServices>(oModel.BuildingControls);
                    bldControlServices.TransactNo = GenTransactNo();
                    bldControlServices.BldPermits.ConsultantCrNo = (long)oModel.oUserInfo.ConsultantCrNo;
                    bldControlServices.BldPermits.ConsultantCivilId = (long)oModel.oUserInfo.CivilId;
                    bldControlServices.CreatedBy = oModel.oUserInfo.FullName;
                    bldControlServices.CreatedOn = DateTime.Now.Date;
                    bldControlServices.RequestDate = DateTime.Now.Date;
                    bldControlServices.Status = 8;
                    db.BldControlServices.Add(bldControlServices);
                    db.SaveChanges();
                    if (oModel.ListOfAttachments != null)
                    {
                        var lstAttachments = Mapper.Map<List<PermitsAttachments>, List<BldPermitsAttachments>>(oModel.ListOfAttachments);
                        foreach (var file in lstAttachments)
                        {
                            file.BldPermitId = bldControlServices.BldPermits.Id;
                            file.Description = bldControlServices.TransactNo;
                            db.BldPermitsAttachments.Add(file);
                            db.SaveChanges();
                        }

                    }

                    return bldControlServices.TransactNo;

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }




        #endregion

        #region Method :: Save Engineer Controls 


        public static string SaveEngineerControls(ViewModels.Internal.Permits.ControlsViewModel oModel)
        {
            using (var db = new eServicesEntities())
            {
                try
                {

                    var bldControlServices = db.BldControlServices.SingleOrDefault(x => x.DmEngineerNo == oModel.BuildingControls.DmEngineerNo);

                    if (bldControlServices == null)
                    {
                        return null;
                    }

                    //  _BldControlServices = Mapper.Map<BuildingControls, BldControlServices>(oModel.BuildingControls);
                    bldControlServices.Status = oModel.BuildingControls.Status;
                    bldControlServices.UpdatedBy = oModel.oEmployeeInfo.NAME;
                    bldControlServices.UpdatedOn = DateTime.Now.Date;
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

        #region Method :: Save Consultat Controls 


        public static string SaveConsultantcontrols(ControlsViewModel oModel)
        {
            using (var db = new eServicesEntities())
            {
                try
                {

                    var bldControlServices = db.BldControlServices.SingleOrDefault(x => x.ConsultantCivilId == oModel.BuildingControls.ConsultantCivilId);

                    if (bldControlServices == null)
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
                                attachment.Description = oModel.Attachments.Description + "BLD-ControlService";
                                attachment.UpdatedBy = oModel.oUserInfo.FirstName;
                                attachment.UpdatedOn = DateTime.Now.Date;
                                db.SaveChanges();
                            }
                            else
                            {
                                file.BldPermitId = bldControlServices.ID;
                                db.BldPermitsAttachments.Add(file);
                                db.SaveChanges();
                            }


                        }

                    }

                    //  _BldControlServices = Mapper.Map<BuildingControls, BldControlServices>(oModel.BuildingControls);
                    bldControlServices.Status = 10;
                    bldControlServices.UpdatedBy = oModel.oUserInfo.FirstName;
                    bldControlServices.UpdatedOn = DateTime.Now.Date;
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

        #region Method :: Assign Controls 

        public static BuildingControls AssignControls(BuildingControls controls)
        {
            using (var db = new eServicesEntities())
            {
                var bldControlServices = new BldControlServices();
                var buildingControls = new BuildingControls();
                try
                {

                    bldControlServices = db.BldControlServices.Where(x => x.ID == controls.ID).SingleOrDefault();

                    if (bldControlServices == null)
                    {
                        return null;
                    }

                    bldControlServices.DmEngineerNo = controls.DmEngineerNo;
                    bldControlServices.Status = 12;
                    db.SaveChanges();
                    buildingControls = Mapper.Map<BldControlServices, BuildingControls>(bldControlServices);
                    return buildingControls;

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

                BldControlServices Service = db.BldControlServices.OrderByDescending(x => x.ID).FirstOrDefault();


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
