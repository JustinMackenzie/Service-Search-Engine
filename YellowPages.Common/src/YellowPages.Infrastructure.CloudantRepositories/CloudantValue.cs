using Newtonsoft.Json;

namespace YellowPages.Infrastructure.CloudantRepositories
{
    public class CloudantValue
    {
        [JsonProperty(PropertyName = "rev")]
        public string Revision { get; set; }
    }
}