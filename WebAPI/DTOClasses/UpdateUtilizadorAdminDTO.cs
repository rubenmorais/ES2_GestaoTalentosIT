using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses
{
    public class UpdateUtilizadorAdminDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail fornecido não é válido.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "O tipo de ID é obrigatório.")]
        public int Tipoid { get; set; }
    }
}