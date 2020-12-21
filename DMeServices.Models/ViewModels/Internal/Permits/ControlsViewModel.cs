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
    public class ControlsViewModel
    {
        public ControlsViewModel()
        {
            oEmployeeInfo = new Employee();
            if (HttpContext.Current.Session["EmployeeInfo"] != null)
            {
                oEmployeeInfo = (Employee)HttpContext.Current.Session["EmployeeInfo"];

                if (oEmployeeInfo.IsControlHead)
                {
                    ListBuildingControls = ControlsCom.GetAllControlsByFlowStatus(8);
                }
                else
                {
                    ListBuildingControls = ControlsCom.ControlsByEngineerId(oEmployeeInfo.EMP_NO);
                }
            }
        }

        public Employee oEmployeeInfo { get; set; }

        public BuildingControls BuildingControls { get; set; }

        public List<BuildingControls> ListBuildingControls { get; set; }

        public ControlServicesTypes ServiceType { get; set; }

        public List<ControlServicesTypes> ServiceTypesList { get; set; }

        public BuildingPermits BuildingPermits { get; set; }

        public List<BuildingPermits> BuildingPermitsList { get; set; }

        public List<PermitsAttachments> ListOfAttachments { get; set; }

        public PermitsAttachments Attachments { get; set; }
    }
}
