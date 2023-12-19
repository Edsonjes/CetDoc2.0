using Duende.IdentityServer;
using Duende.IdentityServer.Models;


namespace IdentityService.Configurations
{
	public static class IdentityConfiguration
	{
		public const string Admin = "Admin";
		public const string Client = "Client";

		public static IEnumerable<IdentityResource> IdentityResources =>
			new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
				new IdentityResources.Email(),
				
			};

		public static IEnumerable<ApiScope> ApiScopes =>
			new List<ApiScope>
			{
				new ApiScope("CetDocsApp", "Cet Docs"),
                 new ApiScope(name: "read", "Read data."),
                new ApiScope(name: "write", "Write data."),
                new ApiScope(name: "delete", "Delete data."),

            };

		public static IEnumerable<Client> Clients =>
			new List<Client>
			{
				new Client
				{
					ClientId = "client",
					ClientSecrets = { new Secret("CriptoRash".Sha256()) },
					AllowedGrantTypes = GrantTypes.ClientCredentials,
					AllowedScopes = { "read","write","profile" }
				},
				new Client
				{
					ClientId = "CetDocsApp",
					ClientSecrets = { new Secret("CriptoRash".Sha256()) },
					RedirectUris = { "https://localhost:4430/signin-oidc" },
					PostLogoutRedirectUris = { "https://localhost:4430/signout-callback-oidc" },
					AllowedGrantTypes = GrantTypes.Code,
					AllowedScopes = new List<string>
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.Email,
						"CetDocsApp"
					},
				}
			};

	}
}
