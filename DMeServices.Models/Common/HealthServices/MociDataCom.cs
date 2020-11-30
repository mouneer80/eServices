using AutoMapper;
using DMeServices.DAL;
using DMeServices.Models.HealthServices;
using DMeServices.Models.Models.HealthServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Common.HealthServices
{
  public  class MociDataCom
    {
        
        #region Method :: MociData By Civil ID
        
        public static List<MOCI_Permits_BY_CIVILID> MociDataByCivilID(long CivilID)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<MOCI_Permits_BY_CIVILID_Result> LstMociData = db.MOCI_Permits_BY_CIVILID(CivilID.ToString()).ToList();

                List<MOCI_Permits_BY_CIVILID> LstHmociData = Mapper.Map<List<MOCI_Permits_BY_CIVILID_Result>, List<MOCI_Permits_BY_CIVILID>>(LstMociData);

                return LstHmociData;

            }
        }
        #endregion

    }
}
