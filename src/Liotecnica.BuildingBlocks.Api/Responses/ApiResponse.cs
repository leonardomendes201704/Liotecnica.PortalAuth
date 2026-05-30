namespace Liotecnica.BuildingBlocks.Api.Responses;

public sealed record ApiResponse<T>(
    bool Success,
    T? Data,
    string? Message,
    ApiError? Error,
    string? CorrelationId)
{
    public static ApiResponse<T> Ok(T data, string? message = null, string? correlationId = null)
    {
        return new ApiResponse<T>(true, data, message, null, correlationId);
    }

    public static ApiResponse<T> Fail(ApiError error, string? correlationId = null)
    {
        return new ApiResponse<T>(false, default, null, error, correlationId);
    }
}
