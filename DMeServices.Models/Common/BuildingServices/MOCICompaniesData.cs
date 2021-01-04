using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using AutoMapper;
using DMeServices.DAL;
using DMeServices.Models.ViewModels;

namespace DMeServices.Models.Common.BuildingServices
{
    public class MociCompaniesData
    {
        #region Method :: List Comapnies By Owner CivilId
        public static List<MociData> CompaniesByOwnerCivilId(long civilId)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<MociData> companies = db.MociData.Where(x => x.CIVIL_ID == civilId.ToString()).OrderByDescending(x => x.CIVIL_ID).ToList();
                //List<BuildingPermits> _BuildingPermits = Mapper.Map<List<BldPermits>, List<BuildingPermits>>(_BldPermits);
                return companies;
            }
        }
        #endregion
        
        #region Method :: Register New Comapny 
        public static string SaveCompany(MociData compData)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                MociData isCompanyExist = db.MociData.SingleOrDefault(x => x.COMMERCIAL_NO == compData.COMMERCIAL_NO);
                if (isCompanyExist != null)
                {
                    return "الشركة مسجلة من قبل";
                }
                db.MociData.Add(compData);
                db.SaveChanges();
                return "تم التسحيل بنجاح";
            }
        }

        public static List<MociCompaniesData> AllCompanies()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<MociData> _BldCompanies = db.MociData.OrderBy(x => x.COMMERCIAL_NO).ToList();
                List<MociCompaniesData> _MociCompanies = Mapper.Map<List<MociData>, List<MociCompaniesData>>(_BldCompanies);

                return _MociCompanies;
            }
        }
        #endregion

        #region Method :: Edit Company Details
        public static string SaveCompany(CompanyViewModel compData)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                var isCompanyExist = db.MociData.SingleOrDefault(x => x.COMMERCIAL_NO == compData.CompanyData.COMMERCIAL_NO && x.CIVIL_ID == compData.CompanyData.CIVIL_ID);
                if (isCompanyExist == null)
                {
                    return null;
                }
                isCompanyExist.EMAIL = compData.CompanyData.EMAIL;
                isCompanyExist.PHONE_NO = compData.CompanyData.PHONE_NO;
                
                //db.MociData.Add(isCompanyExist);
                try { 
                db.SaveChanges();
                }
                catch(Exception e) { }
                if (compData.ConsultantsList != null)
                {
                    var lstConsultantsList = Mapper.Map<List<User>, List<Users>>(compData.ConsultantsList);
                    foreach (var consultant in lstConsultantsList)
                    {
                        if (UserCom.UserByCivilID(consultant.CivilId) == null)
                        {
                            db.Users.Add(consultant);
                            db.SaveChanges();
                        }
                    }
                }
                return "تم التسحيل بنجاح";
            }
        }
        #endregion
        
        #region Method :: Get Company By CR Number
        public static MociData GetCompanyByCr(long cr)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                MociData mcData =  db.MociData.SingleOrDefault(x => x.COMMERCIAL_NO == cr);
                return mcData;
            }
        }
        #endregion

        #region Method :: Get Frist Company By CR Number
        public static MociData GetCompanyByCr_Frist(long cr)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                MociData mcData = db.MociData.FirstOrDefault(x => x.COMMERCIAL_NO == cr);
                return mcData;
            }
        }
        #endregion

        #region Method :: Get Company By CR Number and civil id
        public static MociData GetCompanyByCrCid(long cr, int cid)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                MociData mcData = db.MociData.SingleOrDefault(x => x.COMMERCIAL_NO == cr && x.CIVIL_ID == cid.ToString());
                return mcData;
            }
        }
        #endregion
    }
}
