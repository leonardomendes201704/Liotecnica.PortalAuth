using Microsoft.AspNetCore.Http;

namespace Liotecnica.BuildingBlocks.Web.Middleware;

public sealed class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public SecurityHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.TryAdd("X-Content-Type-Options", "nosniff");
        context.Response.Headers.TryAdd("X-Frame-Options", "DENY");
        context.Response.Headers.TryAdd("Referrer-Policy", "no-referrer");
        context.Response.Headers.TryAdd("Permissions-Policy", "geolocation=(), microphone=(), camera=()");

        if (!context.Request.Path.StartsWithSegments("/swagger"))
        {
            context.Response.Headers.TryAdd(
                "Content-Security-Policy",
                "default-src 'self'; img-src 'self' data:; style-src 'self' 'unsafe-inline'; script-src 'self'; frame-ancestors 'none'; base-uri 'self'; form-action 'self'");
        }

        await _next(context);
    }
}
