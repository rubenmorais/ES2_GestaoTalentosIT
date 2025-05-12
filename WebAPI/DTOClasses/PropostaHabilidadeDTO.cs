using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOClasses
{
    public class PropostaHabilidadeDTO
    {
        public int PropostaId { get; set; }
        public int HabilidadeId { get; set; }
        public string NomeHabilidade { get; set; }
        public int MinAnosExp { get; set; }
    }
}