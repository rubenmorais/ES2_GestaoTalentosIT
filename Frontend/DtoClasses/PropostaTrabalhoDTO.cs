namespace Frontend.DTOClasses
{
    public class PropostaTrabalhoDTO 
    {
        public int PropostaId { get; set; }
        public int UtilizadorId { get; set; }
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public int CategoriaId { get; set; }
        public string NomeCategoria { get; set; }
        public int TotalHoras { get; set; }
        public string Descricao { get; set; }
        public string NomeEstado { get; set; } = string.Empty;
    }
}