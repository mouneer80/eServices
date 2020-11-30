using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.HealthServices
{
  public  class PermitSubActivity
    {
        public Nullable<int> ActivityCode { get; set; }
        public Nullable<int> SubCode { get; set; }
        public string SubName { get; set; }
        public Nullable<int> Price { get; set; }
        public int Id { get; set; }
        public Nullable<int> PermitTypeId { get; set; }
    }
}
