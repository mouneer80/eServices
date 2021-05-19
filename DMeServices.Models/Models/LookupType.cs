using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models
{
  public  class LookupType
    {


        public int LookupId { get; set; }
        public string LookupNameAr { get; set; }
        public string LookupNameEn { get; set; }
        public string LookupParentDesc { get; set; }
        public int LookupParentId { get; set; }
        public string CssClassName { get; set; }

    }
}
