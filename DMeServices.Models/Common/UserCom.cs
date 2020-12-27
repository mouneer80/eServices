using AutoMapper;
using DMeServices.DAL;
using DMeServices.Models.ViewModels.Permits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DMeServices.Models.Common
{
   public class UserCom
    {
        #region Method :: User By Civil Id

        public static User UserByCivilID(long CivilId)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                Users oUsers = db.Users.Where(x => x.CivilId == CivilId).SingleOrDefault();
                User oUser = Mapper.Map<Users, User>(oUsers);
                return oUser;
            }
        }
        #endregion

        #region Method :: User By Id

        public static User UserByID(long Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {


                Users oUsers = db.Users.Where(x => x.Id == Id).SingleOrDefault();
                User oUser = Mapper.Map<Users, User>(oUsers);

                return oUser;
            }

        }

        public static List<User> AllUsers()
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<Users> oUsers = db.Users.OrderBy(x => x.ConsultantCrNo).ToList();
                List<User> oUser = Mapper.Map<List<Users>, List<User>>(oUsers);
                return oUser;
            }
        }
        #endregion

        #region Method :: Register New User

        public static int RegisterNewUser(UserViewModel oModel)
        {   
            using (eServicesEntities db = new eServicesEntities())
            {


                //var oUsers = new Users();
                // Users oUsers  = db.Users.Where(
                //      x => x.Id == oModel.oUser.Id ||
                // x.CivilId == oModel.oUser.CivilId ||
                // x.PhoneNo == oModel.oUser.PhoneNo ||
                //     x.Email == oModel.oUser.Email ||
                //x.Username == oModel.oUser.Username).SingleOrDefault();

                Users oUsers = db.Users.Where(
                x => x.Id == oModel.oUser.Id).SingleOrDefault();

                if (oUsers != null)
                {
                    return 99;
                }

                // Add by Emad
                //oUsers.CivilId = oModel.oUser.CivilId;
                //oUsers.PhoneNo = oModel.oUser.PhoneNo;
                //oUsers.Email = oModel.oUser.Email;
                // oUsers.Username = oModel.oUser.Username;

                // Emad End

                oUsers = Mapper.Map<User, Users>(oModel.oUser);
                oUsers.CreatedBy = oModel.oUserInfo.FirstName;
                oUsers.CreatedOn = DateTime.Now;
                string sPassword = GetMd5Hash(oModel.oUser.Password);
                oUsers.Password = sPassword;
                db.Users.Add(oUsers);
                db.SaveChanges();
                return 1;
            }
        }
        #endregion

        #region Method :: Update  User Information

        public static int UpdateUserInfo(UserViewModel oModel)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                Users oUsers = db.Users.Where(
                     x => x.Id == oModel.oUser.Id ||
                x.CivilId == oModel.oUser.CivilId ||
                x.PhoneNo == oModel.oUser.PhoneNo ||
                    x.Email == oModel.oUser.Email ||
               x.Username == oModel.oUser.Username).SingleOrDefault();
                if (oUsers == null)
                {
                    return 0;
                }
                oUsers = Mapper.Map<User, Users>(oModel.oUser);
                oUsers.CreatedBy = oModel.oUserInfo.FirstName;
                string sPassword = GetMd5Hash(oModel.oUser.Password);
                oUsers.Password = sPassword;
                oUsers.UpdatedBy = oModel.oUserInfo.FirstName;
                oUsers.UpdatedOn = DateTime.Now;
                db.SaveChanges();
                return 1;
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
        
        #region Method :: Get All Users By CR Number

        public static List<User> GetUsersListByCr(long cr)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<Users> oUsers = db.Users.Where(x => x.ConsultantCrNo == cr).ToList();
                List<User> oUser = Mapper.Map<List<Users>, List<User>>(oUsers);
                return oUser;
            }
        }
        #endregion
    }
}
