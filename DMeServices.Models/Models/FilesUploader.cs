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

        public List<HttpPostedFileBase> Files { get; set; }

        public List<HttpPostedFileBase> EditFiles { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "RequirdField", AllowEmptyStrings = false)]
        //[RegularExpression(@"([a-zA-Z0-9\s@,=%$#&_\u0600-\u06FF])+(.png|.PNG|.jpg|.JPG|.jpe?g|.JPE?G|.pdf|.PDF)$", ErrorMessageResourceType = typeof(Resources.Validation_Ar), ErrorMessageResourceName = "FileType")]
        public HttpPostedFileBase File { get; set; }
    }


}