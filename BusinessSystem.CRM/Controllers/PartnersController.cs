using BusinessSystem.CRM.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using BusinessSystem.CRM.Logics.Contexts;
using BusinessSystem.CRM.Logics.Contexts.Authorization;
using Microsoft.AspNetCore.Hosting;
using BusinessSystem.CRM.Logics.Services.User;
using BusinessSystem.Database.Models.DataTransferObjects;
using BusinessSystem.Database.Models.DataTransferObjects.Request;
using BusinessSystem.Database.Models.DataTransferObjects.Response;

namespace BusinessSystem.CRM.Controllers
{
    [Authorize]
    [ExceptionFilter]
    public class PartnersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PartnersController(IHttpContextAccessor httpContextAccessor, 
            IConfiguration configuration, 
            IWebHostEnvironment webHostEnvironment, 
            IUserService userService) : base(httpContextAccessor, configuration)
        {
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }
        
        [HttpGet]
        [Authorize(Roles = PermissionContext.Administrator)]
        public async Task<IActionResult> Index()
        {
            if (CurrentUserEntity == null)
                return Redirect("~/Login/Index");

            await SetUserProperties();
            var partnersList = await _userService.GetPartners();
            return View(partnersList);
        }

        #region Partner Contents

        [HttpGet]
        [Route("Partners/Contents/{userId?}")]
        public async Task<IActionResult> Contents(int? userId)
        {
            if (CurrentUserEntity == null)
                return Redirect("~/Login/Index");

            await SetUserProperties();

            if (userId == null)
                return StatusCode(404);
            if (CurrentUserEntity.RoleId != RolesContext.Administrator && CurrentUserEntity.Id != userId.Value)
                return StatusCode(401);
            
            var partner = await _userService.GetPartnerContentViewModel(userId.Value);
            if (partner?.Partner == null)
                return StatusCode(404);

            ViewBag.SelectedPartnerId = userId.Value;
            return View(partner);
        }
        #endregion

        #region Create
        [HttpGet]
        [Authorize(Roles = PermissionContext.Administrator)]
        public async Task<IActionResult> New()
        {
            if (CurrentUserEntity == null)
                return Redirect("~/Login/Index");

            await SetUserProperties();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = PermissionContext.Administrator)]
        public async Task<IActionResult> New(PartnerRequestModel partnerModel)
        {
            if (CurrentUserEntity == null)
                return Redirect("~/Login/Index");
            
            await SetUserProperties();
            if (!ModelState.IsValid)
                return View(partnerModel);

            await _userService.CreateNewPartner(partnerModel, CdnPath, _webHostEnvironment.WebRootPath);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
