using System;
using System.Collections.Generic;
using YellowPages.Core.Entities;

namespace YellowPages.Infrastructure.CloudantRepositories
{
    public class PostServiceRequest : PostRequest
    {
        private readonly Service item;

        public PostServiceRequest(Service item)
        {
            this.item = item;
        }

        public Guid Id => item.Id;
        public Guid OrganizationId => item.OrganizationId;
        public string Name => item.Name;
        public string Description => item.Description;
        public List<string> Tags => item.Tags;
        public List<ServiceInput> Input => item.Input;
    }
}