using System.Threading.Tasks;
using BusinessSystem.CRM.Filters;
using BusinessSystem.CRM.Logics.Contexts;
using BusinessSystem.CRM.Logics.Services.Category;
using BusinessSystem.Database.Models;
using BusinessSystem.Database.Models.BusinessObjects;
using BusinessSystem.Database.Models.DataTransferObjects.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BusinessSystem.CRM.Controllers
{
    [Authorize]
    [ExceptionFilter]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        
        public CategoryController(IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,
            ICategoryService categoryService) : base(httpContextAccessor, configuration)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("Category/Index/{userId?}")]
        public async Task<IActionResult> Index(int? userId)
        {
            if (userId == null)
                return StatusCode(404);

            if (userId != CurrentUserEntity.Id && CurrentUserEntity.RoleId != RolesContext.Administrator) return StatusCode(401);
            var categories = await _categoryService.GetCategories(userId.Value);
            return PartialView(categories);

        }

        [HttpPost]
        [Route("Category/Create/")]
        public async Task<JsonResult> CreateCategory(CategoryRequestModel requestModel)
        {
            if(CurrentUserEntity.RoleId != RolesContext.Administrator && CurrentUserEntity.Id != requestModel.PartnerId)
                return new JsonResult(new { statusCode = 401, data = "Unauthorized" });

            if(string.IsNullOrEmpty(requestModel.CategoryName))
                return new JsonResult(new { statusCode = 400, message = "Bad Request" });

            var category = await _categoryService.CreateCategory(requestModel);
            return new JsonResult(new { statusCode = 200, message = "OK", data = category });
        }

        [HttpGet]
        [Route("Category/GetCategories/{userId?}")]
        public async Task<JsonResult> GetCategories(int? userId)
        {
            if(userId == null)
                return new JsonResult(new { statusCode = 400, data = "Bad Request" });

            if(CurrentUserEntity.RoleId != RolesContext.Administrator && CurrentUserEntity.Id != userId)
                return new JsonResult(new { statusCode = 401, data = "[]" });

            var list = await _categoryService.GetCategories(userId.Value);
            return new JsonResult(new { statusCode = 200, data = list });
        }
    }
}