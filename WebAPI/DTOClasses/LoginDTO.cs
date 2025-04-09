using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses {
    public class LoginDTO {
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A palavra-passe é obrigatória.")]
        public string PalavraPasse { get; set; }
    }
}