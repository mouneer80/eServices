using DMeServices.Models.Configrations;
using DMeServices.Models.MetaData.BuildingServices;
using System;
using System.ComponentModel.DataAnnotations;

namespace DMeServices.Models.BuildingServices
{

    [LocalizedDisplayName("BuildingControls"), MetadataType(typeof(Meta_Controls))]

    public  class BuildingControls
    {
        public int ID { get; set; }
        public long BldPermitID { get; set; }
        public int ServiceTypeID { get; set; }
        public int Status { get; set; }
        public long OwnerCivilId { get; set; }
        public string KrokiNO { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhoneNo { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string LicenseNo { get; set; }
        public Nullable<int> DmEngineerNo { get; set; }
        public string DmEngineerComments { get; set; }
        public Nullable<long> ConsultantCivilId { get; set; }
        public System.DateTime RequestDate { get; set; }
        public string TransactNo { get; set; }
        public string DmFileNumber { get; set; }
        public ControlServicesTypes ServicesTypes { get; set; }
        public BuildingPermits BuildingPermits { get; set; }

    }
}
