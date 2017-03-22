using System.Collections.Generic;
using Newtonsoft.Json;

namespace YellowPages.Infrastructure.CloudantRepositories
{
    public class CloudantResponse<T>
    {
        [JsonProperty(PropertyName = "total_rows")]
        public int Total { get; set; }
        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }
        [JsonProperty(PropertyName = "rows")]
        public IEnumerable<CloudantDocumentRow<T>> Rows { get; set; }
    }
}