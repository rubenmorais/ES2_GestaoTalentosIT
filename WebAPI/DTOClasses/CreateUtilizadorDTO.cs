namespace WebAPI.DTOClasses;

public class CreateUtilizadorDTO
{
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PalavraPasse { get; set; } = null!;
    public int Tipoid { get; set; }  
}