using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMeServices.DAL;
using DMeServices.Models.Common;
using DMeServices.Models.Common.BuildingServices;

namespace DMeServices.Models.ViewModels
{
    public class CompanyViewModel
    { 
        public MociData CompanyData { get; set; }
        public List<User> ConsultantsList { get; set; }
        public User Consultant { get; set; }

        public bool ShowMessage { get; set; }
        public string ReturnedMessage { get; set; }
    }
}
