namespace Liotecnica.PortalAuth.Domain.Entities;

public sealed class RoleSystemAccess
{
    private RoleSystemAccess()
    {
    }

    public RoleSystemAccess(Guid roleId, Guid systemId)
    {
        RoleId = roleId;
        SystemId = systemId;
    }

    public Guid RoleId { get; private set; }
    public Guid SystemId { get; private set; }
    public CorporateSystem System { get; private set; } = null!;
}
