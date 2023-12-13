using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

while (true)
{
    Console.WriteLine(".NET Core");
    Console.WriteLine("Press enter to send a request");
    Console.ReadLine();

    LoginDTO objToSend = new LoginDTO()
    {
        UserName = "TestHeader",
        Password = "TestHeader"
    };

    SendNetCoreRequest(objToSend, @"http://localhost:5135/Login");
}

void SendNetCoreRequest(LoginDTO pObjToSend, string pRequestUrl)
{
    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
    
    using (HttpClient httpClient = new HttpClient())
    {
        httpClient.BaseAddress = new Uri(pRequestUrl);
        httpClient.DefaultRequestHeaders.Accept
            .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, httpClient.BaseAddress);
        request.Content =
            new StringContent(JsonSerializer.Serialize(pObjToSend), Encoding.UTF8, "application/json");

        HttpResponseMessage response = httpClient.SendAsync(request).Result;
        string strResult = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        Console.WriteLine(string.Join(Environment.NewLine, JsonSerializer.Deserialize<List<string>>(strResult)));

        Console.ReadKey();
    }
}
