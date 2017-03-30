using System;
using System.Collections.Generic;
using System.Linq;
using YellowPages.Core.Entities;
using YellowPages.Core.Repositories;

namespace dotnetCloudantWebstarter.Services
{
    public class ServiceManager : IServiceManager
    {
        /// <summary>
        /// The cloudant service
        /// </summary>
        private readonly IServiceRepository serviceRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceManager" /> class.
        /// </summary>
        /// <param name="serviceRepository">The service repository.</param>
        public ServiceManager(IServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

        /// <summary>
        /// Gets all services.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Service> GetAllServices()
        {
            return serviceRepository.GetAll();
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Service GetService(Guid id)
        {
            return serviceRepository.Get(id);
        }

        /// <summary>
        /// Adds the service.
        /// </summary>
        /// <param name="service">The service.</param>
        public void AddService(Service service)
        {
            serviceRepository.Create(service);
        }

        /// <summary>
        /// Updates the service.
        /// </summary>
        /// <param name="service">The service.</param>
        public void UpdateService(Service service)
        {
            serviceRepository.Update(service);
        }

        /// <summary>
        /// Removes the service.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void RemoveService(Guid id)
        {
            Service service = GetService(id);
            serviceRepository.Delete(service);
        }

        /// <summary>
        /// Gets all services by organization.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        public IEnumerable<Service> GetAllServicesByOrganization(Guid organizationId)
        {
            return GetAllServices().Where(s => s.OrganizationId == organizationId);
        }
    }
}
