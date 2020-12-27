using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.BuildingServices
{
    public class PaymentDetails
    {
        public int PaymentDetailID { get; set; }
        public Nullable<int> ServiceID { get; set; }
        public Nullable<int> ServiceQuantity { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> ServiceFees { get; set; }
        public string Notes { get; set; }
        public Nullable<int> PaymentID { get; set; }

        public ServiceFeesDetails ServiceFeesItem { get; set; }
        public List<ServiceFeesDetails> ServiceFeesItemsList { get; set; }
    }
}
