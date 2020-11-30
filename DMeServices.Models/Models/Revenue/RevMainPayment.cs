using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Revenue
{
    public class RevMainPayment
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        public Nullable<int> DeptCode { get; set; }
        public Nullable<long> PaymentCode { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        [Range(1, int.MaxValue)]
        public long FinancialItemId { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        [Range(1, int.MaxValue)]
        public long FinancialSubItemId { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        public string PayerName { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        public Nullable<int> WelyatCode { get; set; }
        public Nullable<int> LocationCode { get; set; }
        public string PaymentType { get; set; }
        public string PaymentCheqNo { get; set; }
        public string PaymentBankCode { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime PaymentCheqDate { get; set; }
        public Nullable<decimal> TransAmount { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        [RegularExpression(@"^\d+(?:\.\d{0,3})?$", ErrorMessage = "أرقام فقط")]
        public Nullable<decimal> PaymentAmount { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        public string PaymentAmountInWords { get; set; }
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime PaymentDate { get; set; }
        public string PaymentCashierCode { get; set; }
        public Nullable<int> MofeNo { get; set; }
        public string InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> MofeDate { get; set; }
        public string DeptBankCode { get; set; }
        public Nullable<System.DateTime> DeptDate { get; set; }
        public string PaymentCheqName { get; set; }
        [Required(ErrorMessage = "حقل مطلوب")]
        public string CreditCardNo { get; set; }
        public Nullable<int> FinancialYear { get; set; }
        public Nullable<int> UserCode { get; set; }
        public Nullable<int> SiteCd { get; set; }

    }
}
