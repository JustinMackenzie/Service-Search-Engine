using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YellowPages.Core.Entities;

namespace YellowPages.Core.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        void Create(T item);
        void Delete(T item);
        IEnumerable<T> GetAll();
        T Get(Guid id);
        void Update(T item);
    }
}
