using DMeServices.Models.Configrations;
using DMeServices.Models.MetaData.BuildingServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DMeServices.Models.BuildingServices
{

    
    public class Contractor
    {
        public int Id { get; set; }
        public Nullable<int> Cr_No { get; set; }
        public string Cr_Name { get; set; }
        public Nullable<System.DateTime> Cr_Date { get; set; }
        public string LegalForm { get; set; }
        public Nullable<int> PhoneNo { get; set; }
        public string Email { get; set; }
        public string Governorate { get; set; }
        public string State { get; set; }
        public Nullable<int> Owner_Civil_ID { get; set; }
        public string Foreman_Name { get; set; }
        public Nullable<int> Foreman_Civil_ID { get; set; }
        public string OwnerFullName { get; set; }
    }
}
