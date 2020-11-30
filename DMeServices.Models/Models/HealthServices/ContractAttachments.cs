using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DMeServices.Models.HealthServices
{
  public  class ContractAttachments
    {
        public long Id { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentContentType { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public string AttachmentPath { get; set; }
        public string Description { get; set; }
        public byte[] AttachmentStream { get; set; }
        public Nullable<long> HsPermitDurationId { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.pdf)$", ErrorMessage = "الملف صوره او pdf فقط")]
        public HttpPostedFileBase File { get; set; }
    }
}
