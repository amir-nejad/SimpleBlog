using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Claims;
using Secret = IdentityServer4.Models.Secret;

namespace SimpleBlog.IdentityServer.IdentityConfiguration
{
    public static class ClientsConfiguration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new("webApi", "Simple Blog API")
            };
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new() {
                    ClientId = "webapi",
                    ClientSecrets = {new Secret("webapi".Sha256()) },
                    ClientUri = "https://localhost:7196",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = false,
                    AllowedCorsOrigins = new string[]{ "https://localhost:7196"},
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new string[]{"https://localhost:7196/swagger/oauth2-redirect.html" },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true,
                    AllowedScopes = { "openid", "profile", "webApi", "roles" }
                }
            };
    }
}
