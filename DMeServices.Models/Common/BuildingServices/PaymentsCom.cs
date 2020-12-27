using AutoMapper;
using DMeServices.DAL;
using DMeServices.Models.BuildingServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Common.BuildingServices
{
    public class PaymentsCom
    {

        #region Method :: List Payments By Permits ID

        public static List<Payments> PaymentsByPermitsID(long BldPermitId)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldPayment> _BldPayment = db.BldPayment.Where(x => x.BldPermitId == BldPermitId).OrderByDescending(x => x.PaymentID).ToList();
                List<Payments> _Payments = Mapper.Map<List<BldPayment>, List<Payments>>(_BldPayment);
                return _Payments;
            }

        }
        #endregion


        #region Method :: List Payment Details By Permits ID

        public static List<PaymentDetails> MapsPaymentDetailsByPermitsID(int BldPermitId)
        {
            List<Payments> paymentList = PaymentsByPermitsID(BldPermitId);
            List<PaymentDetails> _PaymentDetailsList = new List<PaymentDetails>();
            foreach (var item in paymentList)
            {
                using (eServicesEntities db = new eServicesEntities())
                {

                    List<BldPaymentDetails> _BldPaymentDetails = db.BldPaymentDetails.Where(x => x.PaymentID == item.PaymentID).OrderByDescending(x => x.PaymentDetailID).ToList();
                    List<PaymentDetails> _PaymentDetails = Mapper.Map<List<BldPaymentDetails>, List<PaymentDetails>>(_BldPaymentDetails);
                    return _PaymentDetailsList = _PaymentDetails;
                    
                }
            }
            return _PaymentDetailsList;

        }
        #endregion



       
        #region Method ::  Payment By ID

        public static Payments PaymentByID(int Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldPayment _BldPayment = db.BldPayment.Where(x => x.PaymentID == Id).SingleOrDefault();
                Payments _Payment = Mapper.Map<BldPayment, Payments>(_BldPayment);
                return _Payment;
            }

        }
        #endregion



        #region Method ::  Payment Details By ID

        public static PaymentDetails PaymentDetailsByID(int Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldPaymentDetails _BldPaymentDetails = db.BldPaymentDetails.Where(x => x.PaymentDetailID == Id).SingleOrDefault();
                PaymentDetails _PaymentDetails = Mapper.Map<BldPaymentDetails, PaymentDetails>(_BldPaymentDetails);
                return _PaymentDetails;
            }
        }
        #endregion
    }
}
