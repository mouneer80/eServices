using DMeServices.Models.BuildingServices;
using DMeServices.Models.Common;
using DMeServices.Models.Common.BuildingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DMeServices.Models.ViewModels.Permits
{
   public class AreaSurveysViewModel
    {
        public AreaSurveysViewModel()
        {
         //oUserInfo = new User();
         //   if (HttpContext.Current.Session["UserInfo"] != null)
         //   {
         //       oUserInfo = (User)HttpContext.Current.Session["UserInfo"];

         //      //LandSurvey = new LandSurvey { ConsultantCivilId = oUserInfo.CivilId };

         //   }
   
        }

        //public User oUserInfo { get; set; }

        public AreaSurvey AreaSurvey { get; set; }

        public List<AreaSurvey> ListAreaSurveys { get; set; }


    }
}
