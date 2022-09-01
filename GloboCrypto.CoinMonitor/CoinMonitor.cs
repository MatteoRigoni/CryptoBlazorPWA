using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GloboCrypto.CoinMonitor
{
    public static class CoinMonitor
    {
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
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{webApiBase}/api/Notification/check-notify")
                };

                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    log.LogInformation($"Check and Notify executed successfully");
                else
                    log.LogError($"Check and Notify failed: [code:{response.StatusCode} => {response.ReasonPhrase}");

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
