namespace WebAPI.DTOClasses;

public class TalentosElegiveisDTO
{
    public int TalentoId    { get; set; }
    public string Nome      { get; set; }
    public string Email     { get; set; }
    public decimal PrecoHora{ get; set; }
    public int TotalHoras   { get; set; }
    public decimal TotalValue{ get; set; }
}