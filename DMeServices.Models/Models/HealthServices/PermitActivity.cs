using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.HealthServices
{
   public class PermitActivity
    {
        public int ActivityCode { get; set; }
        public string Name { get; set; }
        public int PermitTypeId { get; set; }
        public Nullable<int> Price { get; set; }
        public int Id { get; set; }
    }
}
