using System.ComponentModel.DataAnnotations;

namespace Frontend.DtoClasses;

public class UpdateHabilidadeDTO
{
    public int Habilidadeid { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório."), MaxLength(100, ErrorMessage = "O nome não pode ter mais de 100 carateres.")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "A categoria é obrigatória."), MaxLength(50, ErrorMessage = "O país não pode ter mais de 50 carateres.")]
    public int Categoriaid { get; set; }
    [Required(ErrorMessage = "O criador é obrigatório."), MaxLength(100, ErrorMessage = "Id inválido.")]
    public int Criadorid { get; set; }
}