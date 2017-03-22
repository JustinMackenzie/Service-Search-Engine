using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YellowPages.Core.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "_rev")]
        public string Revision { get; set; }

        [JsonProperty(PropertyName = "_id")]
        public string CloudantId { get; set; }
    }
}
