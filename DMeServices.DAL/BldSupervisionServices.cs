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
    
    public partial class BldSupervisionServices
    {
        public int ID { get; set; }
        public int BldPermitID { get; set; }
        public int ServiceTypeID { get; set; }
        public Nullable<int> Status { get; set; }
        public int OwnerCivilId { get; set; }
        public string KrokiNO { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhoneNo { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string LicenseNo { get; set; }
        public Nullable<int> DmEngineerNo { get; set; }
        public Nullable<int> ConsultantCivilId { get; set; }
        public System.DateTime RequestDate { get; set; }
        public string TransactNo { get; set; }
        public string DmFileNumber { get; set; }
        public string DmSupervisionComments { get; set; }
        public string DmInspectorComments { get; set; }
        public Nullable<int> PaymentID { get; set; }
    
        public virtual BldSupervisionServicesTypes BldSupervisionServicesTypes { get; set; }
        public virtual BldPermits BldPermits { get; set; }
    }
}
