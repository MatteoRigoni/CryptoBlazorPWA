namespace GloboCrypto.PWA.Services
{
    public class AppSettings : IAppSettings
    {
        private readonly IConfiguration _config;

        public AppSettings(IConfiguration config)
        {
            _config = config;
        }

        public string Id => _config["id"];
        public string CoinData => _config["coin-data"];
        public string CoinCache => _config["coin-cache"];
        public string AuthToken => _config["auth-token"];
        public string CacheInvalid => _config["cache-invalid"];
        public string APIHost => _config["api-host"];
        public string Local => _config["app-settings"];
    }
}
