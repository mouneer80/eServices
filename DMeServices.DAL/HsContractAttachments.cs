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
    
    public partial class HsContractAttachments
    {
        public long Id { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentContentType { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public string AttachmentPath { get; set; }
        public string Description { get; set; }
        public byte[] AttachmentStream { get; set; }
        public Nullable<long> HsPermitDurationId { get; set; }
    
        public virtual HsPermitDuration HsPermitDuration { get; set; }
    }
}