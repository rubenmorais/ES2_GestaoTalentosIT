using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses
{
    public class AddPropostaHabilidadeDTO
    {
        [Required]
        public int PropostaId { get; set; }
        
        [Required]
        public int HabilidadeId { get; set; }
        
        [Required]
        [Range(0, 50, ErrorMessage = "Os anos mínimos de experiência devem estar entre 0 e 50.")]
        public int MinAnosExp { get; set; }
    }
}