using System.ComponentModel.DataAnnotations;

namespace Frontend.DtoClasses
{
    public class CreateUtilizadorDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Por favor, insira um e-mail válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A palavra-passe é obrigatória.")]
        [MinLength(6, ErrorMessage = "A palavra-passe deve ter pelo menos 6 caracteres.")]
        public string PalavraPasse { get; set; }
        public int Tipoid { get; set; }
    }
}