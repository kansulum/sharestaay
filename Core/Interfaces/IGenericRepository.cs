using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        Task<IReadOnlyList<T>> GetListAsync();
        Task<T> GetByIDAsync(int id);

        Task<T> GetEntityWithSpec(ISpecifications<T> spec);

         Task<IReadOnlyList<T>> ListAsync(ISpecifications<T> spec);

         Task<int> CountAsync(ISpecifications<T> spec);

         void Add(T entity);
         void Update(T entity);
         void Delete(T entity);
    }
}