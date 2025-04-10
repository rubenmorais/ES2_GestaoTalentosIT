using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses {
    public class HabilidadeDTO
    {
        public int Habilidadeid { get; set; }
        [Required] 
        public string Nome { get; set; } = null!;
        [Required] 
        public int Categoriaid { get; set; }
        [Required] 
        public int Criadorid { get; set; }
    }
}