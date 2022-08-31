using GloboCrypto.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GloboCrypto.WebAPI.Services.Models
{
    public class NomicsCoinPriceInfoInterval
    {
        [JsonPropertyName("price_change")]
        public string PriceChange { get; set; }
        [JsonPropertyName("price_change_pct")]
        public string PriceChangePct { get; set; }

        public static explicit operator CoinPriceInfoInterval(NomicsCoinPriceInfoInterval ncpii)
        {
            return new CoinPriceInfoInterval { PriceChange = ncpii.PriceChange, PriceChangePercent = ncpii.PriceChangePct };
        }
    }

    public class NomicsCoinPriceInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("price")]
        public string Price { get; set; }
        [JsonPropertyName("price_date")]
        public string PriceTimestamp { get; set; }
        [JsonPropertyName("1h")]
        public NomicsCoinPriceInfoInterval OneHour { get; set; }
        [JsonPropertyName("1d")]
        public NomicsCoinPriceInfoInterval OneDay { get; set; }
        [JsonPropertyName("7d")]
        public NomicsCoinPriceInfoInterval OneWeek { get; set; }
        [JsonPropertyName("30d")]
        public NomicsCoinPriceInfoInterval OneMonth { get; set; }
        [JsonPropertyName("365d")]
        public NomicsCoinPriceInfoInterval OneYear { get; set; }
        [JsonPropertyName("ytd")]
        public NomicsCoinPriceInfoInterval YearToDate { get; set; }

        public static explicit operator CoinPriceInfo(NomicsCoinPriceInfo npi)
        {
            var intervals = new Dictionary<string, CoinPriceInfoInterval>();
            if (npi.OneHour != null) intervals.Add("1h", (CoinPriceInfoInterval)npi.OneHour);
            if (npi.OneDay != null) intervals.Add("1d", (CoinPriceInfoInterval)npi.OneDay);
            if (npi.OneWeek != null) intervals.Add("7d", (CoinPriceInfoInterval)npi.OneWeek);
            if (npi.OneMonth != null) intervals.Add("30d", (CoinPriceInfoInterval)npi.OneMonth);
            if (npi.OneYear != null) intervals.Add("365d", (CoinPriceInfoInterval)npi.OneYear);
            if (npi.YearToDate != null) intervals.Add("ytd", (CoinPriceInfoInterval)npi.YearToDate);
            return new CoinPriceInfo
            {
                Id = npi.Id,
                Price = npi.Price,
                PriceTimestamp = npi.PriceTimestamp,
                Intervals = intervals
            };
        }
    }
}
