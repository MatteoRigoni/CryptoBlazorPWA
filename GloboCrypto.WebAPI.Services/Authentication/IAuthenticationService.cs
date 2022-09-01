using GloboCrypto.Model.Authentication;
using GloboCrypto.WebAPI.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboCrypto.WebAPI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthTokenResponse> Authenticate(string id);
        Task<IEnumerable<RegisteredInstance>> GetRegisteredInstances();
    }
}
