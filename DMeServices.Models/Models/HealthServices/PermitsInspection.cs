using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.HealthServices
{
    public class PermitsInspection
    {
        public Nullable<int> InspectorNo { get; set; }
        public Nullable<System.DateTime> InspectionDate { get; set; }
        //[Required(ErrorMessage = "حقل مطلوب")]
        public string InpectionNote { get; set; }
        public Nullable<long> HsPermitId { get; set; }
        public long Id { get; set; }
        public Nullable<long> PermitDurationId { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        public Nullable<int> Status { get; set; }

        public virtual PermitDuration HsPermitDuration { get; set; }
    }
}
