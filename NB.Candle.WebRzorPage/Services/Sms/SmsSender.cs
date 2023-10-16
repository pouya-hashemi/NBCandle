using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Services.Sms
{
    public class SmsSender : ISmsSender
    {
        private readonly string BaseURL = "https://rest.payamak-panel.com/";
        public async Task Send(string body, string PhoneNumber)
        {
            var dto = new SmsSendDto()
            {
                Username="behzad 0797",
                Password="B@a3800797",
                To="98"+PhoneNumber,
                From= "30007004407580",
                Text=body,
                IsFlash=false
            };
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                

                var response =await client.PostAsync("api/SendSMS/SendSMS", content);
                var resBodyString=await response.Content.ReadAsStringAsync();

                var returnDto = JsonConvert.DeserializeObject<SmsResponseDto>(resBodyString);
                
                
            }
        }
        public async Task SendVerification(string PhoneNumber,string verificationCode)
        {
            var dto = new SmsSendVerificationDto()
            {
                Username="behzad 0797",
                Password="B@a3800797",
                To="98"+PhoneNumber,
                Text= verificationCode,
                BodyId= 94693,
            };
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                

                var response =await client.PostAsync("api/SendSMS/BaseServiceNumber", content);
                var resBodyString=await response.Content.ReadAsStringAsync();

                var returnDto = JsonConvert.DeserializeObject<SmsResponseDto>(resBodyString);
                
                
            }
        }
    }
}
