using GloboCrypto.Model.Notifications;
using GloboCrypto.WebAPI.Services.Coins;
using GloboCrypto.WebAPI.Services.Data;
using GloboCrypto.WebAPI.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebPush;

namespace GloboCrypto.WebAPI.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly ILocalDbService _localDb;
        private readonly IEventService _eventService;
        private readonly ICoinService _coinService;

        public NotificationService(ILocalDbService localDb, IEventService eventService, ICoinService coinService)
        {
            _localDb = localDb;
            _eventService = eventService;
            _coinService = coinService;
        }

        public async Task CheckAndNotifyAsync()
        {
            const string INTERVAL = "1d";
            var allCoinIds = string.Join(",", _localDb.All<NotificationSubscription>().Select(sub => string.Join(",", sub.CoinIds.ToArray())));
            var uniqueCoinIds = string.Join(",", allCoinIds.Split(',').Distinct());
            var allInfo = await _coinService.GetCoinPriceInfo(uniqueCoinIds, "GBP", INTERVAL);
            foreach (var coinPriceInfo in allInfo)
            {
                var priceChangePctRaw = coinPriceInfo.Intervals[INTERVAL].PriceChangePercent;
                var priceChangePct = Math.Abs(float.Parse(coinPriceInfo.Intervals[INTERVAL].PriceChangePercent));
                if (priceChangePct > 0.05)
                {
                    await SendAsync(coinPriceInfo.Id, $"{coinPriceInfo.Id} has changed {(priceChangePct * 100):0.00} in the last hour");
                }
            }
        }

        public Task<IEnumerable<NotificationSubscription>> GetSubscriptions()
        {
            throw new NotImplementedException();
        }

        public async Task<NotificationSubscription> SubscribeAsync(string userId, NotificationSubscription subscription)
        {
            _localDb.Delete<NotificationSubscription>(e => e.UserId == userId);

            subscription.UserId = userId;
            await Task.Run(() => _localDb.Upsert(subscription));

            await _eventService.LogSubscription(userId);

            return subscription;
        }

        public async Task UpdateSubscriptionAsync(string userId, string coinIds)
        {
            await Task.Run(() =>
            {
                var subscription = _localDb.Query<NotificationSubscription>(sub => sub.UserId == userId).FirstOrDefault();
                if (subscription is not null)
                {
                    var coins = coinIds?.Split(',').ToList();
                    subscription.CoinIds = coins;
                    _localDb.Upsert(subscription);
                    _eventService.LogSubscriptionUpdate(userId);
                }
            });
        }

        public async Task SendAsync(string coinId, string message)
        {
            var subject = "mailto: <test@mail.com>";
            var publicKey = "BDw_UfmrLPew1lxzc6pCwnk702wIVSgw1JECJRFbTA7S9L_kriidg6XASZsvBJWkrHNUWjpi-_YId2E2eReZt88";
            var privateKey = "MGgIg9a0PWmAwjISgkkPjHYj0DoCf1O5QTG38792zGA";

            var coinInfo = await _coinService.GetCoinInfo(coinId);
            var subs = _localDb.Query<NotificationSubscription>(sub => sub.CoinIds.Contains(coinId)).ToList();

            foreach (var subscription in subs)
            {
                var pushSubscription = new PushSubscription(subscription.Url, subscription.P256dh, subscription.Auth);
                var vapidDetails = new VapidDetails(subject, publicKey, privateKey);
                var webPushClient = new WebPushClient();
                try
                {
                    var payload = JsonSerializer.Serialize(new
                    {
                        message,
                        url = $"/",
                        iconurl = coinInfo.LogoUrl,
                    });
                    await webPushClient.SendNotificationAsync(pushSubscription, payload, vapidDetails);
                    await _eventService.LogCoinUpdateNotification(subscription.UserId, coinId);
                }
                catch (WebPushException ex)
                {
                    await _eventService.LogError("Error sending push notification", ex);
                }
            }
        }
    }
}
