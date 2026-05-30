namespace Liotecnica.PortalAuth.Web.Models;

public sealed class ChangelogViewModel
{
    public IReadOnlyCollection<ChangelogEntryViewModel> Entries { get; init; } = [];
}

public sealed record ChangelogEntryViewModel(
    string Version,
    string Title,
    string Date,
    string Summary,
    IReadOnlyCollection<string> Changes,
    IReadOnlyCollection<string> TechnicalNotes);
