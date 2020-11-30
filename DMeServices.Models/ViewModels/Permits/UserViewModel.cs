using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DMeServices.Models.ViewModels.Permits
{
   public class UserViewModel
    {

      public UserViewModel()
        {

            oUserInfo = new User();
            if (HttpContext.Current.Session["UserInfo"] != null)
            {
                oUserInfo = (User)HttpContext.Current.Session["UserInfo"];
            }

        }

        public User oUserInfo { get; set; }
        public User oUser { get; set; }

    }
}
