using AutoMapper;
using DMeServices.DAL;
using DMeServices.Models.BuildingServices;
using DMeServices.Models.ViewModels.Internal.Permits;
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

        public static string SavePaymentDetails(PermitsViewModel oModel)
        {

            using (eServicesEntities db = new eServicesEntities())
            {

                BldPayment _BldPayment = new BldPayment();
                try
                {
                    
                    
                    _BldPayment = db.BldPayment.Where(x => x.PaymentID == oModel.Payment.PaymentID).SingleOrDefault();

                    if (_BldPayment != null)
                    {
                        return null;
                    }
                    //if (oModel.oUserInfo.ConsultantCrNo == null)
                    //{
                    //    return null;
                    //}

                    

                    _BldPayment = Mapper.Map<Payments, BldPayment>(oModel.Payment);
                    _BldPayment.BldPermitId = oModel.BuildingPermits.Id;
                    _BldPayment.PaymentTotalAmount = oModel.TempGrandTotal;
                    db.BldPayment.Add(_BldPayment);

                    db.SaveChanges();

                    if (oModel.PaymentDetailsList != null)
                    {
                        List<BldPaymentDetails> LstPaymentDetails = Mapper.Map<List<PaymentDetails>, List<BldPaymentDetails>>(oModel.PaymentDetailsList);
                        foreach (var PaymentDetail in LstPaymentDetails)
                        {
                            PaymentDetail.PaymentID = _BldPayment.PaymentID;
                            db.BldPaymentDetails.Add(PaymentDetail);
                            db.SaveChanges();
                        }

                    }




                    return _BldPayment.PaymentID.ToString();

                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        #endregion
    }
}
