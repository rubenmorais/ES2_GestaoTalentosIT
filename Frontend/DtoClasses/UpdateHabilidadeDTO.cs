using System.ComponentModel.DataAnnotations;

namespace Frontend.DtoClasses;

public class UpdateHabilidadeDTO
{
    [Required]
    public int Habilidadeid { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(50, ErrorMessage = "O nome não pode ter mais de 15 carateres.")]
    public string Nome { get; set; }
    public int Categoriaid { get; set; }
}
