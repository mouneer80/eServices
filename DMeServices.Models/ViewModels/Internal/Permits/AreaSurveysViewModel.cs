
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
    public class AreaSurveysViewModel
    {
        public AreaSurveysViewModel()
        {
            //oEmployeeInfo = new Employee();
            //if (HttpContext.Current.Session["EmployeeInfo"] != null)
            //{
            //    oEmployeeInfo = (Employee)HttpContext.Current.Session["EmployeeInfo"];


            //    if (oEmployeeInfo.IsEngineerHead)
            //    {
            ListAreaSurveys = LandSurveyCom.AllAreaSurvey();
                //}
                //else
                //{
                    //ListBuildingPermits = PermitsCom.PermitsByEngineerID(oEmployeeInfo.EMP_NO);
                //}
        }

        public AreaSurvey AreaSurveys { get; set; }
        public List<AreaSurvey> ListAreaSurveys { get; set; }
        public Welyat ListOfWelayat { get; set; }
        public Regions ListOfRegions { get; set; }


    }
}
