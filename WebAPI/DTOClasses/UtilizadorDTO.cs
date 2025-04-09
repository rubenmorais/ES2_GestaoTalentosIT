using DbLayer.Models;

namespace WebAPI.DTOClasses;

public class UtilizadorDTO {
    public int Utilizadorid { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int Tipoid { get; set; }  
    public string Tipo { get; set; } = null!; 
}