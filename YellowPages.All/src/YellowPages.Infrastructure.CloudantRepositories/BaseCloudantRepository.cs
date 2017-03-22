using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using Newtonsoft.Json;
using YellowPages.Core.Entities;
using YellowPages.Core.Repositories;

namespace YellowPages.Infrastructure.CloudantRepositories
{
    public abstract class BaseCloudantRepository<T> : IRepository<T> where T : Entity
    {
        private readonly Creds cloudantCreds;
        private readonly UrlEncoder urlEncoder;

        protected BaseCloudantRepository(Creds creds, UrlEncoder urlEncoder)
        {
            cloudantCreds = creds;
            this.urlEncoder = urlEncoder;
        }

        protected abstract string DatabaseName { get; }

        protected abstract PostRequest CreatePostRequest(T item);

        public void Create(T item)
        {
            using (var client = CloudantClient())
            {
                var response = client.PostAsJsonAsync(DatabaseName, CreatePostRequest(item)).Result;
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                string msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
                throw new Exception(msg);
            }
        }

        public void Delete(T item)
        {
            using (var client = CloudantClient())
            {
                var response = client.DeleteAsync(DatabaseName + "/" + urlEncoder.Encode(item.CloudantId) + "?rev=" + urlEncoder.Encode(item.Revision)).Result;
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                string msg = "Failure to DELETE. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
                throw new Exception(msg);
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (var client = CloudantClient())
            {
                var response = client.GetAsync(DatabaseName + "/_all_docs?include_docs=true").Result;

                if (response.IsSuccessStatusCode)
                {
                    CloudantResponse<T> cloundantResponse =
                        JsonConvert.DeserializeObject<CloudantResponse<T>>(
                            response.Content.ReadAsStringAsync().Result);
                    return cloundantResponse.Rows.Select(r => r.Item);
                }

                string msg = "Failure to GET. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
                throw new Exception(msg);
            }
        }

        public T Get(Guid id)
        {
            return GetAll().FirstOrDefault(s => s.Id == id);
        }

        public void Update(T item)
        {
            using (var client = CloudantClient())
            {
                var response = client.PutAsJsonAsync(DatabaseName + "/" + urlEncoder.Encode(item.CloudantId) + "?rev=" + urlEncoder.Encode(item.Revision), item).Result;
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                string msg = "Failure to PUT. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
                throw new Exception(msg);
            }
        }

        private HttpClient CloudantClient()
        {
            if (cloudantCreds.username == null || cloudantCreds.password == null || cloudantCreds.host == null)
            {
                throw new Exception("Missing Cloudant NoSQL DB service credentials");
            }

            var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(cloudantCreds.username + ":" + cloudantCreds.password));

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://" + cloudantCreds.host);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
            return client;
        }
    }

    public class PostRequest
    {
    }
}
