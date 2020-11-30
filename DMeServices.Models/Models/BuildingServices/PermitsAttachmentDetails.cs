using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.BuildingServices
{
   public class PermitsAttachmentDetails
    {

        public long Id { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentContentType { get; set; }
        public System.DateTime InsertDate { get; set; }
        public string AttachmentPath { get; set; }
        public string Description { get; set; }
        public long BldPermitAttId { get; set; }
        public byte[] AttachmentStream { get; set; }
        public Nullable<int> DmEngineerNo { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> AttachmentTypeId { get; set; }
        public Nullable<int> ConsultantCivilId { get; set; }


    }
}
