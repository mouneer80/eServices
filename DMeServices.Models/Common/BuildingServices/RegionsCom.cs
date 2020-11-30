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
    public class RegionsCom
    {

        #region Method :: All Regions

        public static List<Regions> AllRegions()
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldRegions> _BldRegions = db.BldRegions.OrderByDescending(x => x.RegionCode).ToList();
                List<Regions> _Regions = Mapper.Map<List<BldRegions>, List<Regions>>(_BldRegions);

                return _Regions;
            }

        }
        #endregion







        #region Method :: All Structured Regions

        public static List<Regions> AllStructuredRegions()
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldRegions> _BldRegions = db.BldRegions.Where(x => x.RegionID > 3).AsEnumerable().ToList();
                List<Regions> _Regions = Mapper.Map<List<BldRegions>, List<Regions>>(_BldRegions);

                return _Regions;
            }

        }
        #endregion


        #region Method :: Regions By ID

        public static Regions RegionByID(int Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldRegions _BldRegions = db.BldRegions.Where(x => x.RegionID == Id).SingleOrDefault();
                Regions _Regions = Mapper.Map<BldRegions, Regions>(_BldRegions);

                return _Regions;
            }

        }
        #endregion

        #region Method :: Regions By Welayah ID

        public static List<Regions> RegionByWelayahID(int? Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldRegions> _BldRegions = db.BldRegions.Where(x => x.WelyahID == Id).ToList();
                List<Regions> _Regions = Mapper.Map<List<BldRegions>, List<Regions>>(_BldRegions);

                return _Regions;
            }

        }
        #endregion


    }
}
