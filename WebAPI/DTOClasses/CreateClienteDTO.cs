using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses {
    public class CreateClienteDTO {
        [Required(ErrorMessage = "O utilizador é obrigatório.")]
        public int UtilizadorId { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O nome não pode exceder 50 carateres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
        [MaxLength(100, ErrorMessage = "O email não pode exceder 100 carateres.")]
        public string Email { get; set; }
    }
}