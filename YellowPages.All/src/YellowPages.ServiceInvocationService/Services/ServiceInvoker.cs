using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using YellowPages.Core.Entities;
using YellowPages.Core.Repositories;

namespace YellowPages.ServiceInvocationService.Services
{
    public class ServiceInvoker : IServiceInvoker
    {
        /// <summary>
        /// The service repository
        /// </summary>
        private readonly IServiceRepository serviceRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceInvoker"/> class.
        /// </summary>
        /// <param name="serviceRepository">The service repository.</param>
        public ServiceInvoker(IServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

        /// <summary>
        /// Invokes the service.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public InvokeServiceResponse InvokeService(InvokeServiceRequest request)
        {
            InvokeServiceResponse response = new InvokeServiceResponse();

            Service service = this.serviceRepository.Get(request.ServiceId);

            using (HttpClient client = new HttpClient())
            {
                string url = this.BuildUrl(service, request.Input);

                HttpResponseMessage serviceResponse = client.GetAsync(url).Result;
                response.Response = JsonConvert.DeserializeObject<dynamic>(serviceResponse.Content.ReadAsStringAsync().Result);
            }

            return response;
        }

        /// <summary>
        /// Builds the URL.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private string BuildUrl(Service service, IDictionary<string, object> input)
        {
            if (input == null || !input.Any())
                return service.Url;

            StringBuilder builder = new StringBuilder($"{service.Url}?");
            builder.Append(string.Join("&", service.Input.Select(i => $"{i.Name}={input[i.Name]}")));

            return builder.ToString();
        }
    }
}
