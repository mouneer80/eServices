using AutoMapper;
using DMeServices.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Common
{
    public class EmployeeCom
    {


        #region Method :: Employee By ID

        public static Employee EmployeeByID(string EmpId)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                int? Id = int.Parse(EmpId);

                Employees oEmployees = db.Employees.Where(x => x.EMP_NO == Id).Include(y => y.Departments).Include(y => y.EmployeeGroups).SingleOrDefault();
                Employee oEmployee = Mapper.Map<Employees, Employee>(oEmployees);
                if (oEmployee == null)
                {
                    return null;
                }
                oEmployee.DEPTName = oEmployees.Departments.NAME;
                //      List<EmployeeGroup> LstEmployeeGroup = Mapper.Map<List<EmployeeGroups>, List<EmployeeGroup>>(oEmployees.EmployeeGroups.ToList());
                foreach (var item in oEmployees.EmployeeGroups)
                {
                    switch (item.GroupId)
                    {
                        case 1:
                            oEmployee.IsEngineer = true;
                            break;

                        case 2:
                            oEmployee.IsHealthHead = true;
                            break;

                        case 3:
                            oEmployee.IsInspectors = true;
                            break;

                        case 4:
                            oEmployee.IsEngineerHead = true;
                            break;
                        case 5:
                            oEmployee.IsCollectors = true;
                            break;

                        case 6:
                            oEmployee.IsManager = true;
                            break;

                        case 7:
                            oEmployee.IsSupervisionInspector = true;
                            break;

                        case 8:
                            oEmployee.IsSupervisionHead = true;
                            break;

                    }
                }
                //       oEmployee.Roles = LstEmployeeGroup;
                return oEmployee;
            }

        }
        #endregion





        #region Method :: Employee By ID

        public static List<Employee> EmployeeByJobID(int Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<Employees> ListEmployees = db.Employees.Where(x => x.JobId == Id).Include(y => y.Departments).Include(y => y.EmployeeGroups).ToList();
                List<Employee> LstEmployee = Mapper.Map<List<Employees>, List<Employee>>(ListEmployees);

                return LstEmployee;


            }

        }
        #endregion

    }
}
