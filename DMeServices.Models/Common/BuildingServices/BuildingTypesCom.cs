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
    public class BuildingTypesCom
    {

        #region Method :: All BuildingTypes

        public static List<BuildingTypes> AllBuildingTypes()
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldBuildingTypes> _BldBuildingTypes = db.BldBuildingTypes.OrderBy(x => x.ID).ToList();
                List<BuildingTypes> _BuildingTypes = Mapper.Map<List<BldBuildingTypes>, List<BuildingTypes>>(_BldBuildingTypes);

                return _BuildingTypes;
            }

        }
        #endregion







        #region Method :: All Structured BuildingTypes

        public static List<BuildingTypes> AllStructuredBuildingTypes()
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldBuildingTypes> _BldBuildingTypes = db.BldBuildingTypes.Where(x => x.ID > 3).AsEnumerable().ToList();
                List<BuildingTypes> _BuildingTypes = Mapper.Map<List<BldBuildingTypes>, List<BuildingTypes>>(_BldBuildingTypes);

                return _BuildingTypes;
            }

        }
        #endregion


        #region Method :: BuildingTypes By ID

        public static BuildingTypes WelayahByID(int Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldBuildingTypes _BldBuildingTypes = db.BldBuildingTypes.Where(x => x.ID == Id).SingleOrDefault();
                BuildingTypes _BuildingTypes = Mapper.Map<BldBuildingTypes, BuildingTypes>(_BldBuildingTypes);

                return _BuildingTypes;
            }

        }
        #endregion


    }
}
