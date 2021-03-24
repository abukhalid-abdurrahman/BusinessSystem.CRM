using System.Threading.Tasks;

namespace BusinessSystem.Database.Interfaces
{
    public interface IRequestRepository<T>
    {
        Task<int> CreateAsync(T model);
        Task UpdateAsync(T model);
        Task DeleteAsync(int id);
    }
}