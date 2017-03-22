using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using YellowPages.Core.Entities;
using YellowPages.ServiceSearchService.Models;
using YellowPages.ServiceSearchService.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace YellowPages.ServiceSearchService.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        /// <summary>
        /// The search service
        /// </summary>
        private readonly ISearchService searchService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchController"/> class.
        /// </summary>
        /// <param name="searchService">The search service.</param>
        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        // POST api/search
        [HttpPost]
        public IEnumerable<Service> Post([FromBody]SearchRequest request)
        {
            return this.searchService.SearchServicesByKeywords(request.Keywords);
        }
    }
}
