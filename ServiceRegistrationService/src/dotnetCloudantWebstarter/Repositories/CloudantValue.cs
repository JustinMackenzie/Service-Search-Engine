using Newtonsoft.Json;

namespace dotnetCloudantWebstarter.Repositories
{
    public class CloudantValue
    {
        [JsonProperty(PropertyName = "rev")]
        public string Revision { get; set; }
    }
}