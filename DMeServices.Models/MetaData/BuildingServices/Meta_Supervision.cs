﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.MetaData.BuildingServices
{
  public class Meta_Supervision
    {

        [Key]
        public int Id { get; set; }

        public string TransactNo { get; set; }


        [Display(Name = "OwnerName", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]

        public string OwnerName { get; set; }

        [Display(Name = "OwnerCivilId", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = true)]
        //[RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "NumberOnly")]
        //[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]



        public int OwnerCivilId { get; set; }


        public int ConsultantCivilId { get; set; }

        


        [Display(Name = "PhoneNo", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "NumberOnly")]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]


        public string OwnerPhoneNo { get; set; }



        [Display(Name = "DistrictName", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]


        public string DistrictName { get; set; }


        [Display(Name = "DistrictNo", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
  //      [RegularExpression("^[0-9]*$", ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "NumberOnly")]
   //     [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]

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


        [Display(Name = "ConstructionType", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]

        public string ConstructionType { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime RequestDate { get; set; }


        [Display(Name = "SquareLetterID", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        //[StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]
        public Nullable<int> SquareLetterID { get; set; }


        [Display(Name = "LicenseNo", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]

        public string LicenseNo { get; set; }

        public string PaymentReceiptNo { get; set; }

        public Nullable<decimal> PaymentAmount { get; set; }

        public Nullable<System.DateTime> PaymentDate { get; set; }

        public Nullable<int> WorkflowStatus { get; set; }

        public Nullable<int> WelayahID { get; set; }
        public Nullable<int> RegionID { get; set; }
        public Nullable<int> UseTypeID { get; set; }
        public Nullable<int> BuildingTypeID { get; set; }


    }
}
