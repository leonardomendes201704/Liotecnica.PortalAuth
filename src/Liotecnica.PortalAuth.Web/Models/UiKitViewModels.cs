namespace Liotecnica.PortalAuth.Web.Models;

public sealed record BreadcrumbItemViewModel(
    string Label,
    string? Controller = null,
    string? Action = null,
    bool IsCurrent = false);
