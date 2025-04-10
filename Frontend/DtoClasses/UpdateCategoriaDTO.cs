using System.ComponentModel.DataAnnotations;

namespace Frontend.DtoClasses;

public class UpdateCategoriaDTO
{
    [Required(ErrorMessage = "O Id da categoria é obrigatória.")]
    public int CategoriaId { get; set; }
    [Required(ErrorMessage = "A categoria é obrigatória."), MaxLength(50)]
    public string Categoria { get; set; }
}