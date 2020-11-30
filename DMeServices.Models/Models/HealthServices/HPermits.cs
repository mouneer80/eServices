using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.HealthServices
{
  public  class HPermits
    {
        public Nullable<System.DateTime> RequestDate { get; set; }
        public Nullable<int> PermitTypeId { get; set; }
        public Nullable<long> CommercialNo { get; set; }
        public Nullable<int> ActivityTypeId { get; set; }
        public string Location { get; set; }
        public string StreetName { get; set; }
        public Nullable<int> SubActivityId { get; set; }
        public Nullable<long> MunicipalLicenseNo { get; set; }
        public long Id { get; set; }
        public string TransNo { get; set; }
        public Nullable<int> StateId { get; set; }
        public string landlordName { get; set; }
    }

}
