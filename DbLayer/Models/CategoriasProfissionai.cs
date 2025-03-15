using System;
using System.Collections.Generic;

namespace DbLayer.Models;

public partial class CategoriasProfissionai
{
    public int Categoriaid { get; set; }

    public string Categoria { get; set; } = null!;

    public virtual ICollection<Habilidade> Habilidades { get; set; } = new List<Habilidade>();

    public virtual ICollection<PropostasTrabalho> PropostasTrabalhos { get; set; } = new List<PropostasTrabalho>();
}
