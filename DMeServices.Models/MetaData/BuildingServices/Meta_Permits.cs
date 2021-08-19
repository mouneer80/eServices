using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.MetaData.BuildingServices
{
  public class Meta_Permits
    {

        [Key]
        public int Id { get; set; }

        public string TransactNo { get; set; }


        [Display(Name = "OwnerName", ResourceType = typeof(Resources.DisplayName_Ar))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]
        public string OwnerName { get; set; }

        [Display(Name = "OwnerCivilId", ResourceType = typeof(Resources.DisplayName_Ar))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "NumberOnly")]
        //[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]
        public int OwnerCivilId { get; set; }


        public int ConsultantCivilId { get; set; }

        


        [Display(Name = "PhoneNo", ResourceType = typeof(Resources.DisplayName_Ar))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "NumberOnly")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]
        public string OwnerPhoneNo { get; set; }



        [Display(Name = "ServiceName", ResourceType = typeof(Resources.DisplayName_Ar))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]
        public string ServiceName { get; set; }


        [Display(Name = "DistrictNo", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "NumberOnly")]
        //[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]
        public Nullable<int> DistrictNo { get; set; }


        [Display(Name = "ConstructedArea", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]
        public string ConstructedArea { get; set; }




        [Display(Name = "NumberOfFloors", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]
        public string NumberOfFloors { get; set; }

        public int ConsultantCrNo { get; set; }


        [Display(Name = "BuildingComplex", ResourceType = typeof(Resources.DisplayName_Ar))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]
        public string BuildingComplex { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime RequestDate { get; set; }


        [Display(Name = "SquareLetterID", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        //[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]
        public Nullable<int> SquareLetterID { get; set; }


        [Display(Name = "LicenseNo", ResourceType = typeof(Resources.DisplayName_Ar))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]
        public string LicenseNo { get; set; }

        public string AppointmentStatus { get; set; }

        public Nullable<decimal> PaymentAmount { get; set; }

        public Nullable<System.DateTime> AppointmentDate { get; set; }


        [Display(Name = "WorkflowStatus", ResourceType = typeof(Resources.DisplayName_Ar))]
        public Nullable<int> WorkflowStatus { get; set; }

        [Display(Name = "WelayahID", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [Range(1, Int32.MaxValue)]
        public Nullable<int> WelayahID { get; set; }

        [Display(Name = "RegionID", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        public Nullable<int> RegionID { get; set; }

        [Display(Name = "UseTypeID", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        public Nullable<int> UseTypeID { get; set; }

        [Display(Name = "BuildingTypeID", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        public Nullable<int> BuildingTypeID { get; set; }

        [Display(Name = "KrokiNO", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]
        public string KrokiNO { get; set; }

        [Display(Name = "LandDeedNo", ResourceType = typeof(Resources.DisplayName_Ar))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]
        public string LandDeedNo { get; set; }

        [Display(Name = "DmEngineerComments", ResourceType = typeof(Resources.DisplayName_Ar))]
        public string DmEngineerComments { get; set; }

        [Display(Name = "DmFileNumber", ResourceType = typeof(Resources.DisplayName_Ar))]
        public string DmFileNumber { get; set; }

        [Display(Name = "DMManagerNotes", ResourceType = typeof(Resources.DisplayName_Ar))]
        public string DMManagerNotes { get; set; }

        [Display(Name = "DMLicenseComments", ResourceType = typeof(Resources.DisplayName_Ar))]
        public string DMLicenseComments { get; set; }

        [Display(Name = "MLicenseStatement", ResourceType = typeof(Resources.DisplayName_Ar))]
        public string MLicenseStatement { get; set; }

        [Display(Name = "DMEngineerNotes", ResourceType = typeof(Resources.DisplayName_Ar))]
        public string DMEngineerNotes { get; set; }

        [Display(Name = "LandArea", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]
        public string LandArea { get; set; }

        [Display(Name = "LandDeedReceiptNo", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]
        public string LandDeedReceiptNo { get; set; }

        [Display(Name = "LandDeedReceiptDate", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        //[DataType(DataType.Date, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> LandDeedReceiptDate { get; set; }

    }
}
