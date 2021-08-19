using DMeServices.Models.Configrations;
using DMeServices.Models.MetaData.BuildingServices;
using System;
using System.ComponentModel.DataAnnotations;

namespace DMeServices.Models.BuildingServices
{

    [LocalizedDisplayName("BuildingSupervision"), MetadataType(typeof(Meta_Supervision))]

    public  class BuildingSupervision
    {
        public int ID { get; set; }
        public int BldPermitID { get; set; }
        public int ServiceTypeID { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> OwnerCivilId { get; set; }
        public string KrokiNO { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhoneNo { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string LicenseNo { get; set; }
        public Nullable<int> DmEngineerNo { get; set; }

        public Nullable<int> ConsultantCivilId { get; set; }
        public System.DateTime RequestDate { get; set; }
        public string TransactNo { get; set; }
        public string DmFileNumber { get; set; }
        public string DmSupervisionComments { get; set; }
        public string DmInspectorComments { get; set; }
        public Nullable<int> PaymentID { get; set; }
        public string ConsultantNotes { get; set; }
        public string OwnerNotes { get; set; }
        public Nullable<int> ContractorCR_No { get; set; }
        public Nullable<int> ContractorID { get; set; }
        public string DMSupervisionStatment { get; set; }
        public Nullable<int> SupervisionRelatedID { get; set; }
        public Nullable<int> WelayahID { get; set; }
        public Nullable<int> RegionID { get; set; }
        public Nullable<int> SquareLetterID { get; set; }
        public Nullable<int> DistrictNo { get; set; }
        public Nullable<int> UseTypeID { get; set; }
        public string LandArea { get; set; }
        public SupervisionServicesTypes BldSupervisionServicesTypes { get; set; }
        public BuildingPermits BldPermits { get; set; }
        public Contractor Contractor { get; set; }

    }
}
