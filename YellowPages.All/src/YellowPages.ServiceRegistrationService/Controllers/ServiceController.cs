using System;
using System.Collections.Generic;
using dotnetCloudantWebstarter.Models;
using dotnetCloudantWebstarter.Services;
using Microsoft.AspNetCore.Mvc;
using YellowPages.Core.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace dotnetCloudantWebstarter.Controllers
{
    [Route("api/[controller]")]
    public class ServiceController : Controller
    {
        /// <summary>
        /// The service manager
        /// </summary>
        private readonly IServiceManager serviceManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceController"/> class.
        /// </summary>
        /// <param name="serviceManager">The serviceManager.</param>
        public ServiceController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Service> Get()
        {
            return serviceManager.GetAllServices();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Service Get(Guid id)
        {
            return serviceManager.GetService(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]CreateServiceViewModel value)
        {
            Service service = new Service
            {
                Id = Guid.NewGuid(),
                Description = value.Description,
                Input = value.Input,
                Name = value.Name,
                OrganizationId = value.OrganizationId,
                Tags = value.Tags,
                Url = value.Url
            };

            serviceManager.AddService(service);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]Service value)
        {
            serviceManager.UpdateService(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            serviceManager.RemoveService(id);
        }
    }
}
