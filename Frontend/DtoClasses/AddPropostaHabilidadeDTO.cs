using System.ComponentModel.DataAnnotations;
namespace Frontend.DTOClasses
{
    public class AddPropostaHabilidadeDTO
    {
        [Required]
        public int PropostaId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Deve selecionar uma habilidade")]
        public int HabilidadeId { get; set; }

        [Required]
        [Range(0, 50, ErrorMessage = "O mínimo de anos deve estar entre 0 e 50")]
        public int MinAnosExp { get; set; }
    }
}