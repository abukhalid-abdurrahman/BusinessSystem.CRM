using System.Threading.Tasks;

namespace BusinessSystem.Database.Interfaces
{
    public interface IRemovable<in T>
    {
        Task RemoveAsync(T model);
    }
}