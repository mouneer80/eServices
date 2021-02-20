using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace DMeServices.Models.MetaData.BuildingServices
{
    public class Meta_PermitsAttachments
    {


        [Key]
        public int Id { get; set; }


        public string AttachmentName { get; set; }

        public string AttachmentContentType { get; set; }

        public System.DateTime InsertDate { get; set; }

        public string AttachmentPath { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessageResourceName = "OutOfRange", ErrorMessageResourceType = typeof(Resources.Validation_Ar))]
        public string Description { get; set; }

        public int BldPermitId { get; set; }

        public Nullable<int> AttachmentTypeId { get; set; }
        public string AttachmentStream { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [RegularExpression(@"([a-zA-Z0-9\s@,=%$#&_\u0600-\u06FF])+(.png|.PNG|.jpg|.JPG|.jpe?g|.JPE?G|.pdf|.PDF)$", ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "FileType")]
        public HttpPostedFile File { get; set; }
    }
}
