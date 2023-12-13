using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientExampleNetFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(".NET Framework");
                Console.WriteLine("Press enter to send a request");
                Console.ReadLine();

                LoginDTO objToSend = new LoginDTO()
                {
                    UserName = "TestHeader",
                    Password = "TestHeader"
                };

                SendFrameworkRequest(objToSend, @"http://localhost:5135/Login");
            }
        }

        private static void SendFrameworkRequest(LoginDTO pObjToSend, string pRequestUrl)
        {
            using (HttpClient httpClient = new HttpClient())
            {               
                httpClient.BaseAddress = new Uri(pRequestUrl);
                httpClient.DefaultRequestHeaders.Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //Example to dont send Expect Header
                //httpClient.DefaultRequestHeaders.ExpectContinue = false;

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, httpClient.BaseAddress);
                request.Content =
                    new StringContent(JsonSerializer.Serialize(pObjToSend), Encoding.UTF8, "application/json");
                
                //Example to change httpVersion
                //request.Version = HttpVersion.Version10;

                HttpResponseMessage response = httpClient.SendAsync(request).Result;
                string strResult = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                Console.WriteLine(string.Join(Environment.NewLine, JsonSerializer.Deserialize<List<string>>(strResult)));

                Console.ReadKey();
            }
        }

    }
}
