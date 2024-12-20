using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;

public class ComprasDetalle
{
    [Key]
    public int CompraDetalleId {  get; set; }
    public int ProductoId { get; set; }
    public int CompraId { get; set; }

    [ForeignKey(nameof(ProductoId))]
    public Productos? Producto { get; set; }

    public int Cantidad { get; set; }
    public double Total { get; set; }
    
}
