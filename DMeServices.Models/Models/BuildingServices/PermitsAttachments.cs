using DMeServices.Models.Configrations;
using DMeServices.Models.MetaData.BuildingServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DMeServices.Models.BuildingServices
{

    [LocalizedDisplayName("PermitsAttachments"), MetadataType(typeof(Meta_PermitsAttachments))]
    public class PermitsAttachments
    {
    
        public long Id { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentContentType { get; set; }
        public System.DateTime InsertDate { get; set; }
        public string AttachmentPath { get; set; }
        public string Description { get; set; }
        public long BldPermitId { get; set; }
        public Nullable<int> AttachmentTypeId { get; set; }
        public byte[] AttachmentStream { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string AttachmentUrl { get; set; }
        



    }
}
