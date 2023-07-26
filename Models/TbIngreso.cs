using System;
using System.Collections.Generic;

namespace Presupuestos.Models;

public partial class TbIngreso
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public double Valor { get; set; }
}
