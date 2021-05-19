using AutoMapper;
using DMeServices.DAL;
using DMeServices.Models.BuildingServices;
using DMeServices.Models.ViewModels.Internal.Permits;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public static List<Payments> PaymentsByPermitsID(int BldPermitId)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldPayment> _BldPayment = db.BldPayment.Where(x => x.BldPermitId == BldPermitId).OrderByDescending(x => x.PaymentID).ToList();
                List<Payments> _Payments = Mapper.Map<List<BldPayment>, List<Payments>>(_BldPayment);
                return _Payments;
            }

        }
        #endregion

        #region Method :: List Payments By Supervision ID

        public static List<Payments> PaymentsBySupervisionID(int BldSupervisionId)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldPayment> _BldPayment = db.BldPayment.Where(x => x.SupervisionID == BldSupervisionId).OrderByDescending(x => x.PaymentID).ToList();
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

        #region Method :: List Payment Details By Supervision ID

        public static List<PaymentDetails> MapsPaymentDetailsBySupervisionID(int BldSupervisionID)
        {
            List<Payments> paymentList = PaymentsBySupervisionID(BldSupervisionID);
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

        #region Method ::  Payment ID By Token ID

        public static int PaymentIDByToken(string token)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                int _SupervisionID = 0;
                BldPayment _BldPayment = db.BldPayment.Where(x => x.TokenID == token).SingleOrDefault();
                if (_BldPayment.SupervisionID != null)
                {
                    _SupervisionID = (int)_BldPayment.SupervisionID;
                }
                return _SupervisionID;
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


        #region Method ::  Payment Details By Payment ID

        public static List<PaymentDetails> PaymentDetailsByPaymentID(int ID)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldPaymentDetails> _BldPaymentDetails = db.BldPaymentDetails.Where(x => x.PaymentID == ID).OrderByDescending(x => x.PaymentDetailID).ToList();
                List<PaymentDetails> _PaymentDetails = Mapper.Map<List<BldPaymentDetails>, List<PaymentDetails>>(_BldPaymentDetails);
                return _PaymentDetails;
            }

        }
        #endregion
        #region Method ::  Save Payment Details
        public static string SavePaymentDetailsForPermit(PermitsViewModel oModel)
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
                    int paymentType = 0;
                    if (oModel.PaymentDetailsList.Count > 0)
                    {
                        foreach (var item in oModel.PaymentDetailsList)
                        {
                            int serviceType = (int)DMeServices.Models.Common.BuildingServices.ServiceFeesCom.TypeByID((int)item.ServiceID).ServiceType;

                            if (serviceType == 3) //3 is for insurance merchant (source type) only
                            {
                                paymentType = 3;
                            }
                            else
                            {
                                paymentType = 1;
                            }
                        }
                    }
                    _BldPayment = Mapper.Map<Payments, BldPayment>(oModel.Payment);
                    _BldPayment.BldPermitId = oModel.BuildingPermits.Id;
                    _BldPayment.PaymentTotalAmount = oModel.Payment.PaymentTotalAmount;
                    _BldPayment.FeesDate = DateTime.Now;
                    _BldPayment.PaymentType = paymentType;
                    if(oModel.Payment.PaymentTotalAmount == 0)
                    {
                        _BldPayment.PaymentStatus = 1;
                    }
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

        public static string SavePaymentDetailsForSupervision(SupervisionViewModel oModel)
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
                    int paymentType = 0;
                    if (oModel.PaymentDetailsList.Count > 0)
                    {
                        foreach (var item in oModel.PaymentDetailsList)
                        {
                            int serviceType = (int)DMeServices.Models.Common.BuildingServices.ServiceFeesCom.TypeByID((int)item.ServiceID).ServiceType;

                            if (serviceType == 3) //3 is for insurance merchant (source type) only
                            {
                                paymentType = 3;
                            }
                            else
                            {
                                paymentType = 1;
                            }
                        }
                    }
                    _BldPayment = Mapper.Map<Payments, BldPayment>(oModel.Payment);
                    _BldPayment.BldPermitId = oModel.BuildingSupervision.BldPermitID;
                    _BldPayment.SupervisionID = oModel.BuildingSupervision.ID;
                    _BldPayment.PaymentTotalAmount = oModel.Payment.PaymentTotalAmount;
                    _BldPayment.FeesDate = DateTime.Now;
                    _BldPayment.PaymentType = paymentType;
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

        public static string UpdatPaymentStatus(string t, BankResponse result)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldPayment _BldPayment = db.BldPayment.Where(x => x.TokenID == t).SingleOrDefault();


                _BldPayment.BankResponseID = Convert.ToInt32(result.statusDetails.Bankresponseid);
                _BldPayment.Bankpaymentid = result.statusDetails.Bankpaymentid;
                _BldPayment.PaymentDate = DateTime.ParseExact(result.statusDetails.Postdate.ToString(), "yyyyMMddHHmm", CultureInfo.InvariantCulture);
                _BldPayment.Referenceid = result.statusDetails.Referenceid;
                _BldPayment.Paymentrequestid = (int)result.statusDetails.Paymentrequestid;
                _BldPayment.Transactionid = result.statusDetails.Transactionid;
                //_BldPayment.PaymentType = (int)result.statusDetails.Paymenttypeid;
                _BldPayment.PaymentStatus = (int)result.statusDetails.Paymentstatusid;


                db.SaveChanges();

                return _BldPayment.BldPermitId.ToString();
            }
        }
        #endregion

        #region Method ::  Update Payment Token
        public static string UpdatPaymentToken(int id, string token)
        {

            using (eServicesEntities db = new eServicesEntities())
            {



                BldPayment _BldPayment = db.BldPayment.Where(x => x.PaymentID == id).SingleOrDefault();


                _BldPayment.TokenID = token;


                db.SaveChanges();

                return _BldPayment.PaymentID.ToString();

            }
        }
        #endregion
    }
}
