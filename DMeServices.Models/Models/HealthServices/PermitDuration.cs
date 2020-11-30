using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DMeServices.Models.HealthServices
{
    public class AvalidFileAttribute : ValidationAttribute

    {

        public int MaxLength { get; set; }

        public string[] Allowed { get; set; }

        public override bool IsValid(object value)

        {

            var file = value as HttpPostedFileBase;

            if (file == null)

            {

                return false;

            }

            if (file.ContentLength > MaxLength)

            {

                return false;

            }

            if (string.IsNullOrEmpty(file.FileName) && string.IsNullOrWhiteSpace(file.FileName))

            {

                return false;

            }

            if (!Allowed.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))

            {

                return false;

            }

            return true;

        }

    }
    public class PermitDuration
    {
        public Nullable<System.DateTime> PermitStartDate { get; set; }
        public Nullable<System.DateTime> PermitEndDate { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        public Nullable<long> RevenueReceiptNo { get; set; }
        public Nullable<System.DateTime> RevenueReceiptDate { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        public Nullable<decimal> RevenuePermitFees { get; set; }
        public Nullable<long> HsPermitId { get; set; }
        public long Id { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        public Nullable<System.DateTime> ContractStartDate { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        public Nullable<System.DateTime> ContractEndDate { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "أرقام فقط")]
        public string ContractNo { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "أرقام فقط")]
        public Nullable<decimal> ContractMonthlyFees { get; set; }
        public Nullable<int> WorkflowStatus { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentContentType { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
        public string AttachmentPath { get; set; }
        public string Description { get; set; }
        public byte[] AttachmentStream { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        public Nullable<int> InspectorNo { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        public Nullable<System.DateTime> InspectionDate { get; set; }
        public string HeadSectionNotes { get; set; }
        public Nullable<System.DateTime> RequestDate { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.pdf)$", ErrorMessage = "الملف صوره او pdf فقط")]
        //[AvalidFile(Allowed = new string[] { ".png", ".jpeg", ".jpg" },
        //MaxLength = (1024 * 1024 * 1) / (1024 * 1024),
        //ErrorMessage = "مساحة المرفق لا تزيد عن 1 ميجا بايت")]
        public HttpPostedFileBase File { get; set; }
        public Nullable<int> CivilId { get; set; }
    }
}
