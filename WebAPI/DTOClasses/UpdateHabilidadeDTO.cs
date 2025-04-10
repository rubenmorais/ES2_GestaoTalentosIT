using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses
{

    public class UpdateHabilidadeDTO
    {
        [Required, MaxLength(50)] 
        public string Nome { get; set; }
        
        [Required, MaxLength(50)] 
        public int Categoriaid { get; set; }
        
        [Required, MaxLength(50)] 
        public int Criadorid { get; set; }
    }
}    