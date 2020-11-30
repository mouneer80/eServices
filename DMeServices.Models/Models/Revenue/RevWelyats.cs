using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Revenue
{
    public class RevWelyats
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string CodeChar { get; set; }
        public Nullable<int> SiteCd { get; set; }
    }
}
