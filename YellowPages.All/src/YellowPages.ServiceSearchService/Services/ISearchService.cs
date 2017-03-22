using System.Collections.Generic;
using YellowPages.Core.Entities;

namespace YellowPages.ServiceSearchService.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        /// Searches the services by keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        IEnumerable<Service> SearchServicesByKeywords(string keywords);
    }
}
