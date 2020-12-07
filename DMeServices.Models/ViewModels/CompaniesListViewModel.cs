using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DMeServices.DAL;
using DMeServices.Models.Common.BuildingServices;

namespace DMeServices.Models.ViewModels
{
    public class CompaniesListViewModel
    {
        public CompaniesListViewModel()
        {
            oUserInfo = new User();
            if (HttpContext.Current.Session["UserInfo"] != null)
            {
                oUserInfo = (User)HttpContext.Current.Session["UserInfo"];
                CompaniesList = MociCompaniesData.CompaniesByOwnerCivilId(oUserInfo.CivilId);
            }
        }

        public User oUserInfo { get; set; }
        public List<MociData> CompaniesList { get; set; }
    }
}
