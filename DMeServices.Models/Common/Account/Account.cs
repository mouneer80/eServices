using AutoMapper;
using DMeServices.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DMeServices.Models.Common.Account
{
   public class Account
    {
        #region Method ::User Login
        public static User UserLogin(string userName, string password)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                string sPassword = GetMd5Hash(password);
                Users oUsers = db.Users.Where(u => u.Username == userName && u.Password == sPassword).SingleOrDefault();
                User oLoggedUser = Mapper.Map<Users, User>(oUsers);
                return oLoggedUser;
            }
        }
        #endregion

        #region Method ::User Login By Civil ID
        public static User UserLoginByIDCard(string cardID)
        {
            var id = Convert.ToInt32(cardID);
            using (eServicesEntities db = new eServicesEntities())
            {

                
                Users oUsers = db.Users.Where(u => u.CivilId == id).SingleOrDefault();
                User oLoggedUser = Mapper.Map<Users, User>(oUsers);
                return oLoggedUser;
            }
        }
        #endregion

        #region Method ::Employee Login

        public static Employee EmployeeLogin(string userName, string password)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                string sPassword = GetMd5Hash(password);
                Employees oEmployees = db.Employees.Where(u => u.Username == userName && u.Password == sPassword).Include(y => y.Departments).Include(y => y.EmployeeGroups).SingleOrDefault();

                if (oEmployees == null)
                {
                    return null;
                }
                Employee oEmployee = Mapper.Map<Employees, Employee>(oEmployees);

                oEmployee.DEPTName = oEmployees.Departments.NAME;

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

                    }
                }
                return oEmployee;

            }
        }
        #endregion

        #region Method :: GetMd5Hash
        public static string GetMd5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        #endregion

    }
}
