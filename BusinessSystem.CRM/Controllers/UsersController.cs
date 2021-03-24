using System.Threading.Tasks;
using BusinessSystem.CRM.Filters;
using BusinessSystem.CRM.Logics.Contexts.Authorization;
using BusinessSystem.CRM.Logics.Services.User;
using BusinessSystem.Database.Models.DataTransferObjects.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BusinessSystem.CRM.Controllers
{
    [Authorize]
    [ExceptionFilter]
    [Authorize(Roles = PermissionContext.Administrator)]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironmental;
        
        public UsersController(IHttpContextAccessor httpContextAccessor, 
            IConfiguration configuration, 
            IWebHostEnvironment webHostEnvironment, 
            IUserService userService) 
        : base(httpContextAccessor, configuration)
        {
            _userService = userService;
            _webHostEnvironmental = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (CurrentUserEntity == null)
                return Redirect("~/Login/Index");

            await SetUserProperties();
            var usersList = await _userService.GetUsers();
            return View(usersList);
        }
        
        #region Edit
        [HttpGet]
        [Route("Users/Edit/{id?}")]
        [Authorize(Roles = PermissionContext.Administrator)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (CurrentUserEntity == null)
                return Redirect("~/Login/Index");

            await SetUserProperties();

            if (id == null)
                return NotFound();

            var partnerModel = await _userService.GetPartner(id.Value);
            if (partnerModel == null)
                return NotFound();

            ViewBag.UserActiveStatus = partnerModel.Active != true;
            
            return View(new PartnerRequestModel()
            {
                Description = partnerModel.Description,
                Login = partnerModel.Login,
                Password = partnerModel.Password,
                Username = partnerModel.Username,
                PartnerId = partnerModel.PartnerId,
                Email = partnerModel.Email,
                PhoneNumber = partnerModel.PhoneNumber
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = PermissionContext.Administrator)]
        public async Task<IActionResult> Edit(PartnerRequestModel partnerModel)
        {
            if (CurrentUserEntity == null)
                return Redirect("~/Login/Index");
            await SetUserProperties();
            var isEditedSuccessfully = await _userService.EditPartner(partnerModel, CdnPath, _webHostEnvironmental.WebRootPath);
            if (!isEditedSuccessfully)
                return StatusCode(500);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Users/RemoveUser")]
        [Authorize(Roles = PermissionContext.Administrator)]
        public async Task<JsonResult> RemoveUser(RemovePartnerRequestModel partnerRequestModel)
        {
            await _userService.DeactivatePartner(partnerRequestModel);
            return new JsonResult(new { statusCode = 200, message = "OK", data = new { id = partnerRequestModel.Id, removed = partnerRequestModel.Status } });
        }
        #endregion
    }
}