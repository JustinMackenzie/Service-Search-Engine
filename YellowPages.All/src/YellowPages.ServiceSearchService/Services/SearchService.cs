using System;
using System.Collections.Generic;
using System.Linq;
using YellowPages.Core.Entities;
using YellowPages.Core.Repositories;

namespace YellowPages.ServiceSearchService.Services
{
    public class SearchService : ISearchService
    {
        /// <summary>
        /// The service repository
        /// </summary>
        private readonly IServiceRepository serviceRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchService"/> class.
        /// </summary>
        /// <param name="serviceRepository">The service repository.</param>
        public SearchService(IServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

        /// <summary>
        /// Searches the services by keywords.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        public IEnumerable<Service> SearchServicesByKeywords(string keywords)
        {
            IEnumerable<string> words = this.GetWordsFromSearch(keywords);
            IEnumerable<Service> services = this.serviceRepository.GetAll();

            return services.Where(s => this.KeywordsContains(words, s));
        }

        /// <summary>
        /// Keywordses the contains.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        private bool KeywordsContains(IEnumerable<string> keywords, Service service)
        {
            foreach (string keyword in keywords)
            {
                if (service.Tags.Contains(keyword, StringComparer.CurrentCultureIgnoreCase))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the words from search.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        private IEnumerable<string> GetWordsFromSearch(string keywords)
        {
            char[] punctuation = keywords.Where(char.IsPunctuation).Distinct().ToArray();
            return keywords.Split().Select(x => x.Trim(punctuation));
        }
    }
}
