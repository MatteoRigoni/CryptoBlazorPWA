using GloboCrypto.Model.Data;
using GloboCrypto.Model.Notifications;

namespace GloboCrypto.PWA.Services
{
    public interface ICoinAPIService
    {
        Task<CoinInfo> GetCoinInfo(string coinId);
        Task<IEnumerable<CoinPriceInfo>> GetCoinPriceInfo(string coinIds, string currency = "EUR", string intervals = "1d");
        Task SubscribeToNotifications(NotificationSubscription subscription);
        Task UpdateSubscriptions(string coinIds);
    }
}
