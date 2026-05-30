namespace Liotecnica.BuildingBlocks.Api.Responses;

public sealed record ApiError(
    string Code,
    string Message,
    IReadOnlyCollection<ApiErrorDetail>? Details = null);

public sealed record ApiErrorDetail(
    string? Field,
    string Message);
