using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses
{
    public class CreatePropostaTrabalhoDTO
    {
        [Required(ErrorMessage = "O utilizador é obrigatório.")]
        public int UtilizadorId { get; set; }
        
        [Required(ErrorMessage = "O cliente é obrigatório.")]
        public int ClienteId { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome não pode ter mais de 100 carateres.")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public int CategoriaId { get; set; }
        
        [Required(ErrorMessage = "O total de horas é obrigatório.")]
        [Range(1, 50, ErrorMessage = "O total de horas deve estar entre 0 e 50.")]
        public int TotalHoras { get; set; }
        
        [MaxLength(500, ErrorMessage = "A descrição não pode ter mais de 500 carateres.")]
        public string Descricao { get; set; }
    }
}