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
                var userCRNO = UserCom.UserByCivilID(oUserInfo.CivilId);
                if (userCRNO != null)
                {
                    BuildingPermits = new BuildingPermits { ConsultantCivilId = oUserInfo.CivilId, ConsultantCrNo = userCRNO.ConsultantCrNo };
                }
                else
                {
                    BuildingPermits = new BuildingPermits();
                }
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

        public PermitsAttachments OptionalLetter1 { get; set; }
        public PermitsAttachments OptionalLetter2 { get; set; }
        public PermitsAttachments OptionalLetter3 { get; set; }
        public PermitsAttachments OptionalLetter4 { get; set; }
        public PermitsAttachments OptionalLetter5 { get; set; }
        public PermitsAttachments OptionalLetter6 { get; set; }
        public PermitsAttachments OptionalLetter7 { get; set; }
        public PermitsAttachments OptionalLetter8 { get; set; }
        public PermitsAttachments OptionalLetter9 { get; set; }
        public PermitsAttachments OptionalLetter10 { get; set; }
        public PermitsAttachments OptionalLetter11 { get; set; }
        public PermitsAttachments OptionalLetter12 { get; set; }


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

        public Owner Owner { get; set; }
        public List<Owner> ListOfOwners { get; set; }

    }
}
