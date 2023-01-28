using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogWebApi.DataAccess
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        #nullable enable
        Task<T?> GetAsync(Guid id);
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
    }

}
