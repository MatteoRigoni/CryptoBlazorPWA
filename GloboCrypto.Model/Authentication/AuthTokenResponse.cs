using System;
using System.Collections.Generic;
using System.Text;

namespace GloboCrypto.Model.Authentication
{
    public enum AuthTokenResponseResult
    {
        Success,
        Fail
    }

    public class AuthToken
    {
        public string Value { get; set; }
        public DateTime Expiry { get; set; }
        public bool HasExpired => (DateTime.Now > this.Expiry);
    }

    public class AuthTokenResponse
    {
        public AuthTokenResponseResult Result { get; set; }
        public Exception? Error { get; set; }
        public AuthToken? Token { get; set; }
    }
}
