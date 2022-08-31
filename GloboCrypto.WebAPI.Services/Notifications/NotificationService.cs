using GloboCrypto.Model.Notifications;
using GloboCrypto.WebAPI.Services.Data;
using GloboCrypto.WebAPI.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboCrypto.WebAPI.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly ILocalDbService _localDb;
        private readonly IEventService _eventService;

        public NotificationService(ILocalDbService localDb, IEventService eventService)
        {
            _localDb = localDb;
            _eventService = eventService;
        }

        public Task CheckAndNotifyAsync()
        {
            throw new NotImplementedException();
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
    }
}
