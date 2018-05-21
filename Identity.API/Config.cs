using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Identity.API
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("gateway-api","网关服务API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    AllowedGrantTypes = new List<string>{ "sms_code"},
                    ClientId = "client_001",
                    ClientSecrets = { new Secret("secret_001".Sha256())},
                    RedirectUris = { "http://localhost:10001/signin-oidc"},
                    PostLogoutRedirectUris = { "http://localhost:10001/signout-callback-oidc" },
                    AllowedScopes =
                    {
                        "gateway-api",
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email
                    },
                    RequireConsent = false,
                    RequireClientSecret = false,
                    AlwaysIncludeUserClaimsInIdToken=true,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    AllowOfflineAccess = true
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };
        }

    }
}
