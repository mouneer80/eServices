using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Revenue
{
    public class RevFinancialSubItem
    {
        public int Id { get; set; }
        public Nullable<long> Code { get; set; }
        public string Name { get; set; }
        public Nullable<long> FinancialItemId { get; set; }
        public string ChargeType { get; set; }
        public Nullable<long> ChargeAmount { get; set; }
        public Nullable<long> SiteCd { get; set; }
    }
}
