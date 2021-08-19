using AutoMapper;
using DMeServices.DAL;
using DMeServices.Models.BuildingServices;
using DMeServices.Models.ViewModels.Internal.Permits;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;


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

        #region Method ::  Processing of Save Payment Details
        private static string SavePaymentDetailsList(PermitsViewModel oModel, List<PaymentDetails> List, int paymentType)
        {
            decimal total = GetListTotal(List);
            int paymentID = SavePayment(oModel, total, paymentType);
            if (paymentID == -1) return "ERROR";
            SavePaymentDetails(paymentID, List);
            return "DONE";
        }
        private static string SavePaymentDetailsList(SupervisionViewModel oModel, List<PaymentDetails> List, int paymentType)
        {
            decimal total = GetListTotal(List);
            int paymentID = SavePayment(oModel, total, paymentType);
            if (paymentID == -1) return "ERROR";
            SavePaymentDetails(paymentID, List);
            return "DONE";
        }
        private static int SavePayment(PermitsViewModel oModel, decimal total, int PaymentType)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldPayment _BldPayment = new BldPayment();
                try
                {
                    _BldPayment = db.BldPayment.Where(x => x.PaymentID == oModel.Payment.PaymentID).SingleOrDefault();
                    if (_BldPayment != null)
                    {
                        return -1;
                    }
                    
                    _BldPayment = Mapper.Map<Payments, BldPayment>(oModel.Payment);
                    if (total == 0)
                        _BldPayment.Referenceid = "0";
                    _BldPayment.BldPermitId = oModel.BuildingPermits.Id;
                    _BldPayment.PaymentTotalAmount = total;
                    _BldPayment.FeesDate = DateTime.Now;
                    _BldPayment.PaymentType = PaymentType;
                    if (oModel.Payment.PaymentTotalAmount == 0)
                    {
                        _BldPayment.PaymentStatus = 1;
                    }
                    db.BldPayment.Add(_BldPayment);
                    db.SaveChanges();
                    return _BldPayment.PaymentID;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        private static int SavePayment(SupervisionViewModel oModel, decimal total, int PaymentType)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldPayment _BldPayment = new BldPayment();
                try
                {
                    _BldPayment = db.BldPayment.Where(x => x.PaymentID == oModel.Payment.PaymentID).SingleOrDefault();
                    if (_BldPayment != null)
                    {
                        return -1;
                    }
                    _BldPayment = Mapper.Map<Payments, BldPayment>(oModel.Payment);
                    _BldPayment.BldPermitId = oModel.BuildingSupervision.BldPermitID;
                    _BldPayment.SupervisionID = oModel.BuildingSupervision.ID;
                    _BldPayment.PaymentTotalAmount = total;
                    _BldPayment.FeesDate = DateTime.Now;
                    _BldPayment.PaymentType = PaymentType;
                    if (oModel.Payment.PaymentTotalAmount == 0)
                    {
                        _BldPayment.PaymentStatus = 1;
                    }
                    db.BldPayment.Add(_BldPayment);
                    db.SaveChanges();
                    return _BldPayment.PaymentID;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        private static void FillPaymentLists(PermitsViewModel oModel, List<PaymentDetails> insuranceList, List<PaymentDetails> feesList)
        {
            foreach (var item in oModel.PaymentDetailsList)
            {
                int serviceType = (int)DMeServices.Models.Common.BuildingServices.ServiceFeesCom.TypeByID((int)item.ServiceID).ServiceType;

                if (serviceType == 3) //3 is for insurance merchant (source type) only
                {
                    //paymentType = 3;
                    insuranceList.Add(item);
                }
                else
                {
                    //paymentType = 1;
                    feesList.Add(item);
                }
            }
        }
        private static void FillPaymentLists(SupervisionViewModel oModel, List<PaymentDetails> insuranceList, List<PaymentDetails> feesList)
        {
            foreach (var item in oModel.PaymentDetailsList)
            {
                int serviceType = (int)DMeServices.Models.Common.BuildingServices.ServiceFeesCom.TypeByID((int)item.ServiceID).ServiceType;

                if (serviceType == 3) //3 is for insurance merchant (source type) only
                {
                    //paymentType = 3;
                    insuranceList.Add(item);
                }
                else
                {
                    //paymentType = 1;
                    feesList.Add(item);
                }
            }
        }
        private static decimal GetListTotal(List<PaymentDetails> list)
        {
            decimal total = 0;
            foreach (var item in list)
            {
                total += (decimal)item.TotalAmount;
            }
            return total;
        }
        private static void SavePaymentDetails(int paymentID, List<PaymentDetails> List)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldPaymentDetails> LstPaymentDetails = Mapper.Map<List<PaymentDetails>, List<BldPaymentDetails>>(List);
                foreach (var PaymentDetail in LstPaymentDetails)
                {
                    PaymentDetail.PaymentID = paymentID;
                    db.BldPaymentDetails.Add(PaymentDetail);
                    db.SaveChanges();
                }
            }
        }
        #endregion

        #region Method ::  Save Payment Details
        public static string SavePaymentDetailsForPermit(PermitsViewModel oModel)
        {
            List<PaymentDetails> insuranceList = new List<PaymentDetails>();
            List<PaymentDetails> feesList = new List<PaymentDetails>();
            if (oModel.PaymentDetailsList.Count > 0)
                FillPaymentLists(oModel, insuranceList, feesList);
            if (insuranceList.Count > 0)
                SavePaymentDetailsList(oModel, insuranceList, 3);
            if (feesList.Count > 0)
                SavePaymentDetailsList(oModel, feesList, 1);
            return "OK";
        }
        public static string SavePaymentDetailsForSupervision(SupervisionViewModel oModel)
        {
            List<PaymentDetails> insuranceList = new List<PaymentDetails>();
            List<PaymentDetails> feesList = new List<PaymentDetails>();
            if (oModel.PaymentDetailsList.Count > 0)
                FillPaymentLists(oModel, insuranceList, feesList);
            if (insuranceList.Count > 0)
                SavePaymentDetailsList(oModel, insuranceList, 3);
            if (feesList.Count > 0)
                SavePaymentDetailsList(oModel, feesList, 1);
            return "OK";
        }
        #endregion
        #region Method :: Update Payment
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

                BldPayment _RevPayment = db.BldPayment.Where(x => x.TokenID == t).SingleOrDefault();
                SavePaymentInRevenue(_RevPayment);

                return _RevPayment.BldPermitId.ToString();
            }
        }
        #endregion

        #region Method :: Save Revenue items
        public static string TestSaveRevenue(string t)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                BldPayment _BldPayment = db.BldPayment.Where(x => x.TokenID == t).SingleOrDefault();

                SavePaymentInRevenue(_BldPayment);

                return _BldPayment.BldPermitId.ToString();
            }
        }
        private static void SavePaymentInRevenue(BldPayment payment)
        {
            using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["oracleDBRev"].ConnectionString))
            {
                string TransactNo = "";
                RevenueObject revenue = new RevenueObject();
                revenue.APP_TYPE = 3;
                revenue.REFERENCE_ID = payment.Referenceid;
                revenue.REQUEST_NO = payment.PaymentID;
                revenue.RP_PAY_AMT = (decimal)payment.PaymentTotalAmount;
                if (payment.PaymentType == 3)
                {
                    revenue.RP_PAY_CODE = "00000001";
                    if (payment.SupervisionID == null)
                    {
                        TransactNo = PermitsCom.PermitsByID((int)payment.BldPermitId).TransactNo;
                        revenue.RP_PAY_NAME = "تأمين تراخيص البناء-المعاملة رقم:" + TransactNo;
                    }
                    else
                    {
                        TransactNo = SupervisionCom.SupervisionsById((int)payment.SupervisionID).TransactNo;
                        revenue.RP_PAY_NAME = "تأمين رقابة البناء-المعاملة رقم:" + TransactNo;
                    }
                }
                else
                {
                    revenue.RP_PAY_CODE = "11055507";
                    TransactNo = PermitsCom.PermitsByID((int)payment.BldPermitId).TransactNo;
                    revenue.RP_PAY_NAME = "رسوم تراخيص البناء-المعاملة رقم:" + TransactNo;
                }
                revenue.RP_PAY_DATE = (DateTime)payment.PaymentDate;
                int welayahCode = (int)PermitsCom.PermitsByID((int)payment.BldPermitId).WelayahID;
                revenue.RP_PAY_TYPE = "O";
                revenue.RP_YEAR = payment.PaymentDate.Value.Year;
                revenue.RP_WLYT_CODE = PermitsCom.PermitsByID((int)payment.BldPermitId).WelayahID.ToString();
                revenue.TRANSACTION_ID = payment.Transactionid;
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                OracleTransaction oracleTransaction;
                oracleTransaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                command.Transaction = oracleTransaction;
                try
                {
                    //using (OracleDataAdapter dataAdapter = new OracleDataAdapter())
                    //{

                    //command.CommandText = string.Format("Insert into DM_CONTRACT.REVENUE_PAYMENT (RP_PAY_CODE,RP_PAY_NAME,RP_WLYT_CODE,RP_PAY_TYPE,RP_PAY_AMT,RP_PAY_DATE,RP_YEAR,REQUEST_NO,APP_TYPE,REFERENCE_ID,TRANSACTION_ID) values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10})", revenue.RP_PAY_CODE, revenue.RP_PAY_NAME, revenue.RP_WLYT_CODE, revenue.RP_PAY_TYPE, revenue.RP_PAY_AMT, revenue.RP_PAY_DATE, revenue.RP_YEAR, revenue.REQUEST_NO, revenue.APP_TYPE, revenue.REFERENCE_ID, revenue.TRANSACTION_ID);
                    //command.CommandText = $@"Insert into DM_CONTRACT.REVENUE_PAYMENT (RP_PAY_CODE,RP_PAY_NAME,RP_WLYT_CODE,RP_PAY_TYPE,RP_PAY_AMT,RP_PAY_DATE,RP_YEAR,REQUEST_NO,APP_TYPE,REFERENCE_ID,TRANSACTION_ID) values ({revenue.RP_PAY_CODE},{revenue.RP_PAY_NAME},{revenue.RP_WLYT_CODE},{revenue.RP_PAY_TYPE},{revenue.RP_PAY_AMT},TO_DATE({revenue.RP_PAY_DATE}),{revenue.RP_YEAR},{revenue.REQUEST_NO},{revenue.APP_TYPE},{revenue.REFERENCE_ID},{revenue.TRANSACTION_ID});";
                    command.CommandText = @"Insert into DM_CONTRACT.REVENUE_PAYMENT (RP_PAY_CODE,RP_PAY_NAME,RP_WLYT_CODE,RP_PAY_TYPE,RP_PAY_AMT,RP_PAY_DATE,RP_YEAR,REQUEST_NO,APP_TYPE,REFERENCE_ID,TRANSACTION_ID) values (:p_RP_PAY_CODE, :p_RP_PAY_NAME, :p_RP_WLYT_CODE, :p_RP_PAY_TYPE, :p_RP_PAY_AMT, :p_RP_PAY_DATE, :p_RP_YEAR, :p_REQUEST_NO, :p_APP_TYPE, :p_REFERENCE_ID, :p_TRANSACTION_ID)";
                    command.Parameters.Add(":p_RP_PAY_CODE", OracleDbType.Varchar2).Value = revenue.RP_PAY_CODE;
                    command.Parameters.Add(":p_RP_PAY_NAME", OracleDbType.Varchar2).Value = revenue.RP_PAY_NAME;
                    command.Parameters.Add(":p_RP_WLYT_CODE", OracleDbType.Varchar2).Value = revenue.RP_WLYT_CODE;
                    command.Parameters.Add(":p_RP_PAY_TYPE", OracleDbType.Varchar2).Value = revenue.RP_PAY_TYPE;
                    command.Parameters.Add(":p_RP_PAY_AMT", OracleDbType.Decimal).Value = revenue.RP_PAY_AMT;
                    command.Parameters.Add(":p_RP_PAY_DATE", OracleDbType.Date).Value = revenue.RP_PAY_DATE;
                    command.Parameters.Add(":p_RP_YEAR", OracleDbType.Int32).Value = revenue.RP_YEAR;
                    command.Parameters.Add(":p_REQUEST_NO", OracleDbType.Int32).Value = revenue.REQUEST_NO;
                    command.Parameters.Add(":p_APP_TYPE", OracleDbType.Int32).Value = revenue.APP_TYPE;
                    command.Parameters.Add(":p_REFERENCE_ID", OracleDbType.Varchar2).Value = revenue.REFERENCE_ID;
                    command.Parameters.Add(":p_TRANSACTION_ID", OracleDbType.Varchar2).Value = revenue.TRANSACTION_ID;
                    command.ExecuteNonQuery();
                    oracleTransaction.Commit();
                    //}
                }
                catch (Exception ex)
                {
                    oracleTransaction.Rollback();
                    Console.WriteLine(ex.Message.ToString());
                }
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

        #region Class :: Objects
        public class RevenueObject
        {
            public string RP_PAY_CODE { get; set; }
            public string RP_PAY_NAME { get; set; }
            public string RP_WLYT_CODE { get; set; }
            public string RP_PAY_TYPE { get; set; }
            public decimal RP_PAY_AMT { get; set; }
            public DateTime RP_PAY_DATE { get; set; }
            public int RP_YEAR { get; set; }
            public int REQUEST_NO { get; set; }
            public int APP_TYPE { get; set; }
            public string REFERENCE_ID { get; set; }
            public string TRANSACTION_ID { get; set; }
        }
        #endregion
    }
}