using DMeServices.Models.BuildingServices;
using DMeServices.Models.Common;
using DMeServices.Models.Common.BuildingServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DMeServices.Models.ViewModels.Permits
{
    public class PermitsViewModel
    {
        public PermitsViewModel()
        {
            oUserInfo = new User();
            if (HttpContext.Current.Session["UserInfo"] != null)
            {
                oUserInfo = (User)HttpContext.Current.Session["UserInfo"];
                long userCRNO = (long)UserCom.UserByCivilID(oUserInfo.CivilId).ConsultantCrNo;
                BuildingPermits = new BuildingPermits { ConsultantCivilId = oUserInfo.CivilId, ConsultantCrNo = userCRNO };
            }

        }

        public User oUserInfo { get; set; }

        public BuildingPermits BuildingPermits { get; set; }

        public List<BuildingPermits> ListBuildingPermits { get; set; }

        public List<PermitsAttachments> ListOfAttachments { get; set; }

        public PermitsAttachments Attachments { get; set; }

        public PermitsAttachments PersonalCard { get; set; }

        public PermitsAttachments KrokeFile { get; set; }

        public PermitsAttachments OwnerFile { get; set; }

        public PermitsAttachments ConsLetter { get; set; }

        public PermitsAttachments ConsAgreementFile { get; set; }

        public PermitsAttachments LandPic { get; set; }

        public PermitsAttachments Others { get; set; }

        public PermitsAttachmentDetails AttachmentDetails { get; set; }

        public List<PermitsAttachmentDetails> ListOfAttachmentDetails { get; set; }
        public Welyat ListOfWelayat { get; set; }
        public Regions ListOfRegions { get; set; }
        public BuildingTypes ListOfBuildingTypes { get; set; }
        public LandUseTypes ListOfLandUseTypes { get; set; }
        public SquareLetters ListOfSquareLetters { get; set; }
        public int WID { get; set; }
        public int RID { get; set; }
        public int LID { get; set; }
        public int BID { get; set; }
        public int SID { get; set; }

        public bool ShowAdd { get; set; }

        public int? Quantity { get; set; }
        public int ServiceID { get; set; }
        public decimal? Fees { get; set; }

        public decimal? Total { get; set; }

        public decimal? TempGrandTotal { get; set; }



        public PaymentDetails PaymentDetails { get; set; }
        public List<PaymentDetails> PaymentDetailsList { get; set; }
        public ServiceFeesDetails ServiceFeesDetails { get; set; }
        public Payments Payment { get; set; }
        public List<Payments> Payments { get; set; }

    }
}
