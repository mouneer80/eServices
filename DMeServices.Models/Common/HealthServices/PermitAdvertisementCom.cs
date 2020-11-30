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
  public  class PermitAdvertisementCom
    {

        #region Method :: Permit Advertisement

        public static List<PermitAdvertisement> PermitAdvertisement(long MunicipalNo)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<PermitAdvertisement_Result> LstPermitAdvertisementData = db.PermitAdvertisement(MunicipalNo).ToList();

                List<PermitAdvertisement> LstHPermitAdvertisementData = Mapper.Map<List<PermitAdvertisement_Result>, List<PermitAdvertisement>>(LstPermitAdvertisementData);

                return LstHPermitAdvertisementData;

            }
        }
        #endregion

    }
}
