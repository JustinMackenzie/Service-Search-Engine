using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YellowPages.Infrastructure.CloudantRepositories
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient client, string requestUri, object o)
        {
            return client.PostAsync(requestUri,
                new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json"));
        }

        public static Task<HttpResponseMessage> PutAsJsonAsync(this HttpClient client, string requestUri, object o)
        {
            return client.PutAsync(requestUri,
                new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json"));
        }
    }
}
