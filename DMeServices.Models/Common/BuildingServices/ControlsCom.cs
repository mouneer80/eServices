﻿using AutoMapper;
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

        #region Method :: AllBuildingPermits

        public static List<BuildingPermits> AllPermits()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldPermits> _BldPermits = db.BldPermits.Include(y => y.BldPermitsAttachments).OrderByDescending(x => x.Id).ToList();
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
                List<BldPermits> bldPermits = db.BldPermits.Where(x => x.OwnerCivilId == landOwnerCivilId).ToList();
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
                List<BldPermits> _BldPermits = db.BldPermits.Where(x => x.ConsultantCivilId == CivilId).OrderByDescending(x => x.Id).Include(y => y.BldPermitsAttachments).OrderByDescending(x => x.Id).ToList();
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
        #endregion

        #region Method :: Get All New Permits 


        public static List<BuildingPermits> GetAllPermitsByflowStatus(int FlowID)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldPermits> _BldPermits = db.BldPermits.Where(x => x.WorkflowStatus == FlowID).OrderByDescending(x => x.Id).Include(y => y.BldPermitsAttachments).OrderByDescending(x => x.Id).ToList();
                List<BuildingPermits> _BuildingPermits = Mapper.Map<List<BldPermits>, List<BuildingPermits>>(_BldPermits);
                return _BuildingPermits;
            }

        }
        #endregion

        #region Method :: Permit By Engineer Number


        public static List<BuildingPermits> PermitsByEngineerID(int? EngNum)
        {
            using (eServicesEntities db = new eServicesEntities())
            {


                List<BldPermits> _BldPermits = db.BldPermits.Where(x => x.DmEngineerNo == EngNum).OrderByDescending(x => x.Id).Include(y => y.BldPermitsAttachments).OrderByDescending(x => x.Id).ToList();
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
                    _BldPermits.TransactNo = GenTransactNo();
                    _BldPermits.ConsultantCrNo = (int)oModel.oUserInfo.ConsultantCrNo;
                    _BldPermits.CreatedBy = oModel.oUserInfo.FirstName;
                    _BldPermits.CreatedOn = DateTime.Now.Date;
                    _BldPermits.RequestDate = DateTime.Now.Date;
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

                    return _BldPermits.TransactNo;

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

                    //  _BldPermits = Mapper.Map<BuildingPermits, BldPermits>(oModel.BuildingPermits);
                    _BldPermits.WorkflowStatus = oModel.BuildingPermits.WorkflowStatus;
                    _BldPermits.UpdatedBy = oModel.oEmployeeInfo.NAME;
                    _BldPermits.UpdatedOn = DateTime.Now.Date;
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
                                Attachment.UpdatedOn = DateTime.Now.Date;
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
                    _BldPermits.UpdatedOn = DateTime.Now.Date;
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
                    _BldPermits.WorkflowStatus = 12;
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

        public static string GenTransactNo()
        {
            string TransactNo = null;
            using (eServicesEntities db = new eServicesEntities())
            {

                string CurrentYear = DateTime.Now.Year.ToString();
                int LastTID;

                string lastTransactNo;

                BldPermits Bld = db.BldPermits.OrderByDescending(x => x.Id).FirstOrDefault();


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

    }
}
