using System;
using System.Collections.Generic;

namespace DbLayer.Models;

public partial class PropostasTrabalho
{
    public int Propostaid { get; set; }

    public int Utilizadorid { get; set; }

    public int Clienteid { get; set; }

    public string Nome { get; set; } = null!;

    public int Categoriaid { get; set; }

    public int TotalHoras { get; set; }

    public string? Descricao { get; set; }

    public int Estadoid { get; set; }

    public virtual CategoriasProfissionai Categoria { get; set; } = null!;

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Estado Estado { get; set; } = null!;

    public virtual ICollection<PropostasHabilidade> PropostasHabilidades { get; set; } = new List<PropostasHabilidade>();

    public virtual Utilizadores Utilizador { get; set; } = null!;

    public virtual ICollection<Talento> Talentos { get; set; } = new List<Talento>();
}
