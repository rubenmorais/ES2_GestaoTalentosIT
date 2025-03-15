using System;
using System.Collections.Generic;

namespace DbLayer.Models;

public partial class Cliente
{
    public int Clienteid { get; set; }

    public int Utilizadorid { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<PropostasTrabalho> PropostasTrabalhos { get; set; } = new List<PropostasTrabalho>();

    public virtual Utilizadores Utilizador { get; set; } = null!;
}
