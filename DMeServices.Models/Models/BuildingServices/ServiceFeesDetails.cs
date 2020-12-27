using DMeServices.Models.Configrations;
using DMeServices.Models.MetaData.BuildingServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DMeServices.Models.BuildingServices
{ 
    public class ServiceFeesDetails
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public Nullable<int> ServiceType { get; set; }
        public Nullable<decimal> ServiceFees { get; set; }
        public string ServiceUnit { get; set; }
        public Nullable<int> SeviceFinancialItem { get; set; }
    }
}
