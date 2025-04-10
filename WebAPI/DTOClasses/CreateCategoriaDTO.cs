using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses {
    public class CreateCategoriaDTO {
        [Required(ErrorMessage = "A categoria é obrigatória."), MaxLength(50)]
        public string Categoria { get; set; }
    }
}
