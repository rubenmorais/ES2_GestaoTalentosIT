using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses {
    public class ExperienciasDTO {
        public int ExperienciaId { get; set; }

        [Required]
        public int TalentoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(100)]
        public string Empresa { get; set; }

        [Range(1900, 2100)]
        public int AnoInicio { get; set; }

        [Range(1900, 2100)]
        public int? AnoFim { get; set; }
    }
}

