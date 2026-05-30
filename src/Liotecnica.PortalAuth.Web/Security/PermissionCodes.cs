namespace Liotecnica.PortalAuth.Web.Security;

public static class PermissionCodes
{
    public const string SystemView = "Sistema.Visualizar";
    public const string SystemCreate = "Sistema.Criar";
    public const string SystemEdit = "Sistema.Editar";
    public const string PermissionManage = "Permissao.Gerenciar";
    public const string RoleManage = "Perfil.Gerenciar";
    public const string UserManage = "Usuario.Gerenciar";
    public const string AuditView = "Auditoria.Visualizar";

    public static readonly IReadOnlySet<string> All = new HashSet<string>
    {
        SystemView,
        SystemCreate,
        SystemEdit,
        PermissionManage,
        RoleManage,
        UserManage,
        AuditView
    };
}
