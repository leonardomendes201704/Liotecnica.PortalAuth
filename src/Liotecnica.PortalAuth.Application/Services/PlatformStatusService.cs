using Liotecnica.PortalAuth.Application.Enums;
using Liotecnica.PortalAuth.Application.Interfaces;
using Liotecnica.PortalAuth.Application.Models;

namespace Liotecnica.PortalAuth.Application.Services;

public sealed class PlatformStatusService : IPlatformStatusService
{
    public PlatformStatusModel GetStatus(string environmentName)
    {
        return new PlatformStatusModel(
            "Liotecnica.PortalAuth.Api",
            environmentName,
            DateTime.UtcNow,
            PlatformComponentStatus.Operational);
    }
}
