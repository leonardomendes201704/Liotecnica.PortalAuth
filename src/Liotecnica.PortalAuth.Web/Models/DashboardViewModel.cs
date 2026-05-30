namespace Liotecnica.PortalAuth.Web.Models;

public sealed class DashboardViewModel
{
    public string DisplayName { get; init; } = "Usuario";
    public string Department { get; init; } = "Portal Corporativo";
    public IReadOnlyCollection<SystemCardViewModel> Systems { get; init; } = [];
    public IReadOnlyCollection<NoticeViewModel> Notices { get; init; } = [];
}

public sealed record SystemCardViewModel(
    string Name,
    string Description,
    string Icon,
    string Accent,
    string BaseUrl);

public sealed record NoticeViewModel(
    string Title,
    string Date);
