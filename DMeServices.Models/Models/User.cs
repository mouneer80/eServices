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
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public int? ConsultantCrNo { get; set; }
        public int MobileNo { get; set; }
        public int? PhoneNo { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public long CivilId { get; set; }
        public int UserType { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public string FullName {
            get => FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
            set
            {
                string[] names = value.Split(new char[] {' '});
                FirstName = names[0];
                SecondName = names[1];
                ThirdName = names[2];
                LastName = String.Join(" ", names, 3, names.Length - 4);
            }
        }

        public  string JobName {
            get => ConsultantOccupationCom.GetConsultantOccupationById(UserType).OccupationDescArabic;
            set => UserType = ConsultantOccupationCom.GetConsultantOccupationByArabicName(value).OccupationID;
        }
    }
}
