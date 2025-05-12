using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses
{
    public class UpdatePropostaTrabalhoDTO
    {
        [Required, MaxLength(100)]
        public string Nome { get; set; }
        
        [Required]
        public int CategoriaId { get; set; }
        
        [Required, Range(1, int.MaxValue)]
        public int TotalHoras { get; set; }
        
        [MaxLength(500)]
        public string Descricao { get; set; }
    }
}