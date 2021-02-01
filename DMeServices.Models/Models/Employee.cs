using DMeServices.Models.Configrations;
using DMeServices.Models.MetaData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models
{

    [LocalizedDisplayName("Employee"), MetadataType(typeof(Meta_Employee))]


    public  class Employee
    {

        public int ID { get; set; }
        public string NAME { get; set; }
        public string JOB_NAME { get; set; }
        public string GRADE { get; set; }
        public Nullable<System.DateTime> HIRE_DATE { get; set; }
        public Nullable<int> EMP_NO { get; set; }
        public string QUALIFICATION { get; set; }
        public Nullable<int> FILE_NO { get; set; }
        public Nullable<System.DateTime> BIRTH_DATE { get; set; }
        public Nullable<int> DEPT_ID { get; set; }
        public string EMAIL { get; set; }
        public Nullable<int> MOBILE_NO { get; set; }
        public Nullable<System.DateTime> CREATED_ON { get; set; }
        public string CREATED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_ON { get; set; }
        public string MODIFIED_BY { get; set; }
        public Nullable<int> JobId { get; set; }
        //Department

        public string DEPTName { get; set; }


        // Employee Groups 
        public List<EmployeeGroup> Roles { get; set; }

        //

        public bool IsEngineer { get; set; }

        public bool IsEngineerHead { get; set; }

        public bool IsInspectors { get; set; }

        public bool IsHealthHead { get; set; }

        public bool IsCollectors { get; set; }

        public bool IsManager { get; set; }

        public int SiteCd { get; set; }

        public bool IsSupervisionHead { get; set; }

        public bool IsSupervisionInspector { get; set; }


    }
}
