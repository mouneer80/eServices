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
    public class WelayatCom
    {

        #region Method :: All Welayat

        public static List<Welyat> AllWelayat()
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldWelyat> _BldWelyat = db.BldWelyat.OrderBy(x => x.WelyahCode).ToList();
                List<Welyat> _Welyat = Mapper.Map<List<BldWelyat>, List<Welyat>>(_BldWelyat);

                return _Welyat;
            }

        }
        #endregion







        #region Method :: All Structured Welyat

        public static List<Welyat> AllStructuredWelyat()
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldWelyat> _BldWelyat = db.BldWelyat.Where(x => x.WelyahID > 3).AsEnumerable().ToList();
                List<Welyat> _Welyat = Mapper.Map<List<BldWelyat>, List<Welyat>>(_BldWelyat);

                return _Welyat;
            }

        }
        #endregion


        #region Method :: Welyat By ID

        public static Welyat WelayahByID(int Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldWelyat _BldWelyat = db.BldWelyat.Where(x => x.WelyahID == Id).SingleOrDefault();
                Welyat _Welyat = Mapper.Map<BldWelyat, Welyat>(_BldWelyat);

                return _Welyat;
            }

        }
        #endregion


    }
}
