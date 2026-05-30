using Liotecnica.BuildingBlocks.Logging.Correlation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Liotecnica.BuildingBlocks.Web.Middleware;

public sealed class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ICorrelationIdAccessor accessor)
    {
        var correlationId = ResolveCorrelationId(context.Request.Headers[CorrelationIdDefaults.HeaderName]);
        accessor.CorrelationId = correlationId;

        context.Response.Headers[CorrelationIdDefaults.HeaderName] = correlationId;

        try
        {
            await _next(context);
        }
        finally
        {
            accessor.CorrelationId = null;
        }
    }

    private static string ResolveCorrelationId(StringValues headerValue)
    {
        return string.IsNullOrWhiteSpace(headerValue)
            ? Guid.NewGuid().ToString("N")
            : headerValue.ToString();
    }
}
