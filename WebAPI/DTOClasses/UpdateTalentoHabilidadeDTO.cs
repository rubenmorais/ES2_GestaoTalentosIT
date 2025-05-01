using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses
{
    public class UpdateTalentoHabilidadeDTO
    {
        [Required]
        [Range(0, 50, ErrorMessage = "Os anos de experiência devem estar entre 0 e 50.")]
        public int AnosExperiencia { get; set; }
    }
}