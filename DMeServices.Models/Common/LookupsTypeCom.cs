using AutoMapper;
using DMeServices.DAL;
using DMeServices.Models.BuildingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Common
{
  public  class LookupsTypeCom
    {


        #region Method :: Lookups By ID

        public static LookupType LookupByID(int Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                LookupTypes _LookupTypes = db.LookupTypes.Where(x => x.LookupId == Id).SingleOrDefault();
                LookupType _LookupType = Mapper.Map<LookupTypes, LookupType>(_LookupTypes);

                return _LookupType;
            }

        }
        #endregion


        #region Method :: Lookups By Desc

        public static List<LookupType> LookupByDesc(string Name)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<LookupTypes> _LookupTypes = db.LookupTypes.Where(x => x.LookupParentDesc == Name).ToList();
                List<LookupType> _LookupType = Mapper.Map<List<LookupTypes>, List<LookupType>>(_LookupTypes);

                return _LookupType;
            }

        }
        #endregion










    }
}
