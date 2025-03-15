using System;
using System.Collections.Generic;

namespace DbLayer.Models;

public partial class PropostasHabilidade
{
    public int Propostaid { get; set; }

    public int Habilidadeid { get; set; }

    public int MinAnosExp { get; set; }

    public virtual Habilidade Habilidade { get; set; } = null!;

    public virtual PropostasTrabalho Proposta { get; set; } = null!;
}
