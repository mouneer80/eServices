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
    public class ServiceFeesCom
    {

        #region Method :: All ServiceFees

        public static List<ServiceFee> AllServiceFees()
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldServiceFees> _BldServiceFees = db.BldServiceFees.OrderByDescending(x => x.ServiceID).ToList();
                List<ServiceFee> _ServiceFees = Mapper.Map<List<BldServiceFees>, List<ServiceFee>>(_BldServiceFees);

                return _ServiceFees;
            }

        }
        #endregion







        #region Method :: All Structured Types

        public static List<ServiceFeesDetails> AllStructuredServiceFees()
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldServiceFees> _BldServiceFees = db.BldServiceFees.Where(x => x.ServiceID > 3).AsEnumerable().ToList();
                List<ServiceFeesDetails> _ServiceFees = Mapper.Map<List<BldServiceFees>, List<ServiceFeesDetails>>(_BldServiceFees);

                return _ServiceFees;
            }

        }
        #endregion


        #region Method :: ServiceFees By ID

        public static ServiceFeesDetails TypeByID(int Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldServiceFees _BldServiceFees = db.BldServiceFees.Where(x => x.ServiceID == Id).SingleOrDefault();
                ServiceFeesDetails _ServiceFees = Mapper.Map<BldServiceFees, ServiceFeesDetails>(_BldServiceFees);

                return _ServiceFees;
            }

        }
        #endregion


    }
}
