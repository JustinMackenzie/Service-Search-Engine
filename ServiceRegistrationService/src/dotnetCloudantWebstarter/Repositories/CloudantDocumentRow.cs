using Newtonsoft.Json;

namespace dotnetCloudantWebstarter.Repositories
{
    public class CloudantDocumentRow<T>
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }
        [JsonProperty(PropertyName = "value")]
        public CloudantValue Value { get; set; }
        [JsonProperty(PropertyName = "doc")]
        public T Item { get; set; }
    }
}