using System.Threading.Tasks;
using BusinessSystem.CRM.Filters;
using BusinessSystem.CRM.Logics.Services.Messaging;
using BusinessSystem.CRM.Logics.Services.PartnerApplication;
using BusinessSystem.Database.Models.BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BusinessSystem.CRM.Controllers
{
    [Authorize]
    [ExceptionFilter]
    public class PartnersApplications : BaseController
    {
        private readonly IMessagingService _messagingService;
        private readonly IPartnersApplicationsService _partnersApplications;
        private readonly MessagingAdapter _messagingAdapter;
        public PartnersApplications(IHttpContextAccessor httpContextAccessor, 
            IConfiguration configuration, 
            IPartnersApplicationsService partnersApplications) : base(httpContextAccessor, configuration)
        {
            _partnersApplications = partnersApplications;
            _messagingService = new ApplicationMessaging(_partnersApplications);
            _messagingAdapter = new MessagingAdapter(_messagingService);
        }
        
        [HttpGet]
        [Route("PartnersApplications/Index/{partnerId?}")]
        public async Task<IActionResult> Index(int? partnerId)
        {
            if (partnerId == null)
                return StatusCode(401);
            
            await SetUserProperties();
            var inboxMails = await _partnersApplications.GetInboxApplicationsAsync(partnerId.Value);
            var sentMails = await _partnersApplications.GetSentApplicationsAsync(partnerId.Value);
            ViewBag.InboxMails = inboxMails;
            ViewBag.SentMails = sentMails;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CreateApplication(MessagingEntity messagingEntity)
        {
            await _messagingAdapter.SendMessage(messagingEntity);
            return new JsonResult(new { statusCode = 200, message = "OK", data = messagingEntity });
        }

        [HttpPost]
        public async Task<JsonResult> EditApplication(MessagingEntity messagingEntity)
        {
            await _messagingAdapter.EditMessage(messagingEntity);
            return new JsonResult(new { statusCode = 200, message = "OK", data = messagingEntity });
        }

        [HttpPost]
        public async Task<JsonResult> ChangeApplicationState(MessagingEntity messagingEntity)
        {
            await _messagingAdapter.ChangeMessageState(messagingEntity);
            return new JsonResult(new { statusCode = 200, message = "OK", data = messagingEntity });
        }
        
        [HttpPost]
        public async Task<JsonResult> RemoveApplication(MessagingEntity messagingEntity)
        {
            await _messagingAdapter.RemoveMessage(messagingEntity);
            return new JsonResult(new { statusCode = 200, message = "OK", data = messagingEntity });
        }
    }
}