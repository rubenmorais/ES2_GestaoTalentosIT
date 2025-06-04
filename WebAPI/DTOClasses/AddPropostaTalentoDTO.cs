using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses
{
    public class AddPropostaTalentoDTO
    {
        [Required]
        public int TalentoId { get; set; }
        
        [Required]
        public int PropostaId { get; set; }
    }
}