using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using CloudantDotNet.Models;
using CloudantDotNet.Services;
using dotnetCloudantWebstarter.Models;
using Newtonsoft.Json;

namespace dotnetCloudantWebstarter.Repositories
{
    public class CloudantServiceRepository : IServiceRepository
    {
        private static readonly string _dbName = "services";
        private readonly Creds cloudantCreds;
        private readonly UrlEncoder urlEncoder;

        public CloudantServiceRepository(Creds creds, UrlEncoder urlEncoder)
        {
            cloudantCreds = creds;
            this.urlEncoder = urlEncoder;
        }

        public void Create(Service item)
        {
            CreateServiceRequest request = new CreateServiceRequest(item);

            using (var client = CloudantClient())
            {
                var response = client.PostAsJsonAsync(_dbName, request).Result;
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                string msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
                throw new Exception(msg);
            }
        }

        public void Delete(Service item)
        {
            using (var client = CloudantClient())
            {
                var response = client.DeleteAsync(_dbName + "/" + urlEncoder.Encode(item.CloudantId) + "?rev=" + urlEncoder.Encode(item.Revision)).Result;
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                string msg = "Failure to DELETE. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
                throw new Exception(msg);
            }
        }

        public IEnumerable<Service> GetAll()
        {
            using (var client = CloudantClient())
            {
                var response = client.GetAsync(_dbName + "/_all_docs?include_docs=true").Result;

                if (response.IsSuccessStatusCode)
                {
                    CloudantResponse<Service> cloundantResponse =
                        JsonConvert.DeserializeObject<CloudantResponse<Service>>(
                            response.Content.ReadAsStringAsync().Result);
                    return cloundantResponse.Rows.Select(r => r.Item);
                }

                string msg = "Failure to GET. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
                throw new Exception(msg);
            }
        }

        public Service Get(Guid id)
        {
            return GetAll().FirstOrDefault(s => s.Id == id);
        }

        public void Update(Service item)
        {
            using (var client = CloudantClient())
            {
                var response = client.PutAsJsonAsync(_dbName + "/" + urlEncoder.Encode(item.CloudantId) + "?rev=" + urlEncoder.Encode(item.Revision), item).Result;
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

            HttpClient client = HttpClientFactory.Create(new LoggingHandler());
            client.BaseAddress = new Uri("https://" + cloudantCreds.host);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
            return client;
        }
    }

    public class CreateServiceRequest
    {
        private readonly Service item;

        public CreateServiceRequest(Service item)
        {
            this.item = item;
        }

        public Guid Id => item.Id;
        public Guid OrganizationId => item.OrganizationId;
        public string Name => item.Name;
        public string Description => item.Description;
        public List<string> Tags => item.Tags;
        public List<ServiceInput> Input => item.Input;
    }
}
