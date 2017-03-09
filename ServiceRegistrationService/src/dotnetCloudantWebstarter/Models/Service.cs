using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace dotnetCloudantWebstarter.Models
{
    public class Service
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public List<ServiceInput> Input { get; set; }
        [JsonProperty(PropertyName = "_rev")]
        public string Revision { get; set; }
        [JsonProperty(PropertyName = "_id")]
        public string CloudantId { get; set; }
    }

    public class ServiceInput
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class ServiceViewModel
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public List<ServiceInput> Input { get; set; }
    }
}
