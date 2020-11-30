using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.HealthServices
{
 public class HMociData
    {
        public Nullable<long> COMMERCIAL_NO { get; set; }
        public string COMMERCIAL_NAME { get; set; }
        public string COMMERCIAL_DATE { get; set; }
        public string LEGAL_FORM { get; set; }
        public Nullable<int> PHONE_NO { get; set; }
        public string EMAIL { get; set; }
        public string GOVERNORATE { get; set; }
        public string STATE { get; set; }
        public string FULL_NAME { get; set; }
        public string CIVIL_ID { get; set; }
        public long ID { get; set; }
    }
}
