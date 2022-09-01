using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboCrypto.WebAPI.Services.Authentication
{
    public interface ITokenService
    {
        string CreateToken(string identifier);
    }
}
