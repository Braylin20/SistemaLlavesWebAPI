using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;

public class Garantias
{
    [Key]
    public int GarantiaId { get; set; }
    public string? Descripcion { get; set; }
    public DateTime InicioGarantia { get; set; }
    public DateTime FinGarantia { get; set; }
    public bool Estado { get; set; }
}
