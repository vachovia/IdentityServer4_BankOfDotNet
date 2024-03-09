using IdentityServer4.Models;

namespace BankOfDotNet.IdentityServer.ServerConfiguration
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetAllApiResources()
        {
            var apiResources = new List<ApiResource> {
                new ApiResource("bankOfDotNetApi", "Customer Api for BankOfDotNet") // { Scopes = { "bankOfDotNetApi" } }
                //new ApiResource("resourcesScope", "My API") {
                //    UserClaims = { "role" }, // doesn't work without this property
                //    Scopes = { "resourcesScope" }
                //}
            };

            return apiResources;
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
             {
                 new ApiScope("bankOfDotNetApi")
             };
        }

        public static IEnumerable<Client> GetClients()
        {
            var clients = new List<Client>
            {
                new Client() {
                    ClientId = "vlad",
                    AllowedGrantTypes = GrantTypes.ClientCredentials, // server to server when there is a trust
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256()) // { Value = "secret".Sha256()}
                    },
                    AllowedScopes = { "bankOfDotNetApi" }
                }
            };

            return clients;
        }
    }
}

// GrantTypes.ClientCredentials => // server to server when there is a trust but not for browsers when we have to store "vlad" and "secret" (machine to machine or 1st party sources => no user involved)
// GrantTypes.ResourceOwnerPassword => client should send not only clientId but Username/Password (user involverd => SPAs, js,..)
// GrantTypes.Code => google, facebook => authorization send by google to let app access (user involved, web app) => auth code flow
// GrantTypes.Implicit => client redirects the user to directly IS4 => user enters Creds => then IS4 provides Consent page where user asked Do you approve Client? (user involved, browser apps)
// GrantTypes.Hybrid => Combination of Implicit and Autohrization Code (user involved, browser apps)