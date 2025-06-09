using System;
using System.Collections.Generic;

namespace DbLayer.Models;

public partial class Talento
{
    public int Talentoid { get; set; }

    public int Utilizadorid { get; set; }

    public string Nome { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string Email { get; set; } = null!;

    public decimal PrecoHora { get; set; }

    public bool Visibilidade { get; set; }

    public virtual ICollection<Experiencia> Experiencia { get; set; } = new List<Experiencia>();

    public virtual ICollection<TalentosHabilidade> TalentosHabilidades { get; set; } = new List<TalentosHabilidade>();

    public virtual Utilizadores Utilizador { get; set; } = null!;

    public virtual ICollection<PropostasTrabalho> Proposta { get; set; } = new List<PropostasTrabalho>();
}
