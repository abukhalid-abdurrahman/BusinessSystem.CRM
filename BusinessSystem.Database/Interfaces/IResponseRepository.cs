using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessSystem.Database.Interfaces
{
    public interface IResponseRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
    }
}