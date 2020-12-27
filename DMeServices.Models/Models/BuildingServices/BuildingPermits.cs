using DMeServices.Models.Configrations;
using DMeServices.Models.MetaData.BuildingServices;
using System;
using System.ComponentModel.DataAnnotations;

namespace DMeServices.Models.BuildingServices
{

    [LocalizedDisplayName("BuildingPermits"), MetadataType(typeof(Meta_Permits))]

    public  class BuildingPermits
    {

        public long Id { get; set; }
        public string TransactNo { get; set; }
        public string OwnerName { get; set; }
        public Nullable<long> OwnerCivilId { get; set; }
        public string OwnerPhoneNo { get; set; }
        public string DistrictName { get; set; }
        public Nullable<int> DistrictNo { get; set; }
        public string ConstructedArea { get; set; }
        public string NumberOfFloors { get; set; }
        public long ConsultantCrNo { get; set; }
        public string ConstructionType { get; set; }
        public System.DateTime RequestDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string SquareArea { get; set; }
        public string LicenseNo { get; set; }
        public string PaymentReceiptNo { get; set; }
        public Nullable<decimal> PaymentAmount { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public Nullable<int> WorkflowStatus { get; set; }
        public Nullable<int> DmEngineerNo { get; set; }
        public string DmEngineerComments { get; set; }
        public Nullable<long> ConsultantCivilId { get; set; }
        public Nullable<int> WelayahID { get; set; }
        public Nullable<int> RegionID { get; set; }
        public Nullable<int> SquareLetterID { get; set; }
        public Nullable<int> UseTypeID { get; set; }
        public Nullable<int> BuildingTypeID { get; set; }
        public string KrokiNO { get; set; }
        public string DmFileNumber { get; set; }
        public BuildingTypes BldBuildingTypes { get; set; }
        public LandUseTypes BldLandUseTypes { get; set; }
        public Regions BldRegions { get; set; }
        public SquareLetters BldSquareLetters { get; set; }
        public Welyat BldWelyat { get; set; }

        public Payments BldPayments { get; set; }
        
        public ServiceFeesDetails ServiceFees { get; set; }

    }
}
