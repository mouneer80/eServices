using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Models.HealthServices
{
    public class MOCI_Permits_Renew_Finish
    {
        public Nullable<long> CommercialNo { get; set; }
        public string MOCI_CommercialName { get; set; }
        public long Permit_Id { get; set; }
        public string Permit_Location { get; set; }
        public Nullable<long> MunicipalLicenseNo { get; set; }
        public string PermitTypeName { get; set; }
        public string PermitActivityName { get; set; }
        public long PermitDurationId { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime ContractStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime ContractEndDate { get; set; }
        public string ContractNo { get; set; }
        public Nullable<decimal> ContractMonthlyFees { get; set; }
        public string AttachmentName { get; set; }
        public Nullable<int> PermitDurationStatus { get; set; }
        public Nullable<int> InspectorNo { get; set; }
        public Nullable<System.DateTime> InspectionDate { get; set; }
        public string HeadSectionNotes { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime RequestDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime InspectorDate { get; set; }
        public string InspectorNotes { get; set; }
        public string InspectorName { get; set; }
    }
}
