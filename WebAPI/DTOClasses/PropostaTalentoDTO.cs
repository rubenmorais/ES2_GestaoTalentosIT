using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses
{
    public class PropostaTalentoDTO
    {
        public int PropostaId { get; set; }
        public int TalentoId { get; set; }
        public string NomeTalento { get; set; }
    }
}