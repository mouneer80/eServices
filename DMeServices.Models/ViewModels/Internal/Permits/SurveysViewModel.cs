
using DMeServices.Models.BuildingServices;
using DMeServices.Models.Common;
using DMeServices.Models.Common.BuildingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DMeServices.Models.ViewModels.Internal.Permits
{
    public class SurveysViewModel
    {
        public SurveysViewModel()
        {
            oUserInfo = new User();
            if (HttpContext.Current.Session["UserInfo"] != null)
            {
                
                oUserInfo = (User)HttpContext.Current.Session["UserInfo"];

                LandSurveys = new LandSurvey { ConsultantCivilId = oUserInfo.CivilId };
                
                //oEmployeeInfo = new Employee();
                //if (HttpContext.Current.Session["EmployeeInfo"] != null)
                //{
                //    oEmployeeInfo = (Employee)HttpContext.Current.Session["EmployeeInfo"];


                //    if (oEmployeeInfo.IsEngineerHead)
                //    {
                // ListLandSurveys = LandSurveyCom.AllLandSurvey();
                //}
                //else
                //{
                //ListBuildingPermits = PermitsCom.PermitsByEngineerID(oEmployeeInfo.EMP_NO);
                //}
            }
        }

        public User oUserInfo { get; set; }

        public LandSurvey LandSurveys { get; set; }
        public List<LandSurvey> ListLandSurveys { get; set; }
        public Welyat ListOfWelayat { get; set; }
        public Regions ListOfRegions { get; set; }

        public WorkersHousingDetails ListOfWorkers { get; set; }


    }
}
