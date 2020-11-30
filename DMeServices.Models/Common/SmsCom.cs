using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DMeServices.Models.Common
{
    public class SmsCom
    {




        #region Method ::Send Sms By mobileNum


        public static void SendSms(string mobileNum, string message)
        {

            var userName = ConfigurationManager.AppSettings["UserNameSMS"];
            var password = ConfigurationManager.AppSettings["PasswordSMS"];
            var client = new RestClient("https://ismartsms.net/RestApi/api/SMS/PostSMS");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n\"UserID\": \"" + userName + "\",\r\n\"Password\": \"" + password + "\",\r\n\"Message\": \"" + message + "\",\r\n\"Language\": \"64\",\r\n\"ScheddateTime\": \"01/22/2017 00:00:00\",\r\n\"MobileNo\" : [\"" + mobileNum + "\"]\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);



        }

        #endregion









    }
}
