using GloboCrypto.Model.Authentication;
using GloboCrypto.WebAPI.Services.Data;
using GloboCrypto.WebAPI.Services.Events;
using GloboCrypto.WebAPI.Services.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboCrypto.WebAPI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration Configuration;
        private readonly ILocalDbService LocalDb;
        private readonly ITokenService TokenService;
        private readonly IEventService Log;

        public AuthenticationService(
            IConfiguration configuration,
            ILocalDbService localDb,
            ITokenService tokenService,
            IEventService logService
            )
        {
            Configuration = configuration;
            LocalDb = localDb;
            TokenService = tokenService;
            Log = logService;
        }

        private double ExpiryHours => double.Parse(Configuration["ExpiryHours"]);

        public async Task<AuthTokenResponse> Authenticate(string id)
        {
            //  Validate the identifier is in the correct format i.e. from the app
            if (id.StartsWith("CRYPTO-"))
            {
                LocalDb.Delete<RegisteredInstance>(e => e.Id == id);

                // Store new subscription
                await Task.Run(() => LocalDb.Upsert(new RegisteredInstance { Id = id, LastLogin = DateTime.Now }));

                var token = TokenService.CreateToken(id);
                await Log.LogAuthentication(id);

                return new AuthTokenResponse { Result = AuthTokenResponseResult.Success, Token = new AuthToken { Value = token, Expiry = DateTime.Now.AddHours(ExpiryHours) } };
            }
            else
            {
                await Log.LogError($"authentication failed for user {id}", new Exception("Invalid identifier"));
                return new AuthTokenResponse { Result = AuthTokenResponseResult.Fail, Error = new Exception("Invalid identifier") };
            }
        }

        public async Task<IEnumerable<RegisteredInstance>> GetRegisteredInstances()
        {
            return await Task.Run(() => LocalDb.All<RegisteredInstance>());
        }
    }
}
