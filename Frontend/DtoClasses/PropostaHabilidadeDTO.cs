namespace Frontend.DTOClasses
{
    public class PropostaHabilidadeDTO
    {
        public int PropostaId { get; set; }
        public int HabilidadeId { get; set; }
        public string NomeHabilidade { get; set; } = string.Empty;
        public int MinAnosExp { get; set; }
    }
}