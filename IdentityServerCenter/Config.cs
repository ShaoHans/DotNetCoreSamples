using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerCenter
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("userdeal-api","myapi")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientId = "client_001",
                    ClientSecrets = { new Secret("secret_001".Sha256()) },
                    AllowedScopes = { "userdeal-api" }
                },

                new Client
                {
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientId = "client_002",
                    ClientSecrets = { new Secret("secret_002".Sha256()) },
                    AllowedScopes = { "userdeal-api" },
                    //RequireClientSecret = false
                }

            };
        }

        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "shz",
                    Password = "123456"
                }
            };
        }
    }
}
