using System.Threading.Tasks;
using BusinessSystem.CRM.Manager;
using BusinessSystem.Database.Models.BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BusinessSystem.CRM.Controllers
{
    public class BaseController : Controller
    {
        protected Database.DataBaseContext Db => new Database.DataBaseContext();
        protected readonly UserEntityModel CurrentUserEntity;
        protected readonly string CdnPath;
        public BaseController(IHttpContextAccessor httpContextAccessor, 
            IConfiguration configuration)
        {
            CdnPath = configuration.GetSection("Application").GetSection("CDN").Value;
            var userManager = new UserManager(Db);
            CurrentUserEntity = userManager.GetCurrentUser(httpContextAccessor.HttpContext.User);
        }

        public async Task SetUserProperties()
        {
            var userImage = await Db.Images.GetAsync(CurrentUserEntity.ImageId);
            var currentUserImage = CdnPath + userImage.FileName;
            ViewBag.CurrentUser = CurrentUserEntity;
            ViewBag.UserImage = currentUserImage;
        }
    }
}