using System;
using System.Collections.Generic;

namespace DbLayer.Models;

public partial class Utilizadores
{
    public int Utilizadorid { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PalavraPasse { get; set; } = null!;

    public int Tipoid { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Habilidade> Habilidades { get; set; } = new List<Habilidade>();

    public virtual ICollection<PropostasTrabalho> PropostasTrabalhos { get; set; } = new List<PropostasTrabalho>();

    public virtual ICollection<Talento> Talentos { get; set; } = new List<Talento>();

    public virtual Tipo Tipo { get; set; } = null!;
}
