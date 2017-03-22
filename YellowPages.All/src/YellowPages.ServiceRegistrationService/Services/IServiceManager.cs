using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetCloudantWebstarter.Models;
using YellowPages.Core.Entities;

namespace dotnetCloudantWebstarter.Services
{
    public interface IServiceManager
    {
        /// <summary>
        /// Gets all services.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Service> GetAllServices();

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Service GetService(Guid id);

        /// <summary>
        /// Adds the service.
        /// </summary>
        /// <param name="service">The service.</param>
        void AddService(Service service);

        /// <summary>
        /// Updates the service.
        /// </summary>
        /// <param name="service">The service.</param>
        void UpdateService(Service service);

        /// <summary>
        /// Removes the service.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void RemoveService(Guid id);
    }
}
