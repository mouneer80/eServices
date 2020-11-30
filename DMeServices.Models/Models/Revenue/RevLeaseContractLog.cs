using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Revenue
{
    public class RevLeaseContractLog
    {
        public long Id { get; set; }
        public Nullable<int> DeptCode { get; set; }
        public Nullable<int> PaymentCode { get; set; }
        public Nullable<long> FinancialItemId { get; set; }
        public Nullable<long> FinancialSubItemId { get; set; }
        public Nullable<int> ContractNo { get; set; }
        public string OwnerName { get; set; }
        public string BeneficiaryName { get; set; }
        public string OwnerCivilId { get; set; }
        public string BeneficiaryCivilId { get; set; }
        public string RegistrationNo { get; set; }
        public Nullable<int> LocationCode { get; set; }
        public Nullable<int> Period { get; set; }
        public Nullable<decimal> MonthlyFee { get; set; }
        public Nullable<decimal> YearlyFee { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> ContractType { get; set; }
        public string OwnerAdress { get; set; }
        public Nullable<int> BeneficiaryCountry { get; set; }
        public string BeneficiaryAddress { get; set; }
        public Nullable<int> FinancialYear { get; set; }
        public string Location { get; set; }
        public Nullable<int> SiteCd { get; set; }
        public string StreetName { get; set; }
        public Nullable<int> PlotNo { get; set; }
        public string Block { get; set; }
        public Nullable<int> BuildingNo { get; set; }
        public Nullable<int> AlleyNo { get; set; }
        public Nullable<int> FlatNo { get; set; }
        public string TypeofUse { get; set; }
        public string EventName { get; set; }
        public Nullable<System.DateTime> EventDate { get; set; }
        public Nullable<long> MainPaymentsId { get; set; }
        public string ElectricNo { get; set; }
        public Nullable<long> CommericalNo { get; set; }
        public Nullable<int> TotalYears { get; set; }
        public Nullable<int> TotalMonths { get; set; }
        public Nullable<int> TotalDays { get; set; }
    }
}
