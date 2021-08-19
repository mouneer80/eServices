using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.BuildingServices
{
   public class Transactions
    {

        public int Id { get; set; }
        public Nullable<int> TypeId { get; set; }
        public Nullable<int> BldPermitId { get; set; }
        public Nullable<int> BldSupervisionId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> BldNewSupervisionID { get; set; }
        public string BldPermitLicenseNo { get; set; }
        public string BldSupervisionLicenseNo { get; set; }

    }
}
