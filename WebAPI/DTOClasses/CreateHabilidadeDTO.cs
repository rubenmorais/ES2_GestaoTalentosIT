using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses
{
    public class CreateHabilidadeDTO
    {
        [Required, MaxLength(50)]
        public string Nome { get; set; } 
        
        [Required]
        public int Categoriaid { get; set; }
        
        [Required]
        public int Criadorid { get; set; }
    }
}    