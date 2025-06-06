using System.Collections.Generic;

namespace Frontend.DTOClasses
{
    public class TalentoDTO
    {
        public int Talentoid { get; set; }
        public int UtilizadorId { get; set; }
        public string Nome { get; set; }
        public string Pais { get; set; }
        public string Email { get; set; }
        public decimal PrecoPorHora { get; set; }
        public bool Visibilidade { get; set; }
        public List<TalentoHabilidadeDTO> Habilidades { get; set; } = new List<TalentoHabilidadeDTO>();
    }
}
