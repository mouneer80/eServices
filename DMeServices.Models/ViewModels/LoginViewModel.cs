using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.ViewModels
{
   public class LoginViewModel
    {
        public string userName { get; set; }
        public string Password { get; set; }
    }

    public class GetCookie
    {
        public string cookie { get; set; }
    }

    public class GetUserDataViewModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
    }
}
