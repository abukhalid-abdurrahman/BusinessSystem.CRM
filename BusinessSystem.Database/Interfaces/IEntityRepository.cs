using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessSystem.Database.Interfaces
{
    public interface IEntityRepository<T>
    {
        Task<int> CreateAsync(T model);
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task UpdateAsync(T model);
        Task DeleteAsync(int id);
    }
}