using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Models.HealthServices
{
    public class ISIC4_LEVEL_5s
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Value { get; set; }
        public Nullable<int> Level4Id { get; set; }
    }
}
