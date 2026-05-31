using Liotecnica.BuildingBlocks.Observability.Extensions;
using Liotecnica.BuildingBlocks.Web.Extensions;
using Liotecnica.PortalAuth.Infrastructure;
using Liotecnica.PortalAuth.Infrastructure.Identity;
using Liotecnica.PortalAuth.Infrastructure.Persistence;
using Liotecnica.PortalAuth.Web.Security;
using Liotecnica.PortalAuth.Web.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPortalAuthInfrastructure(builder.Configuration);
builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<PortalAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.SlidingExpiration = true;
});

builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IAuditRetentionService, AuditRetentionService>();
builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore()
            .UseDbContext<PortalAuthDbContext>()
            .ReplaceDefaultEntities<Guid>();
    })
    .AddServer(options =>
    {
        options.SetAuthorizationEndpointUris("/connect/authorize")
            .SetTokenEndpointUris("/connect/token")
            .SetUserInfoEndpointUris("/connect/userinfo")
            .SetEndSessionEndpointUris("/connect/logout");

        options.AllowAuthorizationCodeFlow()
            .AllowRefreshTokenFlow();

        options.RegisterScopes(
            OpenIddictConstants.Scopes.Email,
            OpenIddictConstants.Scopes.Profile,
            OpenIddictConstants.Scopes.Roles,
            OpenIddictConstants.Scopes.OfflineAccess,
            "permissions",
            "department");

        options.AddDevelopmentEncryptionCertificate()
            .AddDevelopmentSigningCertificate();

        options.IgnoreEndpointPermissions();

        var aspNetCore = options.UseAspNetCore()
            .EnableAuthorizationEndpointPassthrough()
            .EnableTokenEndpointPassthrough()
            .EnableUserInfoEndpointPassthrough()
            .EnableEndSessionEndpointPassthrough();

        if (builder.Environment.IsDevelopment())
        {
            aspNetCore.DisableTransportSecurityRequirement();
        }
    });
builder.Services.AddLiotecnicaWebBuildingBlocks();
builder.Services.AddLiotecnicaHealthChecks();
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("Liotecnica.PortalAuth.Web"))
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation();

        var otlpEndpoint = builder.Configuration["OpenTelemetry:OtlpEndpoint"];

        if (!string.IsNullOrWhiteSpace(otlpEndpoint))
        {
            tracing.AddOtlpExporter(options => options.Endpoint = new Uri(otlpEndpoint));
        }
    });
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            context.User.Identity?.Name ?? context.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 120,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));
});
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCorrelationId();
app.UseSecurityHeaders();
app.UseRequestLogging();

app.UseRouting();
app.UseRateLimiter();

app.UseAuthentication();
app.Use(async (context, next) =>
{
    var path = context.Request.Path;
    var isAuthenticated = context.User.Identity?.IsAuthenticated == true;
    var isAllowedPath = path.StartsWithSegments("/Account/ChangePassword")
        || path.StartsWithSegments("/Account/Logout")
        || path.StartsWithSegments("/css")
        || path.StartsWithSegments("/js")
        || path.StartsWithSegments("/lib")
        || path.StartsWithSegments("/health");

    if (isAuthenticated && !isAllowedPath)
    {
        var userManager = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
        var user = await userManager.GetUserAsync(context.User);

        if (user?.MustChangePassword == true)
        {
            context.Response.Redirect("/Account/ChangePassword?forced=true");
            return;
        }
    }

    await next();
});
app.UseAuthorization();

app.MapHealthChecks("/health");
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("live")
});
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<PortalAuthDbContext>();

    await dbContext.Database.MigrateAsync();
    await IdentitySeeder.SeedAsync(app.Services);
    await OpenIddictClientSeeder.SeedAsync(app.Services);
}

app.Run();
