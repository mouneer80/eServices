using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace DMeServices.Models
{
    public class FilesUploader
    {
        public FilesUploader()
        {
            ImagesFiles = new List<HttpPostedFileBase>();
            Files = new List<HttpPostedFileBase>();

        }

        public List<HttpPostedFileBase> ImagesFiles { get; set; }

        //[Display(Name = "Services_File", ResourceType = typeof(Resources.DisplayName_Ar))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Validation), ErrorMessageResourceName = "RequiredFiles")]
        //[FileType("JPG,JPEG,PNG",  ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "ImageType")]
        //public List<HttpPostedFileBase> EditImagesFiles { get; set; }

        public List<HttpPostedFileBase> Files { get; set; }

        public List<HttpPostedFileBase> EditFiles { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.PNG|.JPG|.JPEG|.PDF|.png|.jpg|.jpeg|.pdf)$", ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "FileType")]
        public HttpPostedFileBase File { get; set; }
    }


}