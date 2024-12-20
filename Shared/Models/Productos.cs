using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;

public class Productos
{
    [Key]
    public int ProductoId {  get; set; }
    public string Nombre { get; set; } = string.Empty;
    public double Precio { get; set; }
    public double Costo { get; set; }
    public string? Descripcion { get; set; }
    public int Cantidad { get; set; }
    public double? Itbis { get; set; } = 0.18;
    public double? Descuento { get; set; }

    [ForeignKey("CategoriaId")]
    public Categorias? Categoria {  get; set; }
    public int CategoriaId {  get; set; }

    [ForeignKey("ProveedorId")]
    public Proveedores? Proveedor { get; set; }
    public int ProveedorId { get; set; }

    [ForeignKey("GarantiaId")]
    public Garantias? Garantia {  get; set; }
    public int? GarantiaId { get; set; }

}
