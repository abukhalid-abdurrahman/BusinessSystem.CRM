using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using BusinessSystem.CRM.Models;
using BusinessSystem.CRM.Contexts.Authorization;
using BusinessSystem.Database.Models;
using BusinessSystem.CRM.Filters;
using BusinessSystem.CRM.Logics.Contexts.Authorization;
using BusinessSystem.Database;
using BusinessSystem.Database.Models.BusinessObjects;

namespace BusinessSystem.CRM.Controllers
{
    [ExceptionFilter]
    public class LoginController : Controller
    {
        private readonly AuthenticateContext _authenticateContext;
        private readonly Database.DataBaseContext _db;

        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            _db = new DataBaseContext();
            _authenticateContext = new AuthenticateContext(httpContextAccessor);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel loginModel, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(loginModel);

            var hashContext = new HashContext();
            var hashPassword = hashContext.MD5(loginModel.Password);
            var userEntity = await _db.Users.GetUserAsync(loginModel.Login, hashPassword);
            if (userEntity != null)
            {
                await _authenticateContext.SignIn(userEntity);
                return Redirect(returnUrl ?? "/Home/Index");
            }

            ModelState.AddModelError("", "Login or password is not correct");
            return View(loginModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authenticateContext.SignOut();
            return RedirectToAction("Index");
        }
    }
}
