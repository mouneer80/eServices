using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using RestSharp.Deserializers;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DMeServices.Models.Common
{
    public class PaymentCom
    {




        #region Method ::Send Sms By mobileNum

        //public static void PayAmount(string UserName, string SourceTypeId, string Amount, string Password, string SuccessUrl, string FailureUrl)

        public async Task<Response> GetListAsync<T>(
           string urlBase,
           string servicePrefix,
           string controller,
           string tokenType,
           string accessToken)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);

                var url = $"{servicePrefix}{controller}";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public static string PayAmount()
        {

            //var userName = ConfigurationManager.AppSettings["UserNameSMS"];
            //var password = ConfigurationManager.AppSettings["PasswordSMS"];
            //var UserName = "contract";
            //var SourceTypeId = "1";
            //var Amount = "50.325";
            //var Password = "123";
            //var SuccessUrl = "eeee";
            //var FailureUrl = "ssss";
            var client = new RestClient("https://www.dhofar.gov.om/ePaymentAPI/API/Paymentrequest/OpenPaymentRequest");
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            
            //request.AddJsonBody("application/json", "{\r\n\"UserName\": \"" + UserName + "\",\r\n\"SourceTypeId\": \"" + SourceTypeId + "\",\r\n\"Amount\": \"" + Amount + "\",\r\n\"Language\": \"64\",\r\n\"ScheddateTime\": \"01/22/2017 00:00:00\",\r\n\"MobileNo\" : [\"" + mobileNum + "\"]\r\n}", ParameterType.RequestBody);
            request.AddJsonBody(new
            {
                UserName = "contract",
                SourceTypeId = "1",
                Amount = "50.325",
                Password = "123",
                SuccessUrl = "eeee",
                FailureUrl = "ssss"
            });
            IRestResponse restResponse = client.Execute(request);
            string response = restResponse.Content;
            return response;
            //RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
            //var x = deserial.Deserialize<Token>(response)

            //return (IRestResponse)JsonConvert.DeserializeObject<Token>(response.Content);
        }

        private class Token
        {
            public string token { get; set; }
            public string url { get; set; }
            public string statusId { get; set; }
            public string status { get; set; }
        }

        public class Response
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public object Result { get; set; }
        }
        #endregion









    }
}
