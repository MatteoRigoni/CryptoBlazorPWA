using GloboCrypto.Model.Data;

namespace GloboCrypto.PWA.Models
{
    public class CoinTrackerCache
    {
        public DateTimeOffset CacheTime { get; set; }
        public IEnumerable<CoinPriceInfo> CoinPrices { get; set; }
    }
}
