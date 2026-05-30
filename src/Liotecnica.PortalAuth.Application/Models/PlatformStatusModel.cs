using Liotecnica.PortalAuth.Application.Enums;

namespace Liotecnica.PortalAuth.Application.Models;

public sealed record PlatformStatusModel(
    string Application,
    string Environment,
    DateTime ServerTimeUtc,
    PlatformComponentStatus Status);
