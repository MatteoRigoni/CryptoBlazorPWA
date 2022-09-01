using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using GloboCrypto.Model.Authentication;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GloboCrypto.CoinMonitor
{
    public static class CoinMonitor
    {
        private static AuthToken authToken;

        [FunctionName("CoinMonitor")]
        public async static Task Run([TimerTrigger("0 */5 * * * *", RunOnStartup = true)]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            string webApiBase = Environment.GetEnvironmentVariable("GloboCryptoAPI_Base");
            if (string.IsNullOrEmpty(webApiBase))
            {
                log.LogError("GloboCryptoAPI_Base is not configured");
                return;
            }
            else
            {
                if (authToken == null || authToken.HasExpired)
                {
                    var tokenResponse = await GetAuthToken();
                    if (tokenResponse.Result == AuthTokenResponseResult.Success)
                        authToken = tokenResponse.Token;
                    else
                        log.LogError($"Could not get auth token: {tokenResponse.Error}");

                    log.LogInformation($"new token = {authToken.Value}");
                }

                var httpClient = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{webApiBase}/api/Notification/check-notify")
                };
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken.Value);

                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    log.LogInformation($"Check and Notify executed successfully");
                else
                    log.LogError($"Check and Notify failed: [code:{response.StatusCode} => {response.ReasonPhrase}");

                response.EnsureSuccessStatusCode();
            }
        }

        private static async Task<AuthTokenResponse> GetAuthToken()
        {
            var id = $"CRYPTO-coin-monitor";
            var webAPIbase = Environment.GetEnvironmentVariable("GloboCryptoAPI_BASE");
            string url = $"{webAPIbase}/api/Auth/authenticate?id={id}";

            var httpClient = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };
            request.Headers.Add("X-API-KEY", "apikeyyyyyyyyyyyy");

            using var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var rawToken = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<AuthTokenResponse>(rawToken);
                }
                catch (NotSupportedException)
                {
                    Console.WriteLine("The content type is not supported");
                }
                catch (JsonException)
                {
                    Console.WriteLine("Invalid JSON.");
                }
            }
            return null;
        }
    }
}

