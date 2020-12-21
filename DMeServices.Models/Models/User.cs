using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMeServices.Models.Common;
using DMeServices.Models.Configrations;
using DMeServices.Models.MetaData;

namespace DMeServices.Models
{

    [LocalizedDisplayName("User"), MetadataType(typeof(Meta_User))]
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public int ConsultantCrNo { get; set; }
        public int MobileNo { get; set; }
        public Nullable<int> PhoneNo { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public int CivilId { get; set; }
        public int UserType { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }

        public string FullName {
            get => FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
            set
            {
                string[] names = value.Split(new char[] {' '});
                var lenghth = names.Length - 4 > 0 ? names.Length-4 : 1;
                FirstName = names[0];
                SecondName = names[1]??"";
                ThirdName = names[2]??"";
                LastName = String.Join(" ", names, names.Length - 1, lenghth);
            }
        }

        public  string JobName {
            get => ConsultantOccupationCom.GetConsultantOccupationById(UserType).OccupationDescArabic;
            set => UserType = ConsultantOccupationCom.GetConsultantOccupationByArabicName(value).OccupationID;
        }
    }
}
