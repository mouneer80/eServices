using DMeServices.Models.Revenue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DMeServices.Models.ViewModels.Revenue
{
    public class RevenueViewModel
    {
        public RevenueViewModel()
        {
            oEmployeeInfo = new Employee();
            if (HttpContext.Current.Session["EmployeeInfo"] != null)
            {
                oEmployeeInfo = (Employee)HttpContext.Current.Session["EmployeeInfo"];
                
            }

        }

        public Employee oEmployeeInfo { get; set; }

        public RevMainPayment RevMainPayment { get; set; }

        public List<RevMainPayment> LstRevMainPayment { get; set; }

        public RevLeaseContract RevLeaseContract { get; set; }

        public List<RevLeaseContract> LstRevLeaseContract { get; set; }
    }
}
