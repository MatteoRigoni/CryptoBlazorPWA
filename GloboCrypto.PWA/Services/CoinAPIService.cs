using GloboCrypto.Model.Data;
using System.Net.Http.Json;

namespace GloboCrypto.PWA.Services
{
    public class CoinAPIService : ICoinAPIService
    {
        private readonly IAppSettings _appSettings;
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _httpClient;

        public CoinAPIService(IAppSettings appSettings, IHttpClientFactory clientFactory)
        {
            _appSettings = appSettings;
            _clientFactory = clientFactory;

            _httpClient = _clientFactory.CreateClient("coinapi");
        }

        public async Task<CoinInfo> GetCoinInfo(string coinId)
        {
            string url = $"{_appSettings.APIHost}/api/coin/{coinId}";
            return await _httpClient.GetFromJsonAsync<CoinInfo>(url);
        }

        public async Task<IEnumerable<CoinPriceInfo>> GetCoinPriceInfo(string coinIds, string currency = "EUR", string intervals = "1d")
        {
            string url = $"{_appSettings.APIHost}/api/coin/prices/{coinIds}?currency={currency}&intervals={intervals}";
            return await _httpClient.GetFromJsonAsync<IEnumerable<CoinPriceInfo>>(url);
        }
    }
}
