using System;
using System.Collections.Generic;

namespace DbLayer.Models;

public partial class Experiencia
{
    public int Experienciaid { get; set; }

    public int Talentoid { get; set; }

    public string Titulo { get; set; } = null!;

    public string Empresa { get; set; } = null!;

    public int AnoInicio { get; set; }

    public int? AnoFim { get; set; }

    public virtual Talento Talento { get; set; } = null!;
}
