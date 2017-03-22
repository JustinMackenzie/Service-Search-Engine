using YellowPages.Core.Entities;
using YellowPages.Core.Repositories;

namespace YellowPages.Infrastructure.ApiRepositories
{
    public class ApiServiceRepository : BaseApiRepository<Service>, IServiceRepository
    {
        /// <summary>
        /// Gets the entity route.
        /// </summary>
        /// <value>
        /// The entity route.
        /// </value>
        protected override string EntityRoute => @"http://service-registration-service.mybluemix.net/api/service";
    }
}
