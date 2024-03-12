using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace BankOfDotNet.IdentityServer.ServerConfiguration
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>() {
                new TestUser()
                {
                    SubjectId = "1",
                    Username = "Test",
                    Password = "P@ssword1",
                },
                new TestUser()
                {
                    SubjectId = "2",
                    Username = "Bob",
                    Password = "P@ssword1",
                }
            };
        }

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
                 new ApiScope("bankOfDotNetApi"),
                 //new ApiScope(IdentityServerConstants.StandardScopes.OpenId),
                 //new ApiScope(IdentityServerConstants.StandardScopes.Profile)
             };
        }

        public static IEnumerable<Client> GetClients()
        {
            var clients = new List<Client>
            {
                new Client() {
                    ClientId = "vlad",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256()) // { Value = "secret".Sha256()}
                    },
                    AllowedScopes = { "bankOfDotNetApi" }
                },
                new Client() {
                    ClientId = "petrosyan",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256()) // { Value = "secret".Sha256()}
                    },
                    AllowedScopes = { "bankOfDotNetApi" }
                },
                new Client() {
                    ClientId = "mvc",
                    ClientName = "Mvc Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = {"https://localhost:7002/signin-oidc"},
                    PostLogoutRedirectUris = { "https://localhost:7002/signout-callback-oidc" },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256()) // { Value = "secret".Sha256()}
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
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