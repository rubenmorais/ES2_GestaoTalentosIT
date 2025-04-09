namespace Frontend.DTOClasses;

public class ExperienciaDTO {
    public int ExperienciaId { get; set; }

    public int TalentoId { get; set; }

    public string Titulo { get; set; }

    public string Empresa { get; set; }

    public int AnoInicio { get; set; }

    public int? AnoFim { get; set; }
} 