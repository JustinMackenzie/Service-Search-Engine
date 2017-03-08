﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public int Revision { get; set; }
    }

    public class ServiceInput
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
