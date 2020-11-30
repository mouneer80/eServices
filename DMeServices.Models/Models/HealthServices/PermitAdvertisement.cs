using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.HealthServices
{
    public class PermitAdvertisement
    {
        public long Permit_Id { get; set; }
        public string PermitTypeName { get; set; }
        public string PermitActivityName { get; set; }
        public string ADV_Location { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime PermitDurationStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime PermitDurationEndtDate { get; set; }
        public string Case { get; set; }
    }
}
