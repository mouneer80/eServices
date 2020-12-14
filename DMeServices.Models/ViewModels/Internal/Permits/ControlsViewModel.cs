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
    public class PermitsViewModel
    {
        public PermitsViewModel()
        {
            oEmployeeInfo = new Employee();
            if (HttpContext.Current.Session["EmployeeInfo"] != null)
            {
                oEmployeeInfo = (Employee)HttpContext.Current.Session["EmployeeInfo"];


                if (oEmployeeInfo.IsEngineerHead)
                {
                    ListBuildingPermits = PermitsCom.GetAllPermitsByflowStatus(8);
                }
                else
                {
                    ListBuildingPermits = PermitsCom.PermitsByEngineerID(oEmployeeInfo.EMP_NO);
                }
            }
        }

        public Employee oEmployeeInfo { get; set; }

        public BuildingPermits BuildingPermits { get; set; }

        public List<BuildingPermits> ListBuildingPermits { get; set; }

        public List<PermitsAttachmentDetails> ListOfAttachmentDetails { get; set; }

        public PermitsAttachmentDetails AttachmentDetails { get; set; }

        public List<PermitsAttachments> ListOfAttachments { get; set; }

        public PermitsAttachments Attachments { get; set; }

        public PermitsAttachments PersonalCard { get; set; }

        public PermitsAttachments KrokeFile { get; set; }

        public PermitsAttachments OwnerFile { get; set; }

        public Welyat ListOfWelayat { get; set; }
        public Regions ListOfRegions { get; set; }
        public BuildingTypes ListOfBuildingTypes { get; set; }
        public LandUseTypes ListOfLandUseTypes { get; set; }
        public SquareLetters ListOfSquareLetters { get; set; }


    }
}
