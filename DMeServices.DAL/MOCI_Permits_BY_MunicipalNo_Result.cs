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
    
    public partial class MOCI_Permits_BY_MunicipalNo_Result
    {
        public string MOCI_CIVIL_ID { get; set; }
        public Nullable<long> CommercialNo { get; set; }
        public string MOCI_CommercialName { get; set; }
        public string landlordName { get; set; }
        public long Permit_Id { get; set; }
        public string Permit_Location { get; set; }
        public Nullable<long> MunicipalLicenseNo { get; set; }
        public string StreetName { get; set; }
        public long Id { get; set; }
        public Nullable<int> PermitTypeId { get; set; }
        public Nullable<int> ActivityTypeId { get; set; }
        public Nullable<int> SubActivityId { get; set; }
        public string StateName { get; set; }
        public string PermitTypeName { get; set; }
        public string PermitActivityName { get; set; }
        public Nullable<System.DateTime> PermitDurationStartDate { get; set; }
        public Nullable<System.DateTime> PermitDurationEndtDate { get; set; }
        public string Case { get; set; }
        public Nullable<int> Price { get; set; }
    }
}