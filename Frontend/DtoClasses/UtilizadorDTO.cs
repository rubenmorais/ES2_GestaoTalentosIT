namespace Frontend.DTOClasses
{
    public class UtilizadorDTO
    {
        public int Utilizadorid { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PalavraPasse { get; set; }
        public int Tipoid { get; set; }
        public string Tipo { get; set; } = null!;
    }
}