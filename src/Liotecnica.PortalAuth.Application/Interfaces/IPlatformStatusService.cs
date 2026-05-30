using Liotecnica.PortalAuth.Application.Models;

namespace Liotecnica.PortalAuth.Application.Interfaces;

public interface IPlatformStatusService
{
    PlatformStatusModel GetStatus(string environmentName);
}
