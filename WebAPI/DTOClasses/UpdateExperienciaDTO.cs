using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses {
    public class UpdateExperienciaDTO : IValidatableObject {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A empresa é obrigatória.")]
        [StringLength(100, ErrorMessage = "A empresa deve ter no máximo 100 caracteres.")]
        public string Empresa { get; set; }

        [Required(ErrorMessage = "O ano de início é obrigatório.")]
        [Range(1900, 2100, ErrorMessage = "O ano de início deve estar entre 1900 e 2100.")]
        public int AnoInicio { get; set; }

        [Range(1900, 2100, ErrorMessage = "O ano de fim deve estar entre 1900 e 2100.")]
        public int? AnoFim { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AnoFim.HasValue && AnoFim < AnoInicio)
            {
                yield return new ValidationResult(
                    "O ano de fim não pode ser anterior ao ano de início.",
                    new[] { nameof(AnoFim) }
                );
            }
        }
    }
}