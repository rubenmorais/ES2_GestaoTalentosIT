using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses
{
    public class TalentoHabilidadeDTO
    {
        public int TalentoId { get; set; }
        public int HabilidadeId { get; set; }
        public string NomeHabilidade { get; set; }
        public int AnosExperiencia { get; set; }
    }
}