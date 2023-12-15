using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.DataProtection;
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
                // interactive client using code flow + pkce
                //new Client
                //{
                //    ClientId = "interactive",
                //    ClientSecrets = { new Secret("929E5844-AF76-41BE-8F17-2FA426239E71".Sha256()) },

                //    AllowedGrantTypes = GrantTypes.Code,

                //    RedirectUris = { "https://localhost:7196/signin-oidc" },
                //    FrontChannelLogoutUri = "https://localhost:7196/signout-oidc",
                //    PostLogoutRedirectUris = { "https://localhost:7196/signout-callback-oidc" },

                //    AllowOfflineAccess = true,
                //    AllowedScopes = { "openid", "profile" }
                //},
                new() {
                    ClientId = "webapi",
                    //ClientSecrets = {new Secret("008AD75B-32C6-4241-BE0F-AA1F5972A2D2".Sha256()) },
                    ClientSecrets = {new Secret("webapi".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    AccessTokenType = AccessTokenType.Jwt,
                    RedirectUris = new List<string>{"https://localhost:7196/swagger/oauth2-redirect.html" },
                    Claims = new List<ClientClaim>(){ new ClientClaim("role", "Administrator") },
                    AllowedScopes = { "webApi" }
                }
            };
    }
}
