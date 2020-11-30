using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Revenue
{
    public class RevMainPaymentLog
    {
        public long Id { get; set; }
        public Nullable<int> DeptCode { get; set; }
        public Nullable<long> PaymentCode { get; set; }
        public Nullable<long> FinancialItemId { get; set; }
        public Nullable<long> FinancialSubItemId { get; set; }
        public string PayerName { get; set; }
        public Nullable<int> WelyatCode { get; set; }
        public Nullable<int> LocationCode { get; set; }
        public string PaymentType { get; set; }
        public string PaymentCheqNo { get; set; }
        public string PaymentBankCode { get; set; }
        public Nullable<System.DateTime> PaymentCheqDate { get; set; }
        public Nullable<decimal> TransAmount { get; set; }
        public Nullable<decimal> PaymentAmount { get; set; }
        public string PaymentAmountInWords { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string PaymentCashierCode { get; set; }
        public Nullable<int> MofeNo { get; set; }
        public string InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> MofeDate { get; set; }
        public string DeptBankCode { get; set; }
        public Nullable<System.DateTime> DeptDate { get; set; }
        public string PaymentCheqName { get; set; }
        public string CreditCardNo { get; set; }
        public Nullable<int> FinancialYear { get; set; }
        public Nullable<int> UserCode { get; set; }
        public Nullable<int> SiteCd { get; set; }
        public string EventName { get; set; }
        public Nullable<System.DateTime> EventDate { get; set; }
    }
}
