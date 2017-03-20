using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace YellowPages.Core.Entities
{
    public class Service : Entity
    {
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public List<ServiceInput> Input { get; set; }
    }
}
