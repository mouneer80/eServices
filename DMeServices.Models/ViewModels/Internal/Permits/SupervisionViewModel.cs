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
    public class SupervisionViewModel
    {
        public SupervisionViewModel()
        {
            oEmployeeInfo = new Employee();
            if (HttpContext.Current.Session["EmployeeInfo"] != null)
            {
                oEmployeeInfo = (Employee)HttpContext.Current.Session["EmployeeInfo"];

                if (oEmployeeInfo.IsSupervisionHead || oEmployeeInfo.IsEngineerManager)
                {
                    ListBuildingSupervision = SupervisionCom.AllSupervisionsByWelayah((int)oEmployeeInfo.DEPT_ID);
                }
                else
                {
                    ListBuildingSupervision = SupervisionCom.SupervisionsByInspectorID(oEmployeeInfo.EMP_NO, 1, false);
                }
            }
        }

        public Employee oEmployeeInfo { get; set; }

        public BuildingSupervision BuildingSupervision { get; set; }

        public List<BuildingSupervision> ListBuildingSupervision { get; set; }

        public SupervisionServicesTypes ServiceType { get; set; }

        public List<SupervisionServicesTypes> ServiceTypesList { get; set; }

        public BuildingPermits BuildingPermits { get; set; }

        public List<BuildingPermits> BuildingPermitsList { get; set; }

        public List<PermitsAttachments> ListOfAttachments { get; set; }

        public PermitsAttachments Attachments { get; set; }

        public PermitsAttachments InspectorAttachments { get; set; }

        public PaymentDetails PaymentDetails { get; set; }
        public List<PaymentDetails> PaymentDetailsList { get; set; }
        public ServiceFeesDetails ServiceFeesDetails { get; set; }
        public Payments Payment { get; set; }
        public List<Payments> Payments { get; set; }

        public int? Quantity { get; set; }
        public int ServiceID { get; set; }
        public decimal? Fees { get; set; }
        public decimal? Total { get; set; }
        public decimal? TempGrandTotal { get; set; }

        public Transactions SupervisionTransact { get; set; }
        public List<Transactions> TransactsList { get; set; }

        public Owner Owner { get; set; }
        public List<Owner> ListOfOwners { get; set; }

        public Contractor Contractor { get; set; }
    }
}
