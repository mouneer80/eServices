using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.BuildingServices
{ 
    public class Payments
    {
        public int PaymentID { get; set; }
        public Nullable<long> BldPermitId { get; set; }
        public Nullable<decimal> PaymentTotalAmount { get; set; }
        public string TokenID { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public Nullable<System.DateTime> FeesDate { get; set; }
        public Nullable<int> PaymentType { get; set; }
        public string Transactionid { get; set; }
        public Nullable<int> Paymentrequestid { get; set; }
        public string Referenceid { get; set; }
        public string Bankpaymentid { get; set; }
        public Nullable<int> PaymentStatus { get; set; }
        public Nullable<int> BankResponseID { get; set; }

        public PaymentDetails PaymentDetails { get; set; }
        public List<PaymentDetails> PaymentDetailsList { get; set; }
    }
}
