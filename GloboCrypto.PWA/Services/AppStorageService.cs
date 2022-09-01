using Blazored.LocalStorage;
using GloboCrypto.Model.Authentication;
using GloboCrypto.Model.Data;
using GloboCrypto.PWA.Models;

namespace GloboCrypto.PWA.Services
{
    public class AppStorageService : IAppStorageService
    {
        private readonly IAppSettings _appSettings;
        private readonly ILocalStorageService _localStorage;

        public AppStorageService(IAppSettings appSettings, ILocalStorageService localStorage)
        {
            _appSettings = appSettings;
            _localStorage = localStorage;
        }

        public async Task<List<CoinInfo>> GetCoinListAsync()
        {
            return (List<CoinInfo>)await _localStorage.GetItemAsync<IEnumerable<CoinInfo>>(_appSettings.CoinData);
        }

        public async Task<CoinTrackerCache> GetCoinTrackerCacheAsync()
        {
            return await _localStorage.GetItemAsync<CoinTrackerCache>(_appSettings.CoinCache);
        }

        public async Task<string> GetIdAsync()
        {
            return await _localStorage.GetItemAsync<string>(_appSettings.Id) ?? await _GenerateNewIdAsync();
        }

        public async Task<LocalSettings> GetLocalSettingsAsync()
        {
            return await _localStorage.GetItemAsync<LocalSettings>(_appSettings.Local);
        }

        public async Task<bool> IsCacheInvalidAsync()
        {
            return await _localStorage.GetItemAsync<bool>(_appSettings.CacheInvalid);
        }

        public async Task SaveCoinListAsync(IEnumerable<CoinInfo> coinList)
        {
            await _localStorage.SetItemAsync(_appSettings.CoinData, coinList);
            await _localStorage.SetItemAsync(_appSettings.CacheInvalid, true);
        }

        public async Task SaveCoinTrackerCacheAsync(CoinTrackerCache coinTrackerCache)
        {
            await _localStorage.SetItemAsync(_appSettings.CoinCache, coinTrackerCache);
        }

        public async Task SaveLocalSettingsAsync(LocalSettings settings)
        {
            await _localStorage.SetItemAsync(_appSettings.Local, settings);
        }

        private async Task<string> _GenerateNewIdAsync()
        {
            var newId = Guid.NewGuid().ToString();
            await _localStorage.SetItemAsync(_appSettings.Id, newId);
            return newId;
        }

        public async Task<AuthToken> GetSavedAuthToken()
        {
            return await _localStorage.GetItemAsync<AuthToken>(_appSettings.AuthToken) ?? null;
        }

        public async Task SaveAuthToken(AuthToken authToken)
        {
            await _localStorage.SetItemAsync(_appSettings.AuthToken, authToken);
        }
    }
}
