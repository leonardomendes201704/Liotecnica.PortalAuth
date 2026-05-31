using OpenIddict.Abstractions;

namespace Liotecnica.PortalAuth.Web.Services;

public static class OpenIddictClientSeeder
{
    public const string OpenFIIsClientId = "openfiis-local";
    public const string OpenFIIsClientSecret = "openfiis-local-dev-secret";

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var applicationManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
        var descriptor = CreateOpenFIIsDescriptor();

        var application = await applicationManager.FindByClientIdAsync(OpenFIIsClientId);

        if (application is not null)
        {
            await applicationManager.UpdateAsync(application, descriptor);
            return;
        }

        await applicationManager.CreateAsync(descriptor);
    }

    private static OpenIddictApplicationDescriptor CreateOpenFIIsDescriptor()
    {
        return new OpenIddictApplicationDescriptor
        {
            ClientId = OpenFIIsClientId,
            ClientSecret = OpenFIIsClientSecret,
            ClientType = OpenIddictConstants.ClientTypes.Confidential,
            ConsentType = OpenIddictConstants.ConsentTypes.Implicit,
            DisplayName = "OpenFIIs Local",
            RedirectUris =
            {
                new Uri("http://localhost:3000/api/auth/callback/portalauth"),
                new Uri("http://127.0.0.1:3000/api/auth/callback/portalauth"),
                new Uri("http://localhost:3001/api/auth/callback/portalauth"),
                new Uri("http://127.0.0.1:3001/api/auth/callback/portalauth")
            },
            PostLogoutRedirectUris =
            {
                new Uri("http://localhost:3000/login"),
                new Uri("http://127.0.0.1:3000/login"),
                new Uri("http://localhost:3001/login"),
                new Uri("http://127.0.0.1:3001/login")
            },
            Permissions =
            {
                OpenIddictConstants.Permissions.Endpoints.Authorization,
                OpenIddictConstants.Permissions.Endpoints.EndSession,
                OpenIddictConstants.Permissions.Endpoints.Token,
                OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                OpenIddictConstants.Permissions.ResponseTypes.Code,
                OpenIddictConstants.Permissions.Scopes.Email,
                OpenIddictConstants.Permissions.Scopes.Profile,
                OpenIddictConstants.Permissions.Scopes.Roles,
                OpenIddictConstants.Permissions.Prefixes.Scope + "department",
                OpenIddictConstants.Permissions.Prefixes.Scope + "permissions"
            },
            Requirements =
            {
                OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
            }
        };
    }
}
