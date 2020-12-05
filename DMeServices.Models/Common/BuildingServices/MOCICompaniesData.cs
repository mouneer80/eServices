using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DMeServices.DAL;
using DMeServices.Models.BuildingServices;

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
        
        #region Method :: Register Comapnies 
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
    }
}
