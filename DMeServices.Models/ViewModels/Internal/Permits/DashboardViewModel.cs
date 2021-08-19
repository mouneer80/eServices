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
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            
            oEmployeeInfo = new Employee();
            if (HttpContext.Current.Session["EmployeeInfo"] != null)
            {
                oEmployeeInfo = (Employee)HttpContext.Current.Session["EmployeeInfo"];
            }
        }

        public Employee oEmployeeInfo { get; set; }

        
        public int Owners_count { get; set; }
        public int AllPermits_Count { get; set; }
        public int NewPermits_count { get; set; }
        public int InspectedPermits_count { get; set; }
        public int CompletePermits_count { get; set; }
        public int TotalPermits_count { get; set; }
        public int Companies_count { get; set; }
        public int Users_count { get; set; }
        public DateTime lastrequest { get; set; }
        public DateTime requestTime { get; set; }
        public DateTime updatedTime { get; set; }
        public TimeSpan diffrenceTime { get; set; }
    }
}
