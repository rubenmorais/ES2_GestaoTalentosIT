using System;
using System.Collections.Generic;

namespace DbLayer.Models;

public partial class Estado
{
    public int Estadoid { get; set; }

    public string Estado1 { get; set; } = null!;

    public virtual ICollection<PropostasTrabalho> PropostasTrabalhos { get; set; } = new List<PropostasTrabalho>();
}
