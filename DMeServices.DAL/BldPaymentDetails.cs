//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DMeServices.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class BldPaymentDetails
    {
        public int PaymentDetailID { get; set; }
        public Nullable<int> ServiceID { get; set; }
        public Nullable<int> ServiceQuantity { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> ServiceFees { get; set; }
        public string Notes { get; set; }
        public Nullable<int> PaymentID { get; set; }
    
        public virtual BldServiceFees BldServiceFees { get; set; }
        public virtual BldPayment BldPayment { get; set; }
    }
}