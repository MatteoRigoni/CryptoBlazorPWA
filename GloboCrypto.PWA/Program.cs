using Blazored.LocalStorage;
using Blazored.Toast;
using GloboCrypto.PWA;
using GloboCrypto.PWA.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.Configure<AzureFileLoggerOptions>(builder.Configuration.GetSection("AzureLogging"));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();

builder.Services.AddHttpClient("coinapi");
builder.Services.AddTransient<IAppSettings, AppSettings>();
builder.Services.AddTransient<IAppStorageService, AppStorageService>();
builder.Services.AddScoped<ICoinAPIService, CoinAPIService>();


await builder.Build().RunAsync();
