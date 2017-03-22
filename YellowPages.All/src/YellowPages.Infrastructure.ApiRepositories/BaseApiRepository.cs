using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using YellowPages.Core.Entities;
using YellowPages.Core.Repositories;

namespace YellowPages.Infrastructure.ApiRepositories
{
    public abstract class BaseApiRepository<T> : IRepository<T> where T : Entity
    {
        /// <summary>
        /// Gets the entity route.
        /// </summary>
        /// <value>
        /// The entity route.
        /// </value>
        protected abstract string EntityRoute { get; }

        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Create(T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Delete(T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T Get(Guid id)
        {
            T result = default(T);

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = client.GetAsync($"{EntityRoute}/{id}").Result;
                result = JsonConvert.DeserializeObject<T>(responseMessage.Content.ReadAsStringAsync().Result);
            }

            return result;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> result = Enumerable.Empty<T>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = client.GetAsync(EntityRoute).Result;
                result = JsonConvert.DeserializeObject<IEnumerable<T>>(
                    responseMessage.Content.ReadAsStringAsync().Result);
            }

            return result;
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
