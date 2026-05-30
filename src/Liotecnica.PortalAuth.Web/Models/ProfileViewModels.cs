using System.ComponentModel.DataAnnotations;

namespace Liotecnica.PortalAuth.Web.Models;

public sealed class ProfileViewModel
{
    public string DisplayName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? Department { get; init; }
    public bool MustChangePassword { get; init; }
}

public sealed class ChangePasswordViewModel
{
    [Required(ErrorMessage = "Informe a senha atual.")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a nova senha.")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirme a nova senha.")]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword), ErrorMessage = "A confirmacao deve ser igual a nova senha.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
