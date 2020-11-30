using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.MetaData.BuildingServices
{
  public class Meta_AreaSuveys
    {

        [Key]

        public int IndustrialSurveyID { get; set; }
        public int WelayahID { get; set; }
        public int RegionID { get; set; }
        public int SquareLetterID { get; set; }
        public string RegionArName { get; set; }
        public string RegionEnName { get; set; }
        public Nullable<int> UseTypeID { get; set; }
        public string LocationCoordinates { get; set; }
        public int BuildingsCount { get; set; }
        public int FlatsCount { get; set; }
        public int IndustrialShopsCount { get; set; }
        public int CommercialShopsCount { get; set; }
        public string TransactNo { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedON { get; set; }
        public Nullable<int> BuildingsTypeID { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedON { get; set; }

        




        //[Display(Name = "OwnerName", ResourceType = typeof(Resources.DisplayName_Ar))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        //[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]

        //public string OwnerName { get; set; }

        //[Display(Name = "OwnerCivilId", ResourceType = typeof(Resources.DisplayName_Ar))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        //[RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "NumberOnly")]
        //[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]



        //public long OwnerCivilId { get; set; }


        //public long ConsultantCivilId { get; set; }




        //[Display(Name = "PhoneNo", ResourceType = typeof(Resources.DisplayName_Ar))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        //[RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "NumberOnly")]
        //[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]


        //public string OwnerPhoneNo { get; set; }



        //[Display(Name = "DistrictName", ResourceType = typeof(Resources.DisplayName_Ar))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        //[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]


        //public string DistrictName { get; set; }


        //[Display(Name = "DistrictNo", ResourceType = typeof(Resources.DisplayName_Ar))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        //[RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "NumberOnly")]
        //[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]

        //public Nullable<int> DistrictNo { get; set; }


        //[Display(Name = "ConstructedArea", ResourceType = typeof(Resources.DisplayName_Ar))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        //[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]

        //public string ConstructedArea { get; set; }


        //[Display(Name = "NumberOfFloors", ResourceType = typeof(Resources.DisplayName_Ar))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        //[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]


        //public string NumberOfFloors { get; set; }

        //public int ConsultantCrNo { get; set; }


        //[Display(Name = "ConstructionType", ResourceType = typeof(Resources.DisplayName_Ar))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        //[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]

        //public string ConstructionType { get; set; }
        //[DataType(DataType.Date)]
        //public System.DateTime RequestDate { get; set; }


        //[Display(Name = "SquareArea", ResourceType = typeof(Resources.DisplayName_Ar))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        //[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]

        //public string SquareArea { get; set; }




        //[Display(Name = "LicenseNo", ResourceType = typeof(Resources.DisplayName_Ar))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        //[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]

        //public string LicenseNo { get; set; }

        //public string PaymentReceiptNo { get; set; }

        //public Nullable<decimal> PaymentAmount { get; set; }

        //public Nullable<System.DateTime> PaymentDate { get; set; }

        //public Nullable<int> WorkflowStatus { get; set; }

    }
}
