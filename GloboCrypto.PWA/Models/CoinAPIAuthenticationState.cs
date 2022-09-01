using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace GloboCrypto.PWA.Models
{
    public class CoinAPIAuthenticationState: RemoteAuthenticationState
    {
        public string UniqueId { get; set; }
    }
}
