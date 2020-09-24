using System;

namespace THD.Infrastructure.Options
{
    public class AuthTokenProviderOption
    {
        public string JwtKey { get; set; }
        public TimeSpan AccessTokenExpiration { get; set; }
        public TimeSpan RefreshTokenExpiration { get; set; }

        public AuthTokenProviderOption()
        {
            AccessTokenExpiration = TimeSpan.FromMinutes(10);
            RefreshTokenExpiration = TimeSpan.FromDays(60);
        }
    }
}
