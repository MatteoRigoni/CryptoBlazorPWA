using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboCrypto.WebAPI.Services.Models
{
    public class ApiKeyRequirement : IAuthorizationRequirement
    {
        public IReadOnlyList<string> ApiKeys { get; set; }

        public ApiKeyRequirement(IEnumerable<string> apiKeys)
        {
            ApiKeys = apiKeys?.ToList() ?? new List<string>();
        }
    }
}
