using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;

public class VentasDetalle
{
    [Key]
    public int VentasDetalleId { get; set; }

    public int VentaId { get; set; }

    [ForeignKey("ProductoId")]
    public Productos? Producto {  get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public double ItbisTotal { get; set; }
    public double Total { get; set; }

}
