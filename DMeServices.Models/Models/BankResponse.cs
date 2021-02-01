using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models
{
   public class BankResponse
    {
        public int status { get; set; }
        public string statusDescr { get; set; }
        public StatusDetails statusDetails { get; set; }
    }

    public class StatusDetails
    {
        public decimal Paymentrequestid { get; set; }
        public decimal? Requestdate { get; set; }
        public string Token { get; set; }
        public decimal? Amount { get; set; }
        public decimal Bankresponseid { get; set; }
        public string Bankpaymentid { get; set; }
        public string Result { get; set; }
        public string Paymentdebitid { get; set; }
        public string Referenceid { get; set; }
        public string Transactionid { get; set; }
        public string Rawresponse { get; set; }
        public string Customerid { get; set; }
        public decimal? Postdate { get; set; }
        public string Errorid { get; set; }
        public string Errordescr { get; set; }
        public decimal Cardtypeid { get; set; }
        public string Cardtypedescription { get; set; }
        public decimal Paymenttypeid { get; set; }
        public string Paymenttypedescr { get; set; }
        public string Paymenttypengdescr { get; set; }
        public decimal Paymentstatusid { get; set; }
        public string Paymentstatusardescr { get; set; }
        public string Paymentstatusengdescr { get; set; }
    }
}
