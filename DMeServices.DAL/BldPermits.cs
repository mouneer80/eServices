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
    
    public partial class BldPermits
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BldPermits()
        {
            this.BldPermitsAttachments = new HashSet<BldPermitsAttachments>();
            this.BldControlServices = new HashSet<BldControlServices>();
            this.BldPayment = new HashSet<BldPayment>();
        }
    
        public long Id { get; set; }
        public string TransactNo { get; set; }
        public string OwnerName { get; set; }
        public Nullable<long> OwnerCivilId { get; set; }
        public string OwnerPhoneNo { get; set; }
        public string DistrictName { get; set; }
        public Nullable<int> DistrictNo { get; set; }
        public string ConstructedArea { get; set; }
        public string NumberOfFloors { get; set; }
        public long ConsultantCrNo { get; set; }
        public string ConstructionType { get; set; }
        public System.DateTime RequestDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string SquareArea { get; set; }
        public string LicenseNo { get; set; }
        public string PaymentReceiptNo { get; set; }
        public Nullable<decimal> PaymentAmount { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public Nullable<int> WorkflowStatus { get; set; }
        public Nullable<int> DmEngineerNo { get; set; }
        public string DmEngineerComments { get; set; }
        public Nullable<long> ConsultantCivilId { get; set; }
        public Nullable<int> WelayahID { get; set; }
        public Nullable<int> RegionID { get; set; }
        public Nullable<int> SquareLetterID { get; set; }
        public Nullable<int> UseTypeID { get; set; }
        public Nullable<int> BuildingTypeID { get; set; }
        public string KrokiNO { get; set; }
        public string DmFileNumber { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BldPermitsAttachments> BldPermitsAttachments { get; set; }
        public virtual BldBuildingTypes BldBuildingTypes { get; set; }
        public virtual BldLandUseTypes BldLandUseTypes { get; set; }
        public virtual BldRegions BldRegions { get; set; }
        public virtual BldSquareLetters BldSquareLetters { get; set; }
        public virtual BldWelyat BldWelyat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BldControlServices> BldControlServices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BldPayment> BldPayment { get; set; }
    }
}
