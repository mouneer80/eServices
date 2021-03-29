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

    
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> CivilID { get; set; }
        public Nullable<int> Phone { get; set; }
        public Nullable<int> BldPermitId { get; set; }
    }
}
