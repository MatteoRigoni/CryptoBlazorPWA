using GloboCrypto.Model.Authentication;
using GloboCrypto.WebAPI.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GloboCrypto.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService AuthenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            AuthenticationService = authenticationService;
        }

        [HttpGet("authenticate")]
        [Authorize(Policy = "ApiKeyPolicy")]
        public async Task<AuthTokenResponse> Authenticate(string id)
        {
            return await AuthenticationService.Authenticate(id);
        }
    }
}
