using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessSystem.Database.Models.DataTransferObjects.Request;
using BusinessSystem.Database.Models.DataTransferObjects.Response;

namespace BusinessSystem.CRM.Logics.Services.Category
{
    public interface ICategoryService
    {
        Task<CategoryResponseModel> CreateCategory(CategoryRequestModel requestModel);
        Task UpdateCategory(CategoryRequestModel requestModel);
        Task<List<CategoryResponseModel>> GetCategories(int userId);
    }
}