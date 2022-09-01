namespace GloboCrypto.PWA.Services
{
    public interface IAppSettings
    {
        string Id { get; }
        string CoinData { get; }
        string CoinCache { get; }
        string AuthToken { get; }
        string CacheInvalid { get; }
        string APIHost { get; }
        string Local { get; }
    }
}
