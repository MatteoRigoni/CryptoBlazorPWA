using GloboCrypto.Model.Data;

namespace GloboCrypto.PWA.Services
{
    public interface ICoinAPIService
    {
        Task<CoinInfo> GetCoinInfo(string coinId);
        Task<IEnumerable<CoinPriceInfo>> GetCoinPriceInfo(string coinIds, string currency = "EUR", string intervals = "1d");
    }
}
