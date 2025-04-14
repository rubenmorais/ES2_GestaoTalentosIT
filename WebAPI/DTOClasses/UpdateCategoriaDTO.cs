using System.ComponentModel.DataAnnotations;

namespace WebAPI.DtoClasses;

public class UpdateCategoriaDTO
{
    [Required(ErrorMessage = "A categoria é obrigatória."), MaxLength(50)]
    public string Categoria { get; set; }
}