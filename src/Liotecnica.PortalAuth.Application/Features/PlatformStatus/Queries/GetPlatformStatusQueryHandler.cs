using Liotecnica.PortalAuth.Application.Interfaces;
using Liotecnica.PortalAuth.Application.Models;
using MediatR;

namespace Liotecnica.PortalAuth.Application.Features.PlatformStatus.Queries;

public sealed class GetPlatformStatusQueryHandler : IRequestHandler<GetPlatformStatusQuery, PlatformStatusModel>
{
    private readonly IPlatformStatusService _platformStatusService;

    public GetPlatformStatusQueryHandler(IPlatformStatusService platformStatusService)
    {
        _platformStatusService = platformStatusService;
    }

    public Task<PlatformStatusModel> Handle(GetPlatformStatusQuery request, CancellationToken cancellationToken)
    {
        var status = _platformStatusService.GetStatus(request.EnvironmentName);

        return Task.FromResult(status);
    }
}
