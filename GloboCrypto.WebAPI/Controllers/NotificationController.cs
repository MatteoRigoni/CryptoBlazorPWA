using GloboCrypto.Model.Notifications;
using GloboCrypto.WebAPI.Services.Notifications;
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
        public async Task<NotificationSubscription> Subscribe(string userId, NotificationSubscription subscription)
        {
            return await _notificationService.SubscribeAsync(userId, subscription);
        }

        [HttpPut("subscribe")]
        public async Task UpdateSubscription(string userId, string coinIds)
        {
            await _notificationService.UpdateSubscriptionAsync(userId, coinIds);
        }

        [HttpGet("check-notify")]
        public async Task CheckAndNotify()
        {
            await _notificationService.CheckAndNotifyAsync();
        }
    }
}
