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
    
    public partial class GetPermitsDurationRequests_Result
    {
        public long PermitDurationId { get; set; }
        public Nullable<System.DateTime> ContractStartDate { get; set; }
        public Nullable<System.DateTime> ContractEndDate { get; set; }
        public string ContractNo { get; set; }
        public Nullable<decimal> ContractMonthlyFees { get; set; }
        public string AttachmentName { get; set; }
        public Nullable<int> PermitDurationStatus { get; set; }
        public Nullable<int> InspectorNo { get; set; }
        public Nullable<System.DateTime> InspectionDate { get; set; }
        public string HeadSectionNotes { get; set; }
        public Nullable<System.DateTime> RequestDate { get; set; }
    }
}
