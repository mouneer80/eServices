using System;
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
        public int ID { get; set; }
        public int BldPermitID { get; set; }
        public int ServiceTypeID { get; set; }
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


        [DataType(DataType.Date)]
        public System.DateTime RequestDate { get; set; }


        [Display(Name = "LicenseNo", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]

        public string LicenseNo { get; set; }
        [Display(Name = "SupervisionStatus", ResourceType = typeof(Resources.DisplayName_Ar))]
        public Nullable<int> Status { get; set; }
        [Display(Name = "DmInspectorComments", ResourceType = typeof(Resources.DisplayName_Ar))]
        public string DmInspectorComments { get; set; }
        [Display(Name = "DmSupervisionComments", ResourceType = typeof(Resources.DisplayName_Ar))]
        public string DmSupervisionComments { get; set; }
    }
}
