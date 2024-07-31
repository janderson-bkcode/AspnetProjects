using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace ConsumoAPI
{
    public class Program
    {
        static void Main(string[] args)
        {
            //string endPoint = "https://kitsu.io/api/edge/anime?canonicalTitle=Naruto";
           // HttpClient httpClient = new HttpClient();

            //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, endPoint);
            //request.Headers.Add("User-Agent", "HOST");

            //HttpResponseMessage response = httpClient.SendAsync(request).Result;

            //string json = response.Content.ReadAsStringAsync().Result;

            //   Console.WriteLine(json);

            HttpClient httpClient = new HttpClient();
            var url = httpClient.BaseAddress = new Uri("https://kitsu.io/api/edge/anime?filter[text]=naruto");       
            HttpResponseMessage response2 = httpClient.GetAsync(url).Result;
            string json2 = response2.Content.ReadAsStringAsync().Result;
            Console.WriteLine(json2);
        }
    }
}
