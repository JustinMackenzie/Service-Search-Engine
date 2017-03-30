using System;
using System.Collections.Generic;
using YellowPages.Core.Entities;

namespace dotnetCloudantWebstarter.Models
{
    public class CreateServiceViewModel
    {
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public List<ServiceInput> Input { get; set; }
        public string Url { get; set; }
    }
}