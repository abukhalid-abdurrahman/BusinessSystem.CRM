using BusinessSystem.CRM.Filters;
using BusinessSystem.CRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BusinessSystem.CRM.Controllers
{
    [Authorize]
    [ExceptionFilter]
    public class HomeController : BaseController
    {
        public HomeController(IHttpContextAccessor httpContextAccessor, 
            IConfiguration configuration) 
            : base(httpContextAccessor, configuration)
        {
            
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (CurrentUserEntity == null)
                return Redirect("~/Login/Index");

            await SetUserProperties();
            return View();
        }

        #region Error Handlers
        [HttpGet]
        [AllowAnonymous]
        [Route("/Home/HandleError/{code:int}")]
        public IActionResult HandleError(int code)
        {
            string errorMessage;
            switch (code)
            {
                case 302:
                    errorMessage = "Unavailable for mobile devices!";
                    break;
                case 401:
                    errorMessage = "Unauthorized!";
                    break;
                case 404:
                    errorMessage = "Not Found!";
                    break;
                case 500:
                    errorMessage = "Internal Server Error!";
                    break;
                case 503:
                    errorMessage = "Service Unavailable!";
                    break;
                default:
                    code = 404;
                    errorMessage = "Not Found!";
                    break;
            }
            ErrorViewModel errorModel = new ErrorViewModel()
            {
                StatusCode = code,
                StatusMessage = errorMessage
            };
            return View(errorModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = HttpContext.TraceIdentifier
            });
        }
        #endregion
    }
}
