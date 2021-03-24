using System.Threading.Tasks;
using BusinessSystem.CRM.Filters;
using BusinessSystem.CRM.Logics.Contexts;
using BusinessSystem.CRM.Logics.Services.Good;
using BusinessSystem.Database.Models.DataTransferObjects;
using BusinessSystem.Database.Models.DataTransferObjects.Request;
using BusinessSystem.Database.Models.DataTransferObjects.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BusinessSystem.CRM.Controllers
{
    [Authorize]
    [ExceptionFilter]
    public class GoodController : BaseController
    {
        private readonly IGoodService _goodService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GoodController(IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration, 
            IWebHostEnvironment webHostEnvironment, 
            IGoodService goodService) : base(httpContextAccessor, configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _goodService = goodService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? userId)
        {
            if (userId == null)
                return StatusCode(404);

            if (userId != CurrentUserEntity.Id && CurrentUserEntity.RoleId != RolesContext.Administrator)
                return StatusCode(401);
            var goods = await _goodService.GetPartnerGoodsList(CurrentUserEntity.Id);
            return PartialView(goods);

        }

        [HttpPost]
        [Route("Good/Create")]
        public async Task<JsonResult> CreateGood(GoodRequestModel goodRequestModel)
        {
            if(CurrentUserEntity.RoleId != RolesContext.Administrator && CurrentUserEntity.Id != goodRequestModel.PartnerId)
                return new JsonResult(new { statusCode = 401, message = "Unauthorized" });

            var goodModel = await _goodService.CreateNewGood(goodRequestModel, CdnPath, _webHostEnvironment.WebRootPath);
            return new JsonResult(new { statusCode = 200, message = "OK", data = goodModel });
        }
    }
}