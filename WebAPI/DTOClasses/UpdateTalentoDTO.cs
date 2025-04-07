using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses
{
    public class UpdateTalentoDTO
    {
        [Required, MaxLength(100)]
        public string Nome { get; set; }

        [Required, MaxLength(50)]
        public string Pais { get; set; }
        
        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }
        
        [Required, Range(0, double.MaxValue)]
        public decimal PrecoPorHora { get; set; }

        public bool Visibilidade { get; set; } = true; 
    }
}