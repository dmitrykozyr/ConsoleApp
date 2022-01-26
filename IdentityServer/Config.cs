using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> Apis
        {
            get
            {
                return new List<ApiResource>
                {
                    new ApiResource("hps-api", "H+ Sport API")
                };
            }
        }

        public static IEnumerable<Client> Clients
        {
            get
            {
                return new List<Client>
                {
                    new Client
                    {
                        ClientId = "client",
                        AllowedScopes = { "hps-api" },
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets =
                        {
                            // Пароль
                            new Secret("H+ Sport".Sha256())
                        }
                    }
                };
            }
        }

        public static IEnumerable<ApiScope> Scopes
        {
            get
            {
                return new List<ApiScope>
                {
                    new ApiScope("hps-api")
                };
            }
        }
    }
}
