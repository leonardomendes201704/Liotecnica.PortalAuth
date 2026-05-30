using Liotecnica.PortalAuth.Application.Models;
using MediatR;

namespace Liotecnica.PortalAuth.Application.Features.PlatformStatus.Queries;

public sealed record GetPlatformStatusQuery(string EnvironmentName) : IRequest<PlatformStatusModel>;
