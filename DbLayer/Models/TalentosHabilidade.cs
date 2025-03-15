using System;
using System.Collections.Generic;

namespace DbLayer.Models;

public partial class TalentosHabilidade
{
    public int Talentoid { get; set; }

    public int Habilidadeid { get; set; }

    public int AnosExperiencia { get; set; }

    public virtual Habilidade Habilidade { get; set; } = null!;

    public virtual Talento Talento { get; set; } = null!;
}
