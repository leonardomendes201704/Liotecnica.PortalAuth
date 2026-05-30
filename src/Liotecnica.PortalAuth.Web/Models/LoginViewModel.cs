using System.ComponentModel.DataAnnotations;

namespace Liotecnica.PortalAuth.Web.Models;

public sealed class LoginViewModel
{
    [Required(ErrorMessage = "Informe o e-mail corporativo.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail valido.")]
    [Display(Name = "E-mail corporativo")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a senha.")]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Lembrar-me")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}
