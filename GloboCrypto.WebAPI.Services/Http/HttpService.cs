using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GloboCrypto.WebAPI.Services.Http
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<T> GetAsync<T>(string url)
        {
            return _httpClient.GetFromJsonAsync<T>(url);
        }
    }
}
