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
        public string BankResponse { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }


        public PaymentDetails PaymentDetails { get; set; }
        public List<PaymentDetails> PaymentDetailsList { get; set; }
    }
}
