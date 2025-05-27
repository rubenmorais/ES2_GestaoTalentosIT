using System.ComponentModel.DataAnnotations;
namespace Frontend.DTOClasses
{
    public class UpdatePropostaHabilidadeDTO
    {
        [Required]
        [Range(0, 50, ErrorMessage = "O mínimo de anos deve estar entre 0 e 50")]
        public int MinAnosExp { get; set; }
    }
}