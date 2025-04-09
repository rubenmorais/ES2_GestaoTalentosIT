using System.ComponentModel.DataAnnotations;

namespace Frontend.DTOClasses
{
    public class CreateTalentoDTO
    {
        [Required]
        public int UtilizadorId { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório."), MaxLength(100, ErrorMessage = "O nome não pode ter mais de 100 carateres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O país é obrigatório."), MaxLength(50, ErrorMessage = "O país não pode ter mais de 50 carateres.")]
        public string Pais { get; set; }
        [Required(ErrorMessage = "O email é obrigatório."), EmailAddress(ErrorMessage = "O email não é válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O preço por hora é obrigatório."), Range(0, double.MaxValue, ErrorMessage = "O preço por hora deve ser um valor positivo.")]
        public decimal PrecoPorHora { get; set; }
        public bool Visibilidade { get; set; } = true;
    }
}