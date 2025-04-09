using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses;

public class CreateUtilizadorDTO {
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O email não é válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A palavra-passe é obrigatória.")]
    [MinLength(6, ErrorMessage = "A palavra-passe deve ter pelo menos 6 caracteres.")]
    public string PalavraPasse { get; set; }

    [Required(ErrorMessage = "O tipo de utilizador é obrigatório.")]
    public int Tipoid { get; set; }
}