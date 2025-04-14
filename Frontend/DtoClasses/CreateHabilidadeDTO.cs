using System.ComponentModel.DataAnnotations;

namespace Frontend.DtoClasses;

public class CreateHabilidadeDTO
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(50, ErrorMessage = "O nome não pode ter mais de 50 carateres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A categoria é obrigatória.")]
    public int Categoriaid { get; set; }
    public int Criadorid { get; set; }
}