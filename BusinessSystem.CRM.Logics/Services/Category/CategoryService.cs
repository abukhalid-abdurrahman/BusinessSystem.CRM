using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessSystem.Database;
using BusinessSystem.Database.Models.DataTransferObjects.Request;
using BusinessSystem.Database.Models.DataTransferObjects.Response;

namespace BusinessSystem.CRM.Logics.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly DataBaseContext _db;
        
        public CategoryService()
        {
            _db = new DataBaseContext();   
        }

        public async Task<CategoryResponseModel> CreateCategory(CategoryRequestModel requestModel)
        {
            if(requestModel == null)
                throw new ArgumentNullException();
            var categoryResponse = new CategoryResponseModel()
            {
                CategoryName = requestModel.CategoryName,
                CreateDate = DateTime.Now
            };
            categoryResponse.CategoryId = await _db.CategoriesRequest.CategoriesRequest.CreateAsync(requestModel);
            return categoryResponse;
        }

        public async Task UpdateCategory(CategoryRequestModel requestModel)
        {
            if(requestModel == null)
                throw new ArgumentNullException();
            await _db.CategoriesRequest.UpdateAsync(requestModel);
        }

        public async Task<List<CategoryResponseModel>> GetCategories(int userId)
        {
            if(userId < 0)
                throw new ArgumentException();
            return await _db.CategoriesResponse.GetPartnerCategories(userId);
        }
    }
}