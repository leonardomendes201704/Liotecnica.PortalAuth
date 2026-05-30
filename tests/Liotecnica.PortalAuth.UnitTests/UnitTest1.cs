using Liotecnica.BuildingBlocks.Api.Responses;
using Liotecnica.PortalAuth.Application.Enums;
using Liotecnica.PortalAuth.Application.Services;
using Liotecnica.PortalAuth.Domain.Common;
using Liotecnica.PortalAuth.Domain.Entities;

namespace Liotecnica.PortalAuth.UnitTests;

public class FoundationTests
{
    [Fact]
    public void ApiResponseOk_ShouldCreateSuccessfulEnvelope()
    {
        var response = ApiResponse<string>.Ok("online", "Operacao realizada.", "abc-123");

        Assert.True(response.Success);
        Assert.Equal("online", response.Data);
        Assert.Equal("Operacao realizada.", response.Message);
        Assert.Equal("abc-123", response.CorrelationId);
        Assert.Null(response.Error);
    }

    [Fact]
    public void PagedResult_ShouldCalculateTotalPages()
    {
        var result = new PagedResult<int>([1, 2, 3], 1, 2, 5);

        Assert.Equal(3, result.TotalPages);
    }

    [Fact]
    public void BaseEntity_ShouldTrackSoftDelete()
    {
        var entity = new TestEntity();

        entity.MarkAsDeleted("tester");

        Assert.True(entity.IsDeleted);
        Assert.Equal("tester", entity.UpdatedBy);
        Assert.NotNull(entity.UpdatedAt);
    }

    [Fact]
    public void PlatformStatusService_ShouldReturnOperationalStatus()
    {
        var service = new PlatformStatusService();

        var status = service.GetStatus("Development");

        Assert.Equal("Liotecnica.PortalAuth.Api", status.Application);
        Assert.Equal("Development", status.Environment);
        Assert.Equal(PlatformComponentStatus.Operational, status.Status);
    }

    [Fact]
    public void CorporateSystem_ShouldStartActive()
    {
        var system = new CorporateSystem(
            "Portal Auth",
            "PORTAL_AUTH",
            "Portal corporativo de autenticacao",
            "https://portal.local",
            null,
            requiresMfa: false);

        Assert.True(system.IsActive);
        Assert.Equal("PORTAL_AUTH", system.Code);
        Assert.False(system.RequiresMfa);
    }

    private sealed class TestEntity : BaseEntity;
}