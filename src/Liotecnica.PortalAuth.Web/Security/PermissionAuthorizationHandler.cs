using System.Security.Claims;
using Liotecnica.PortalAuth.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Liotecnica.PortalAuth.Web.Security;

public sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly PortalAuthDbContext _dbContext;

    public PermissionAuthorizationHandler(PortalAuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userIdValue = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdValue, out var userId))
        {
            return;
        }

        var hasPermission = await (
            from userRole in _dbContext.UserRoles
            join rolePermission in _dbContext.RolePermissions on userRole.RoleId equals rolePermission.RoleId
            join permission in _dbContext.Permissions on rolePermission.PermissionId equals permission.Id
            where userRole.UserId == userId
                  && permission.Code == requirement.PermissionCode
                  && permission.IsActive
                  && !permission.IsDeleted
            select permission.Id)
            .AnyAsync();

        if (hasPermission)
        {
            context.Succeed(requirement);
        }
    }
}
