using GloboCrypto.WebAPI.Services.Data;
using GloboCrypto.WebAPI.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GloboCrypto.WebAPI.Services.Events
{
    public class EventService : IEventService
    {
        private readonly ILocalDbService _localDbService;

        public EventService(ILocalDbService localDbService)
        {
            _localDbService = localDbService;
        }

        public async Task<IEnumerable<EventLogEntry>> GetAllEvents()
        {
            return await Task.Run(() => _localDbService.All<EventLogEntry>());
        }

        public async Task<IEnumerable<EventLogEntry>> GetEventsByTypes(IEnumerable<EventLogEntryType> entryTypes)
        {
            return await Task.Run(() => _localDbService.Query<EventLogEntry>(x => entryTypes.Contains(x.EventType)));            
        }

        public async Task LogAuthentication(string userId)
        {
            var logEntry = new EventLogEntry
            {
                UserId = userId,
                EventType = EventLogEntryType.Authenticate,
                Message = $"user {userId} authenticated"
            };
            await LogEntryAsync(logEntry);
        }

        public async Task LogCoinUpdateNotification(string userId, string coinId)
        {
            var logEntry = new EventLogEntry
            {
                UserId = userId,
                CoinId = coinId,
                EventType = EventLogEntryType.Notification,
                Message = $"user {userId} sent notification of update to coin {coinId}"
            };
            await LogEntryAsync(logEntry);
        }

        public async Task LogError(string message, Exception ex)
        {
            var logEntry = new EventLogEntry
            {
                UserId = "system",
                EventType = EventLogEntryType.Error,
                Message = message,
                Exception = ex.ToString()
            };
            await LogEntryAsync(logEntry);
        }

        public async Task LogSubscription(string userId)
        {
            var logEntry = new EventLogEntry
            {
                UserId = userId,
                EventType = EventLogEntryType.Subscription,
                Message = $"user {userId} subscribed to notifications"
            };
            await LogEntryAsync(logEntry);
        }

        // Event of changing coins to which user is subscribed
        public async Task LogSubscriptionUpdate(string userId)
        {
            var logEntry = new EventLogEntry
            {
                UserId = userId,
                EventType = EventLogEntryType.SubscriptionUpdate,
                Message = $"user {userId} updated coin list"
            };
            await LogEntryAsync(logEntry);
        }

        public async Task<IEnumerable<EventLogEntry>> QueryEvents(Expression<Func<EventLogEntry, bool>> query)
        {
            return await Task.Run(() => _localDbService.Query<EventLogEntry>(query));
        }

        private async Task LogEntryAsync(EventLogEntry logEntry)
        {
            logEntry.DateTime = DateTime.Now;
            await Task.Run(() => _localDbService.Insert(logEntry));
        }
    }
}
