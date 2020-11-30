using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Models.HealthServices
{
    public class MOCI_Permits_BY_MunicipalNo
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
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime PermitDurationStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime PermitDurationEndtDate { get; set; }
        public string Case { get; set; }
        public Nullable<int> Price { get; set; }
    }
}
