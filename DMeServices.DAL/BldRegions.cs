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
    
    public partial class BldRegions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BldRegions()
        {
            this.BldLandSurvey = new HashSet<BldLandSurvey>();
            this.BldIndustrialAreasSurvey = new HashSet<BldIndustrialAreasSurvey>();
            this.BldPermits = new HashSet<BldPermits>();
        }
    
        public int RegionID { get; set; }
        public int RegionCode { get; set; }
        public string RegionEnName { get; set; }
        public string RegionArName { get; set; }
        public int WelyahID { get; set; }
    
        public virtual BldWelyat BldWelyat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BldLandSurvey> BldLandSurvey { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BldIndustrialAreasSurvey> BldIndustrialAreasSurvey { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BldPermits> BldPermits { get; set; }
    }
}