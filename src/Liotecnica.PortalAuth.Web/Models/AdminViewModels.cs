using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Liotecnica.PortalAuth.Web.Models;

public sealed class CorporateSystemFormViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Informe o nome do sistema.")]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o codigo do sistema.")]
    [MaxLength(80)]
    public string Code { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Informe a URL base.")]
    [MaxLength(500)]
    public string BaseUrl { get; set; } = string.Empty;

    [MaxLength(120)]
    public string? Icon { get; set; }

    public bool IsActive { get; set; } = true;
    public bool RequiresMfa { get; set; }
}

public sealed class PermissionFormViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Informe o codigo da permissao.")]
    [MaxLength(120)]
    public string Code { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a descricao.")]
    [MaxLength(300)]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o modulo.")]
    [MaxLength(80)]
    public string Module { get; set; } = string.Empty;

    public bool IsCritical { get; set; }
    public bool IsActive { get; set; } = true;
}

public sealed class RoleFormViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Informe o nome do perfil.")]
    [MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    public List<Guid> SelectedPermissionIds { get; set; } = [];
    public List<Guid> SelectedSystemIds { get; set; } = [];
    public List<SelectListItem> Permissions { get; set; } = [];
    public List<SelectListItem> Systems { get; set; } = [];
}

public sealed class UserCreateViewModel
{
    [Required(ErrorMessage = "Informe o nome.")]
    [MaxLength(150)]
    public string DisplayName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o e-mail.")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [MaxLength(120)]
    public string? Department { get; set; }

    [Required(ErrorMessage = "Informe a senha inicial.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public List<Guid> SelectedRoleIds { get; set; } = [];
    public List<SelectListItem> Roles { get; set; } = [];
}

public sealed class UserEditViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Informe o nome.")]
    [MaxLength(150)]
    public string DisplayName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o e-mail.")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [MaxLength(120)]
    public string? Department { get; set; }

    public bool IsLocked { get; set; }
    public List<Guid> SelectedRoleIds { get; set; } = [];
    public List<SelectListItem> Roles { get; set; } = [];
}

public sealed class UserResetPasswordViewModel
{
    public Guid Id { get; set; }

    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a nova senha temporaria.")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirme a nova senha temporaria.")]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword), ErrorMessage = "A confirmacao deve ser igual a nova senha.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
