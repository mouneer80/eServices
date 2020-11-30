using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DMeServices.Models.HealthServices;
using DMeServices.Models.Models.HealthServices;

namespace DMeServices.Models.ViewModels
{
    public class HealthRenewalViewModel
    {
        public User oUserInfo { get; set; }

        public HealthRenewalViewModel()
        {
            oUserInfo = new User();
            oUserInfo = (User)HttpContext.Current.Session["UserInfo"];

            if (HttpContext.Current.Session["UserInfo"] != null)
            {
                this.LstMociData = Common.HealthServices.MociDataCom.MociDataByCivilID(oUserInfo.CivilId);
            }

            long MunicipalNo = 0;

            this.LstPermitData = Common.HealthServices.PermitDataCom.PermitsBYMunicipalNo(MunicipalNo, oUserInfo.CivilId);

            this.LstPermitAdvertisement = Common.HealthServices.PermitAdvertisementCom.PermitAdvertisement(MunicipalNo);
        }
        
        public List<MOCI_Permits_BY_CIVILID> LstMociData { get; set; }

        public List<MOCI_Permits_BY_MunicipalNo> LstPermitData { get; set; }

        public List<PermitAdvertisement> LstPermitAdvertisement { get; set; }

        public HMociData oMociData { get; set; }

        public List<HPermits> LstHPermits { get; set; }

        public HPermits oPermits { get; set; }

        public PermitActivity PermitActivity { get; set; }
        
        public PermitDuration PermitDuration { get; set; }

        public PermitSubActivity PermitSubActivity { get; set; }

        public PermitType PermitType { get; set; }

        public List<ContractAttachments> ListOfAttachments { get; set; }

        public ContractAttachments PersonalCard { get; set; }

        public ContractAttachments OwnerContract { get; set; }

        public ContractAttachments ContractFile { get; set; }

    }
}
