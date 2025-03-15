using System.ComponentModel.DataAnnotations;

namespace Frontend.DtoClasses;
public class LoginDTO
{
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Por favor, insira um e-mail válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A palavra-passe é obrigatória.")]
    public string PalavraPasse { get; set; }
}