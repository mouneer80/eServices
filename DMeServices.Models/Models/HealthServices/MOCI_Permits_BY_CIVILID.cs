using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Models.HealthServices
{
    public class MOCI_Permits_BY_CIVILID 
    {
        //public int CivilID { get; set; }
        public string MOCI_CIVIL_ID { get; set; }
        public string CommercialNo { get; set; }
        public string MOCI_CommercialName { get; set; }
        public int Permit_Id { get; set; }
        public string Permit_Location { get; set; }
        public int MunicipalLicenseNo { get; set; }
        public string PermitTypeName { get; set; }
        public string PermitActivityName { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime PermitDurationStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime PermitDurationEndtDate { get; set; }
    }
}
