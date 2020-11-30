using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMeServices.Models.Configrations;
using DMeServices.Models.MetaData;

namespace DMeServices.Models
{

    [LocalizedDisplayName("User"), MetadataType(typeof(Meta_User))]
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> ConsultantCrNo { get; set; }
        public int MobileNo { get; set; }
        public Nullable<int> PhoneNo { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public long CivilId { get; set; }
        public int UserType { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
