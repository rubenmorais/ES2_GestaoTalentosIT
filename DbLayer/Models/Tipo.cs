using System;
using System.Collections.Generic;

namespace DbLayer.Models;

public partial class Tipo
{
    public int Tipoid { get; set; }

    public string Tipo1 { get; set; } = null!;

    public virtual ICollection<Utilizadores> Utilizadores { get; set; } = new List<Utilizadores>();
}
