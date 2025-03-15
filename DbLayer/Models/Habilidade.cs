using System;
using System.Collections.Generic;

namespace DbLayer.Models;

public partial class Habilidade
{
    public int Habilidadeid { get; set; }

    public string Nome { get; set; } = null!;

    public int Categoriaid { get; set; }

    public int Criadorid { get; set; }

    public virtual CategoriasProfissionai Categoria { get; set; } = null!;

    public virtual Utilizadores Criador { get; set; } = null!;

    public virtual ICollection<PropostasHabilidade> PropostasHabilidades { get; set; } = new List<PropostasHabilidade>();

    public virtual ICollection<TalentosHabilidade> TalentosHabilidades { get; set; } = new List<TalentosHabilidade>();
}
