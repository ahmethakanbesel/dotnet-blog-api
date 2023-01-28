using BlogWebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogWebApi.DataAccess
{
    /*
    public interface IPostRepository : IRepository<Post>
    {
    }*/
    public interface IPostRepository
    {
        Task<bool> CreateAsync(Post post);
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post> GetAsync(Guid guid);
        Task<bool> DeleteAsync(Guid guid);
        Task<bool> UpdateAsync(Post post);

    }
}
