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
    public class ControlsViewModel
    {
        public ControlsViewModel()
        {
            oUserInfo = new User();
            if (HttpContext.Current.Session["UserInfo"] != null)
            {
                oUserInfo = (User)HttpContext.Current.Session["UserInfo"];
                BuildingControls = new BuildingControls { OwnerCivilId  = oUserInfo.CivilId };
            }

        }

        public User oUserInfo { get; set; }

        public BuildingControls BuildingControls { get; set; }

        public List<BuildingControls> ListBuildingControls { get; set; }

        public ControlServicesTypes ServiceType { get; set; }

        public List<ControlServicesTypes> ServiceTypesList { get; set; }

        public BuildingPermits BuildingPermits { get; set; }

        public List<BuildingPermits> BuildingPermitsList { get; set; }
        
        public List<PermitsAttachments> ListOfAttachments { get; set; }

        public PermitsAttachments Attachments { get; set; }

        public PermitsAttachments PersonalCard { get; set; }

        public PermitsAttachments KrokeFile { get; set; }

        public PermitsAttachments OwnerFile { get; set; }
        public bool ShowAdd { get; set; }
    }
}
