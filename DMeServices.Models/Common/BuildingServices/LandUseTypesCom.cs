using AutoMapper;
using DMeServices.DAL;
using DMeServices.Models.BuildingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Common.BuildingServices
{
    public class LandUseTypesCom
    {

        #region Method :: All LandUseTypes

        public static List<LandUseTypes> AllLandUseTypes()
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldLandUseTypes> _BldLandUseTypes = db.BldLandUseTypes.OrderBy(x => x.ID).ToList();
                List<LandUseTypes> _LandUseTypes = Mapper.Map<List<BldLandUseTypes>, List<LandUseTypes>>(_BldLandUseTypes);

                return _LandUseTypes;
            }

        }
        #endregion







        #region Method :: All Structured LandUseTypes

        public static List<LandUseTypes> AllStructuredLandUseTypes()
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldLandUseTypes> _BldLandUseTypes = db.BldLandUseTypes.Where(x => x.ID > 3).AsEnumerable().ToList();
                List<LandUseTypes> _LandUseTypes = Mapper.Map<List<BldLandUseTypes>, List<LandUseTypes>>(_BldLandUseTypes);

                return _LandUseTypes;
            }

        }
        #endregion


        #region Method :: LandUseTypes By ID

        public static LandUseTypes WelayahByID(int Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldLandUseTypes _BldLandUseTypes = db.BldLandUseTypes.Where(x => x.ID == Id).SingleOrDefault();
                LandUseTypes _LandUseTypes = Mapper.Map<BldLandUseTypes, LandUseTypes>(_BldLandUseTypes);

                return _LandUseTypes;
            }

        }
        #endregion


    }
}
