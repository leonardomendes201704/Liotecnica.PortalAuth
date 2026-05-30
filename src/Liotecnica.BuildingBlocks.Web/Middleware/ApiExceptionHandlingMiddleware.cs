using Liotecnica.BuildingBlocks.Api.Responses;
using Liotecnica.BuildingBlocks.Logging.Correlation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Liotecnica.BuildingBlocks.Web.Middleware;

public sealed class ApiExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiExceptionHandlingMiddleware> _logger;

    public ApiExceptionHandlingMiddleware(RequestDelegate next, ILogger<ApiExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, ICorrelationIdAccessor accessor)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Unhandled API exception. CorrelationId: {CorrelationId}",
                accessor.CorrelationId);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = ApiResponse<object>.Fail(
                new ApiError(
                    "UNEXPECTED_ERROR",
                    "Ocorreu um erro inesperado. Informe o codigo de rastreio ao suporte."),
                accessor.CorrelationId);

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
