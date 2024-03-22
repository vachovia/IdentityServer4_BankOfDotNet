using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace BankOfDotNet.IdentityServer.SeedData
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources => new[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource {
                Name = "role",
                UserClaims = new List<string> { "role" }
            }
        };

        public static IEnumerable<ApiResource> GetAllApiResources => new[]
        {
            new ApiResource("bankOfDotNetApi", "Customer Api for BankOfDotNet")
        };

        public static IEnumerable<ApiScope> GetApiScopes => new[]
        {
            new ApiScope("bankOfDotNetApi")
        };

        public static IEnumerable<Client> GetClients => new[]
        {
            new Client() {
                ClientId = "vlad",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new List<Secret>
                {
                    new Secret{ Value = "secret".Sha256()}
                },
                AllowedScopes = { "bankOfDotNetApi" }
            },
            new Client() {
                ClientId = "petrosyan",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = new List<Secret>
                {
                    new Secret("secret".Sha256())
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
                    new Secret{ Value = "secret".Sha256()}
                },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            },
            new Client() {
                ClientId = "swaggerapiui",
                ClientName = "Swagger API UI",
                AllowedGrantTypes = GrantTypes.Implicit,                
                RedirectUris = {"http://localhost:5001/swagger/oauth2-redirect.html"},
                PostLogoutRedirectUris = { "https://localhost:7002/swagger" },
                AllowedScopes = { "bankOfDotNetApi" },
                ClientSecrets = new List<Secret>
                {
                    new Secret("secret".Sha256())
                },
                AllowAccessTokensViaBrowser = true
            },
        };

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
    }
}

// GrantTypes.ClientCredentials => // server to server when there is a trust but not for browsers when we have to store "vlad" and "secret" (machine to machine or 1st party sources => no user involved)
// GrantTypes.ResourceOwnerPassword => client should send not only clientId but Username/Password (user involverd => SPAs, js,..)
// GrantTypes.Code => google, facebook => authorization send by google to let app access (user involved, web app) => auth code flow
// GrantTypes.Implicit => client redirects the user to directly IS4 => user enters Creds => then IS4 provides Consent page where user asked Do you approve Client? (user involved, browser apps)
// GrantTypes.Hybrid => Combination of Implicit and Autohrization Code (user involved, browser apps)