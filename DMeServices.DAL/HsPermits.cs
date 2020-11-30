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
    
    public partial class HsPermits
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HsPermits()
        {
            this.HsPermitAdvertisement = new HashSet<HsPermitAdvertisement>();
            this.HsPermitDuration = new HashSet<HsPermitDuration>();
        }
    
        public Nullable<System.DateTime> RequestDate { get; set; }
        public Nullable<int> PermitTypeId { get; set; }
        public Nullable<long> CommercialNo { get; set; }
        public Nullable<int> ActivityTypeId { get; set; }
        public string Location { get; set; }
        public string StreetName { get; set; }
        public Nullable<int> SubActivityId { get; set; }
        public Nullable<long> MunicipalLicenseNo { get; set; }
        public long Id { get; set; }
        public string TransNo { get; set; }
        public Nullable<int> StateId { get; set; }
        public string landlordName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HsPermitAdvertisement> HsPermitAdvertisement { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HsPermitDuration> HsPermitDuration { get; set; }
    }
}