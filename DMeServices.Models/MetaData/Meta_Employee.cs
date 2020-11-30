using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.MetaData
{
 public class Meta_Employee
    {

        public int ID { get; set; }

        [Display(Name = "Username", ResourceType = typeof(Resources.DisplayName_Ar))]
        public string NAME { get; set; }

        [Display(Name = "JobName", ResourceType = typeof(Resources.DisplayName_Ar))]
        public string JOB_NAME { get; set; }

        [Display(Name = "Grade", ResourceType = typeof(Resources.DisplayName_Ar))]
        public string GRADE { get; set; }

        [Display(Name = "HireDate", ResourceType = typeof(Resources.DisplayName_Ar))]
        public Nullable<System.DateTime> HIRE_DATE { get; set; }

        [Display(Name = "EmployeeNo", ResourceType = typeof(Resources.DisplayName_Ar))]
        public Nullable<int> EMP_NO { get; set; }

        [Display(Name = "Qualification", ResourceType = typeof(Resources.DisplayName_Ar))]
        public string QUALIFICATION { get; set; }

        [Display(Name = "FileNo", ResourceType = typeof(Resources.DisplayName_Ar))]
        public Nullable<int> FILE_NO { get; set; }

        [Display(Name = "BirthDate", ResourceType = typeof(Resources.DisplayName_Ar))]
        public Nullable<System.DateTime> BIRTH_DATE { get; set; }

        [Display(Name = "MobileNo", ResourceType = typeof(Resources.DisplayName_Ar))]
        public Nullable<int> DEPT_ID { get; set; }

        [Display(Name = "Department", ResourceType = typeof(Resources.DisplayName_Ar))]
        public string EMAIL { get; set; }

        [Display(Name = "MobileNo", ResourceType = typeof(Resources.DisplayName_Ar))]
        public Nullable<int> MOBILE_NO { get; set; }


        public Nullable<System.DateTime> CREATED_ON { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_ON { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<int> JobId { get; set; }




    }
}
