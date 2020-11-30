using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.MetaData
{
   public class Meta_User
    {

        public long Id { get; set; }

        [Display(Name = "Username", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]
        public string Username { get; set; }


        [Display(Name = "Password", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]

        public string Password { get; set; }


        [Display(Name = "FirstName", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]


        public string FirstName { get; set; }


        [Display(Name = "SecondName", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]

        public string SecondName { get; set; }


        [Display(Name = "ThirdName", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]

        public string ThirdName { get; set; }


        [Display(Name = "LastName", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]

        public string LastName { get; set; }


        public Nullable<int> ConsultantCrNo { get; set; }

        [Display(Name = "MobileNo", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [RegularExpression(@"[79]\d{7}", ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "PhoneNoinvalid")]
        public int MobileNo { get; set; }


        [Display(Name = "PhoneNo", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [RegularExpression(@"[79]\d{7}", ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "PhoneNoinvalid")]

        public Nullable<int> PhoneNo { get; set; }


        [Display(Name = "Email", ResourceType = typeof(Resources.DisplayName_Ar))]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "EmailFormat")]
        public string Email { get; set; }


    }
}
