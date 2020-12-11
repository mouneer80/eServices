using System.Collections.Generic;
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
        #endregion
        
        #region Method :: Edit Company Details
        public static string SaveCompany(CompanyViewModel compData)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                var isCompanyExist = db.MociData.SingleOrDefault(x => x.COMMERCIAL_NO == compData.CompanyData.COMMERCIAL_NO);
                if (isCompanyExist == null)
                {
                    return null;
                }
                db.MociData.Add(compData.CompanyData);
                db.SaveChanges();
                if (compData.ConsultantsList != null)
                {
                    var lstConsultantsList = Mapper.Map<List<User>, List<Users>>(compData.ConsultantsList);
                    foreach (var consultant in lstConsultantsList)
                    {
                        db.Users.Add(consultant);
                        db.SaveChanges();
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
    }
}
