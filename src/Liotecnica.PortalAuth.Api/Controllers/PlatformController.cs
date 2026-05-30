using Liotecnica.BuildingBlocks.Api.Responses;
using Liotecnica.BuildingBlocks.Logging.Correlation;
using Liotecnica.PortalAuth.Application.Features.PlatformStatus.Queries;
using Liotecnica.PortalAuth.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Liotecnica.PortalAuth.Api.Controllers;

[ApiController]
[Route("api/v1/platform")]
public sealed class PlatformController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICorrelationIdAccessor _correlationIdAccessor;
    private readonly IWebHostEnvironment _environment;

    public PlatformController(
        IMediator mediator,
        ICorrelationIdAccessor correlationIdAccessor,
        IWebHostEnvironment environment)
    {
        _mediator = mediator;
        _correlationIdAccessor = correlationIdAccessor;
        _environment = environment;
    }

    [HttpGet("status")]
    [ProducesResponseType(typeof(ApiResponse<PlatformStatusModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<PlatformStatusModel>>> GetStatus(CancellationToken cancellationToken)
    {
        var status = await _mediator.Send(
            new GetPlatformStatusQuery(_environment.EnvironmentName),
            cancellationToken);

        return Ok(ApiResponse<PlatformStatusModel>.Ok(
            status,
            "API operacional.",
            _correlationIdAccessor.CorrelationId));
    }
}
