using GloboCrypto.Model.Notifications;
using GloboCrypto.WebAPI.Services.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GloboCrypto.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("subscribe")]
        [Authorize]
        public async Task<NotificationSubscription> Subscribe(NotificationSubscription subscription)
        {
            return await _notificationService.SubscribeAsync(User.Identity.Name, subscription);
        }

        [HttpPut("subscribe")]
        [Authorize]
        public async Task UpdateSubscription(string coinIds)
        {
            await _notificationService.UpdateSubscriptionAsync(User.Identity.Name, coinIds);
        }

        [HttpGet("check-notify")]
        public async Task CheckAndNotify()
        {
            await _notificationService.CheckAndNotifyAsync();
        }
    }
}
