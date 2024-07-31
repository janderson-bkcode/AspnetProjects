using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testes.HttpService
{
    public class HttpService : IHttpService
    {
        private HttpClient _httpClient;

        public HttpService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> Get(string endPoint)
        {
            return _httpClient.GetAsync($"{endPoint}").Result;
        }

        public async Task<HttpResponseMessage> Post(string endPoint, object sendObject)
        {
            return _httpClient.PostAsJsonAsync($"{endPoint}", sendObject).Result;
        }
    }

     public interface IHttpService
    {
        Task<HttpResponseMessage> Post(string endPoint, Object sendObject);
        Task<HttpResponseMessage> Get(string endPoint);
    }
}