using System.ComponentModel.DataAnnotations;

namespace Frontend.DTOClasses
{
    public class AddPropostaTalentoDTO
    {
        [Required]
        public int PropostaId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Deve selecionar um talento")]
        public int TalentoId { get; set; }
    }
}