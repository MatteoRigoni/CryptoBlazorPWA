using GloboCrypto.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GloboCrypto.WebAPI.Services.Models
{
    public class NomicsCoinInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("logo_url")]
        public string LogoUrl { get; set; }
        public static explicit operator CoinInfo(NomicsCoinInfo nci)
        {
            return new CoinInfo
            {
                Id = nci.Id,
                Name = nci.Name,
                Description = nci.Description,
                LogoUrl = nci.LogoUrl
            };
        }
    }
}
