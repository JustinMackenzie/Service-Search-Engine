using System;
using System.Collections.Generic;
using dotnetCloudantWebstarter.Models;

namespace dotnetCloudantWebstarter.Repositories
{
    public interface IServiceRepository
    {
        void Create(Service item);
        void Delete(Service item);
        IEnumerable<Service> GetAll();
        Service Get(Guid id);
        void Update(Service item);
    }
}
