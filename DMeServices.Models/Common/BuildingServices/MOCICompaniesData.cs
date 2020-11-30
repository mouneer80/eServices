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
    }
}
