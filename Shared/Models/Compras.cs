using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;

public class Compras
{
    [Key]
    public int CompraId {  get; set; }
 
    public DateTime Fecha { get; set; }

  
    public double Subtotal { get; set; }
    public double Itbis { get; set; }
    public double Total { get; set; }

    [ForeignKey("ProovedorId")]
    public Proveedores? Proveedor { get; set; }
    public int ProovedorId { get; set; }

    [ForeignKey("CompraId")]
    public ICollection<ComprasDetalle> ComprasDetalles { get; set; } = new List<ComprasDetalle>();
}
