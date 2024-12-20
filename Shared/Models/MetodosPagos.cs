using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;

public class MetodosPagos
{
    [Key]
    public int MetodoPagoId { get; set; }
    public string TipoMetodoPago { get; set; } = string.Empty;
}
