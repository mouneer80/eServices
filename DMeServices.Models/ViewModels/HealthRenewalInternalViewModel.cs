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
    public class HealthRenewalInternalViewModel
    {
        public HealthRenewalInternalViewModel()
        {
            oEmployeeInfo = new Employee();
            if (HttpContext.Current.Session["EmployeeInfo"] != null)
            {
                oEmployeeInfo = (Employee)HttpContext.Current.Session["EmployeeInfo"];
                this.LstPermitsInspector = Common.HealthServices.PermitDataCom.PermitsInspector((int)oEmployeeInfo.EMP_NO);
            }

            long MunicipalNo = 0;
            
            this.LstPermitsHeadSection = Common.HealthServices.PermitDataCom.PermitsHeadSection();
            
            this.LstPermitData = Common.HealthServices.PermitDataCom.PermitDataHeadSection(MunicipalNo);

            this.LstPermitsRevenue = Common.HealthServices.PermitDataCom.PermitsRevenue();

            this.LstPermitsFinish = Common.HealthServices.PermitDataCom.PermitsFinished();

            this.LstRevenuePermitData = Common.HealthServices.PermitDataCom.PermitDataRevenue(MunicipalNo);

        }

        public Employee oEmployeeInfo { get; set; }

        public List<MOCI_Permits_HeadSection> LstPermitsHeadSection { get; set; }

        public List<MOCI_Permits_Revenue> LstPermitsRevenue { get; set; }

        public List<MOCI_Permits_Renew_Finish> LstPermitsFinish { get; set; }

        public List<MOCI_Permit_Inspector> LstPermitsInspector { get; set; }

        public List<MOCI_Permits_BY_MunicipalNo_Revenue> LstRevenuePermitData { get; set; }

        public List<MOCI_Permit_BY_MunicipalNo_HeadSection> LstPermitData { get; set; }

        public List<PermitAdvertisement> LstPermitAdvertisement { get; set; }

        public HMociData oMociData { get; set; }

        public List<HPermits> LstHPermits { get; set; }

        public HPermits oPermits { get; set; }

        public PermitActivity PermitActivity { get; set; }
        
        public PermitDuration PermitDuration { get; set; }

        public PermitSubActivity PermitSubActivity { get; set; }

        public PermitType PermitType { get; set; }

        public PermitsInspection PermitsInspection { get; set; }

        public ISIC4_LEVEL_5s ActivityValue { get; set; }

    }
}
