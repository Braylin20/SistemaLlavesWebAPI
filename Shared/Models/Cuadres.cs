using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;

public class Cuadres
{
    [Key]
    public int CuadreId { get; set; }
    public DateTime CierreCaja { get; set; }
    public double VentasEfectivo { get; set; }
    public double VentasTarjeta { get; set; }
    public double VentasTransferencia { get; set; }
    public double VentasTotal { get; set; }
    public double TotalEfectivo { get; set; }
    public double Diferencia { get; set; }
    public string? Observaciones { get; set; }
    public bool Pendiente { get; set; }
}
