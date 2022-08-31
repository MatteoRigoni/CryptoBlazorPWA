using GloboCrypto.Model.Data;
using GloboCrypto.WebAPI.Services.Http;
using GloboCrypto.WebAPI.Services.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboCrypto.WebAPI.Services.Coins
{
    public class CoinService : ICoinService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _configuration;

        public CoinService(IHttpService httpService, IConfiguration configuration)
        {
            _httpService = httpService;
            _configuration = configuration;
        }

        public async Task<CoinInfo> GetCoinInfo(string coinId)
        {
            string url = $"https://api.nomics.com/v1/currencies/ticker?key={NomicsApiKey}&ids={coinId}&attributes=id,name,description,logo_url";
            var nomicsCoin = await _httpService.GetAsync<NomicsCoinInfo[]>(url);
            return (nomicsCoin.Length > 0 ? (CoinInfo)nomicsCoin[0] : null);
        }

        public async Task<IEnumerable<CoinPriceInfo>> GetCoinPriceInfo(string coinIds, string currency, string intervals)
        {
            string url = $"https://api.nomics.com/v1/currencies/ticker?key={NomicsApiKey}&ids={coinIds}&interval={intervals}&convert={currency}";
            var nomicsCoinPrices = await _httpService.GetAsync<NomicsCoinPriceInfo[]>(url);
            return nomicsCoinPrices.Select(x => (CoinPriceInfo)x);
        }

        private string NomicsApiKey => _configuration["NomicsAPIKey"];
    }
}
