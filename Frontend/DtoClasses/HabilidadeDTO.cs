namespace Frontend.DtoClasses
{
    public class HabilidadeDto
    {
        public int Habilidadeid { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Categoriaid { get; set; }
        public int Criadorid { get; set; }
    }
}