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
    
    public partial class BldSupervisionContractors
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BldSupervisionContractors()
        {
            this.BldSupervisionServices = new HashSet<BldSupervisionServices>();
        }
    
        public int Id { get; set; }
        public Nullable<int> Cr_No { get; set; }
        public string Cr_Name { get; set; }
        public Nullable<System.DateTime> Cr_Date { get; set; }
        public string LegalForm { get; set; }
        public Nullable<int> PhoneNo { get; set; }
        public string Email { get; set; }
        public string Governorate { get; set; }
        public string State { get; set; }
        public Nullable<int> Owner_Civil_ID { get; set; }
        public string Foreman_Name { get; set; }
        public Nullable<int> Foreman_Civil_ID { get; set; }
        public string OwnerFullName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BldSupervisionServices> BldSupervisionServices { get; set; }
    }
}
