﻿using DMeServices.Models.BuildingServices;
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

                if (oEmployeeInfo.IsSupervisionHead)
                {
                    ListBuildingSupervision = SupervisionCom.GetAllSupervisionsByFlowStatus(8);
                }
                else
                {
                    ListBuildingSupervision = SupervisionCom.SupervisionsByEngineerId(oEmployeeInfo.EMP_NO);
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
    }
}