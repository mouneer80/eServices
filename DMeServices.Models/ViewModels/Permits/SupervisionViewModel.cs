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
    public class SupervisionViewModel
    {
        public SupervisionViewModel()
        {
            oUserInfo = new User();
            if (HttpContext.Current.Session["UserInfo"] != null)
            {
                oUserInfo = (User)HttpContext.Current.Session["UserInfo"];
                BuildingSupervision = new BuildingSupervision { OwnerCivilId  = oUserInfo.CivilId };
            }

        }

        public User oUserInfo { get; set; }

        public BuildingSupervision BuildingSupervision { get; set; }

        public List<BuildingSupervision> ListBuildingSupervision { get; set; }

        public SupervisionServicesTypes ServiceType { get; set; }

        public List<SupervisionServicesTypes> ServiceTypesList { get; set; }

        public BuildingPermits BuildingPermits { get; set; }

        public List<BuildingPermits> BuildingPermitsList { get; set; }
        
        public List<PermitsAttachments> ListOfAttachments { get; set; }

        public PermitsAttachments Attachments { get; set; }

        public PermitsAttachments ContractorOwnerPersonalCard { get; set; }

        public PermitsAttachments ContractorCRFile { get; set; }

        public PermitsAttachments ConsultantCRFile { get; set; }

        public PermitsAttachments ForemanPersonalCard { get; set; }

        public PermitsAttachments SupervisionLetter { get; set; }

        public PermitsAttachments SupervisionAgreement { get; set; }

        public PermitsAttachments ContractorCommitments { get; set; }

        public PermitsAttachments ConsultantCommitments { get; set; }

        public PermitsAttachments PlotMarksForm { get; set; }

        public PermitsAttachments ProjectBoardForm { get; set; }

        public PermitsAttachments ConstructionPermitApplication { get; set; }


        public bool ShowAdd { get; set; }
    }
}
